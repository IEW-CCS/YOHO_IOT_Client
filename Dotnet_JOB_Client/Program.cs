using System;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;
using MQTTnet;
using MQTTnet.Client;
using IniParser;
using IniParser.Model;
using YohoIoT2018.Models;
using log4net;
using log4net.Config;


namespace Dotnet_JOB_Client
{
    class Program
    {
        //每支程式以不同GUID當成Mutex名稱，可避免執行檔同名同姓的風險
        static string appGuid = "{B19DAFCB-729C-43A6-8232-F3C31BB4E404}";

        //- 1. 宣告 MQTT 實體物件 
        private static bool keepRunning = true;
        private static IMqttClient client = new MqttFactory().CreateMqttClient();

        //- 2. MISC like Config ini or other 
        private static FileIniDataParser parser = new FileIniDataParser();
        private static IniData Config = parser.ReadFile(AppContext.BaseDirectory + "/settings/Task_Client.ini");

        //- 3. Collect 容器設計 (using thread save Dictionary )
        //- DB Snyc 物件
        public static ConcurrentDictionary<string, ConcurrentDictionary<string, int>> _EDC_Label_Data = new ConcurrentDictionary<string, ConcurrentDictionary<string, int>>();

        public static ConcurrentDictionary<string, ConcurrentDictionary<string, Tuple<double, double>>> _EDC_Item_Spec = new ConcurrentDictionary<string, ConcurrentDictionary<string, Tuple<double, double>>>();


        public static ConcurrentDictionary<string, IOT_DEVICE> _IOT_Device = new ConcurrentDictionary<string, IOT_DEVICE>();
        public static ConcurrentDictionary<string, IOT_EDC_GLS_INFO> _IOT_EDC_GlassInfo = new ConcurrentDictionary<string, IOT_EDC_GLS_INFO>();

        // Push to DB Object
        public static ConcurrentDictionary<string, ConcurrentDictionary<string, int>> _Sync_EDC_Label_Data = new ConcurrentDictionary<string, ConcurrentDictionary<string, int>>();

        //  非同步使用的物件
        public static ConcurrentDictionary<string, clsEDC_Info> _IOT_EDC = new ConcurrentDictionary<string, clsEDC_Info>();
        public static ConcurrentQueue<Link_sensor_Data> _IOT_DB = new ConcurrentQueue<Link_sensor_Data>();
        public static ConcurrentQueue<Tuple<string, string>> _Write_EDC_File = new ConcurrentQueue<Tuple<string, string>>();

        //- 4. Timer setting 
        private static Timer timer_report_edc;
        private static Timer timer_report_DB;
        private static Timer timer_refresh_database;

        //-- 5. 宣告 Logger Object 
        public static log4net.ILog log = log4net.LogManager.GetLogger(typeof(Program));
        private static void LoadLog4netConfig()
        {
            var repository = LogManager.CreateRepository(
                    Assembly.GetEntryAssembly(),
                    typeof(log4net.Repository.Hierarchy.Hierarchy)
                );
            XmlConfigurator.Configure(repository, new FileInfo(System.AppContext.BaseDirectory + "/settings/log4net_IOTClient_Config.xml"));
        }

        //-- 6. Define MQTT Report  attribute
        public static string[] item_names = {"RPM","OA","OA_S","Peak","Peak_S","PtoP","PtoP_S","CF","CF_S",
                                             "B01","B01_S","B02","B02_S","B03","B03_S","B04","B04_S","B05","B05_S",
                                             "B06","B06_S","B07","B07_S","B08","B08_S","B09","B09_S","B10","B10_S", "BridgeUUID","RSSI","Channel", "NodeUUID"};

        //-- 7. Loader DB status 
        private static bool Load_DB_Finished = false;

        //-- 8. Setting MQTT Topic from ini setting
        private static string Register_Topic  = string.Empty;
        private static string Collecter_Topic = string.Empty;

        //--- Main Processing -----------
        static void Main(string[] args)
        {
            //Server need check only one process on going
            //如果要做到跨Session唯一，名稱可加入"Global\"前綴字
            //如此即使用多個帳號透過Terminal Service登入系統
            //整台機器也只能執行一份
            using (Mutex m = new Mutex(false, "Global\\" + appGuid))
            {
                //檢查是否同名Mutex已存在(表示另一份程式正在執行)
                if (!m.WaitOne(0, false))
                {
                    Console.WriteLine("Only one instance is allowed!");
                    return;
                }

                //如果是Windows Form，Application.Run()要包在using Mutex範圍內
                //以確保WinForm執行期間Mutex一直存在

                int EDC_Report_Interval = 10;
                int DB_Report_Interval  = 10;
                int DB_Refresh_Interval = 10;

                //------- 確保使用期限 -------
               /*if (OverTrialPeriod()== true)
                {
                   Console.WriteLine("The system was exceeded the trial period.");
                   return;
                }
                */


                LoadLog4netConfig();
                log.InfoFormat("[{0}] {2}-({1})", "Program", MethodBase.GetCurrentMethod().Name, "System Initial");

                try
                {
                    Console.WriteLine("Welcome DotNet Core C# MQTT Client V1.0.2");

                    //-  5.1 Setting Mqtt Topic 
                    Register_Topic =  Config["Basic"]["Register_Topic"].ToString();
                    Collecter_Topic = Config["Basic"]["Collecter_Topic"].ToString();

                    if (Register_Topic == null)
                        Register_Topic = "Reg_Device";

                    if (Collecter_Topic == null)
                        Collecter_Topic = "IOT_Device_EDC";

                    var options = new MqttClientOptionsBuilder()
                        .WithClientId(Config["MQTT"]["ClinetID"])
                        .WithTcpServer(Config["MQTT"]["BrokerIP"], Convert.ToInt32(Config["MQTT"]["BrokerPort"]))
                        .Build();

                    //- 1. setting receive topic defect # for all
                    client.Connected += async (s, e) =>
                    {
                        Console.WriteLine("### CONNECTED TO MQTT SERVER ###");
                        log.InfoFormat("[{0}] {2}-({1})", "Program", MethodBase.GetCurrentMethod().Name, "Connect to MQTT Server");
                        // await client.SubscribeAsync(new TopicFilterBuilder().WithTopic("#").WithAtMostOnceQoS().Build());
                        await client.SubscribeAsync(new TopicFilterBuilder().WithTopic(Register_Topic).WithAtMostOnceQoS().Build());
                        await client.SubscribeAsync(new TopicFilterBuilder().WithTopic(Collecter_Topic).WithAtMostOnceQoS().Build());
                    };

                    //- 2. if disconnected try to re-connect 
                    client.Disconnected += async (s, e) =>
                    {
                        Console.WriteLine("++++++++DisconnectEvent +++++++++++");
                        Console.WriteLine("### DISCONNECTED FROM SERVER ###");
                        log.ErrorFormat("[{0}] {2}-({1})", "Program", MethodBase.GetCurrentMethod().Name, "Disconnect from MQTT Server");
                        await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(10));
                        try
                        {
                            log.DebugFormat("[{0}] {2}-({1})", "Program", MethodBase.GetCurrentMethod().Name, "Try to reconnect to MQTT Server");
                            await client.ConnectAsync(options);
                        }
                        catch
                        {
                            log.ErrorFormat("[{0}] {2}-({1})", "Program", MethodBase.GetCurrentMethod().Name, "Reconnect MQTT Server Faild");
                            Console.WriteLine("### RECONNECTING FAILED ###");
                        }
                    };
                    client.ConnectAsync(options);

                    //- 3. receive 委派到 client_PublishArrived 
                    client.ApplicationMessageReceived += client_PublishArrived;

                    //- 4. Handle process Abort Event (Ctrl - C )
                    Console.CancelKeyPress += delegate (object sender, ConsoleCancelEventArgs e)
                    {
                        e.Cancel = true;
                        Program.keepRunning = false;
                    };

                    AppDomain.CurrentDomain.ProcessExit += delegate (object sender, EventArgs e)
                    {
                        log.InfoFormat("[{0}] {2}-({1})", "Program", MethodBase.GetCurrentMethod().Name, "Process is exiting!!");
                        Console.WriteLine("Process is exiting!");
                    };

                    log.InfoFormat("[{0}] {2}-({1})", "Program", MethodBase.GetCurrentMethod().Name, "System Initial Finished");

                    //- 5. Report EDC Interval time by Config setting Default 10s
                    int.TryParse(Config["EDC"]["Interval"].ToString(), out EDC_Report_Interval);
                    int.TryParse(Config["DB"]["Report_Interval"].ToString(), out DB_Report_Interval);
                    int.TryParse(Config["DB"]["Refresh_Interval"].ToString(), out DB_Refresh_Interval);

                    Timer_Report_EDC(EDC_Report_Interval);
                    Timer_Report_DB(DB_Report_Interval);
                    Timer_Refresh_DB(DB_Refresh_Interval);

                    //-  5.2 set thread pool max
                    ThreadPool.SetMaxThreads(16, 16);
                    ThreadPool.SetMinThreads(4, 4);

                    //- 6. 執行無窮迴圈等待 
                    Console.WriteLine("Please key in Ctrl+ C to exit");
                    while (Program.keepRunning)
                    {
                        ReportEDC();
                        System.Threading.Thread.Sleep(100);
                    }
                    log.InfoFormat("[{0}] {2}-({1})", "Program", MethodBase.GetCurrentMethod().Name, "Process is exited successfully !!");
                    Console.WriteLine("exited successfully");

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        // ---------- Handle MQTT Subscribe
        static void client_PublishArrived(object sender, MqttApplicationMessageReceivedEventArgs e)
        {

            if (Load_DB_Finished == false)
            {
                log.DebugFormat("[{0}] {2}-({1})", "Program", MethodBase.GetCurrentMethod().Name, "Load DB Not Ready, Skip Process");
                return;
            }

            string topic = e.ApplicationMessage.Topic;
            string message = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
            string datetimestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            Console.WriteLine(datetimestamp + " MQTT Message Arriver : Topic :" + topic.ToString());
            log.InfoFormat("{0} [{1}] {3}-({2})", datetimestamp, "Program", MethodBase.GetCurrentMethod().Name, "MQTT Message Arriver : Topic :" + topic.ToString());


            if (topic == Register_Topic)
            {
                Handle_Reg_Device(message);
            }
            else if(topic == Collecter_Topic)
            {
                Handle_IOT_DEVICE_EDC_ThreadPool(message);
               // Handle_IOT_DEVICE_EDC(message);
            }
            else
            {
               // log.DebugFormat("[{0}] {2}-({1})", "Program", MethodBase.GetCurrentMethod().Name, "TOPIC : " + topic.ToString() + "Out of handle list so Skip this Message");
               // Console.WriteLine("TOPIC : " + topic.ToString() + "out of handle list");
            }   


        }

        // ---------- This is function is put message to MQTT Broker 
        static void client_Publish_To_Broker(string Topic, string Message)
        {
            if (client.IsConnected == true)
            {
                var message = new MqttApplicationMessageBuilder()
                    .WithTopic(Topic)
                    .WithPayload(Message)
                    .Build();
                client.PublishAsync(message);
            }
        }

        // ---------- This is function Handle Alert  Bypass
        static void Handle_IOT_ALERT(string message)
        {
            //使用匿名方法，建立帶有參數的委派
            System.Threading.Thread Thread_IOT_ALERT = new System.Threading.Thread
           (
               delegate (object value)
               {
                   try
                   {
                       IOT_Alert IOT_Alert = Newtonsoft.Json.JsonConvert.DeserializeObject<IOT_Alert>(value.ToString());
                       using (var db = new IOT_DbContext())
                       {
                           IOT_ALERT OIOTAlarm = new IOT_ALERT();
                           OIOTAlarm.device_id = IOT_Alert.device_id;
                           OIOTAlarm.alert_id = IOT_Alert.alert_code;
                           OIOTAlarm.alert_lvl = IOT_Alert.alert_level;
                           OIOTAlarm.alert_msg = IOT_Alert.alert_msg;
                           OIOTAlarm.rpt_date_time = DateTime.Parse(IOT_Alert.alert_time);
                           OIOTAlarm.review_date_time = DateTime.Parse(IOT_Alert.alert_time);
                           OIOTAlarm.rpt_user = IOT_Alert.rpt_user;
                           db.Add(OIOTAlarm);
                           var count = db.SaveChanges();
                       }
                       log.InfoFormat("[{0}] {2}-({1})", "Program", MethodBase.GetCurrentMethod().Name, "Insert Alert Message to DB : " + IOT_Alert.alert_msg);
                   }

                   catch (Exception ex)
                   {
                       log.ErrorFormat("[{0}] {2}-({1})", "Program", MethodBase.GetCurrentMethod().Name, "Insert Alert Message to DB Faild ex : " + ex.Message);
                       Console.WriteLine(ex.Message);
                   }
               }
         );

            Thread_IOT_ALERT.Start(message);
        }

        // ---------- This is function Handle Device Status Bypass
        static void Handle_IOT_Device(string message)
        {
            //使用匿名方法，建立帶有參數的委派
            System.Threading.Thread Thread_IOT_Device = new System.Threading.Thread
           (
               delegate (object value)
               {
                   try
                   {
                       IOT_Status IOT_Status = Newtonsoft.Json.JsonConvert.DeserializeObject<IOT_Status>(value.ToString());
                       using (var db = new IOT_DbContext())
                       {
                           var vIOT_Device_Result = db.IOT_DEVICE.AsQueryable();
                           var _IOT_Status = vIOT_Device_Result.Where(c => (c.device_id == IOT_Status.device_id && c.device_type == IOT_Status.device_type)).FirstOrDefault();
                           if (_IOT_Status != null)
                           {
                               _IOT_Status.clm_date_time = DateTime.Now;
                               _IOT_Status.status = IOT_Status.device_status;
                               db.Update(_IOT_Status);
                               var count = db.SaveChanges();
                           }
                       }
                       log.InfoFormat("[{0}] {2}-({1})", "Program", MethodBase.GetCurrentMethod().Name, "Insert IOT_Device_Status to DB : " + IOT_Status.device_id);
                   }
                   catch (Exception ex)
                   {
                       log.ErrorFormat("[{0}] {2}-({1})", "Program", MethodBase.GetCurrentMethod().Name, "Insert IOT_Device_Status to DB Faild ex : " + ex.Message);
                       Console.WriteLine(ex.Message);
                   }

               }
         );
            Thread_IOT_Device.Start(message);
        }

        //---------- this is function of handle register device
        static void Handle_Reg_Device(string message)
        {
            //使用匿名方法，建立帶有參數的委派
            System.Threading.Thread Thread_Reg_Device = new System.Threading.Thread
           (
               delegate (object value)
               {
                   try
                   {
                       
                       IOT_Register Handle_Register = Newtonsoft.Json.JsonConvert.DeserializeObject<IOT_Register>(value.ToString());
                       using (var db = new IOT_DbContext())
                       {
                           var vIOT_Device_Result = db.IOT_DEVICE.AsQueryable();
                           var _IOT_Status = vIOT_Device_Result.Where(c => (c.device_id == Handle_Register.device_id /*&& c.device_type == IOT_Status.device_type*/)).FirstOrDefault();
                           if (_IOT_Status != null)
                           {

                                   IOT_ALERT OIOTAlarm = new IOT_ALERT();
                                   OIOTAlarm.device_id = Handle_Register.device_id;
                                   OIOTAlarm.alert_id = "0000";
                                   OIOTAlarm.alert_lvl = "H";
                                   OIOTAlarm.alert_msg = "Duplicate Device Rregister";
                                   OIOTAlarm.rpt_date_time = DateTime.Now;
                                   OIOTAlarm.rpt_user = "system";
                                   db.Add(OIOTAlarm);
                                   db.SaveChanges();
                           }
                           else
                           {
                               // add device to host 
                               IOT_DEVICE Regist_Device = new IOT_DEVICE();
                               Regist_Device.device_id = Handle_Register.device_id;
                               Regist_Device.device_desc = "This is test";
                               Regist_Device.device_type = "N";
                               Regist_Device.status = "N";
                               Regist_Device.location = "Haven";
                               Regist_Device.value = 0;
                               Regist_Device.lcl = 0;
                               Regist_Device.ucl = 0;
                               Regist_Device.lspec = 0;
                               Regist_Device.uspec = 0;
                               Regist_Device.rpt_interval = 0;
                               Regist_Device.col_interval = 0;
                               Regist_Device.oos_flg = "N";
                               Regist_Device.ooc_flg = "N";
                               Regist_Device.alarm_flg = "";
                               Regist_Device.clm_date_time = DateTime.Now;
                               Regist_Device.clm_user = "system";
                               Regist_Device.eqp_id = "test";
                               Regist_Device.sub_eqp_id = "test";
                               Regist_Device.device_no = 33;
                               db.IOT_DEVICE.Add(Regist_Device);
                               db.SaveChanges();

                           }
                       }
                       log.InfoFormat("[{0}] {2}-({1})", "Program", MethodBase.GetCurrentMethod().Name, "Insert IOT_Device to DB : " + Handle_Register.device_id);
                   }
                   catch (Exception ex)
                   {
                       log.ErrorFormat("[{0}] {2}-({1})", "Program", MethodBase.GetCurrentMethod().Name, "Insert IOT_Device to DB Faild ex : " + ex.Message);
                       Console.WriteLine(ex.Message);
                   }

               }
         );
            Thread_Reg_Device.Start(message);
        }

        // ---------- This is function Handle Device EDC
        static void Handle_IOT_DEVICE_EDC(string message)
        {
            //使用匿名方法，建立帶有參數的委派
            //防呆處理判斷json file fotmat 

            System.Threading.Thread Thread_IOT_Device_EDC = new System.Threading.Thread
            (
              delegate (object value)
              {
                  IOT_DEVICE Device = null;
                  try
                  {
                     Link_sensor_Data IOT_Device_EDC = Newtonsoft.Json.JsonConvert.DeserializeObject<Link_sensor_Data>(value.ToString());
                     Program._IOT_Device.TryGetValue(IOT_Device_EDC.PointID, out Device);

                     if (Device == null)
                     {
                         Console.WriteLine("Device_ID Not Exist so Skip This Process Device "+ IOT_Device_EDC.PointID.ToString());
                         log.DebugFormat("[{0}] {2}-({1})", "Program", MethodBase.GetCurrentMethod().Name, "Device_ID Not Exist so Skip This Process Device "+ IOT_Device_EDC.PointID.ToString());
                         return;
                     }
                     cls_Function_List Function = new cls_Function_List(IOT_Device_EDC, Device);
                     Function.Active();

                  }
                  catch (Exception ex)
                  {
                      log.ErrorFormat("[{0}] {2}-({1})", "Program", MethodBase.GetCurrentMethod().Name, "Process Faild ex : " + ex.Message);
                      Console.WriteLine(ex.Message);
                  }

              }
           );
            Thread_IOT_Device_EDC.Start(message);
        }

        // ---------- This is function Handle Device EDC
        static void Handle_IOT_DEVICE_EDC_ThreadPool(string message)
        {
            try
            {
                cls_Function_List Function = new cls_Function_List();
                ThreadPool.QueueUserWorkItem( o => Function.ThreadPool_Proc(message));
            }

            catch (Exception ex)
            {
                log.ErrorFormat("[{0}] {2}-({1})", "Program", MethodBase.GetCurrentMethod().Name, "Process Faild ex : " + ex.Message);
                log.ErrorFormat("[{0}] {2}-({1})", "Program", MethodBase.GetCurrentMethod().Name, "Message Payload  : " + message);
                Console.WriteLine(ex.Message);
            }
        }

        //==========================================================
        //----------- Threading Timer Function Define --------------
        static void Timer_Refresh_DB(int interval)
        {
            if (interval == 0)
                interval = 60000;  // 60s

            //使用匿名方法，建立帶有參數的委派
            System.Threading.Thread Thread_Timer_Refresh_DB = new System.Threading.Thread
            (
               delegate (object value)
               {
                   int Interval = Convert.ToInt32(value);
                   timer_refresh_database = new Timer(new TimerCallback(Builde_Update_DB_Information), null, 0, Interval);
               }
            );
            Thread_Timer_Refresh_DB.Start(interval);
        }

        static void Timer_Report_EDC(int interval)
        {
            if (interval == 0)
                interval = 10000;  // 10s

            //使用匿名方法，建立帶有參數的委派
            System.Threading.Thread Thread_Timer_Report_EDC = new System.Threading.Thread
            (
               delegate (object value)
               {
                   int Interval = Convert.ToInt32(value);
                   timer_report_edc = new Timer(new TimerCallback(EDC_TimerTask), null, 1000, Interval);
               }
            );
            Thread_Timer_Report_EDC.Start(interval);
        }

        static void Timer_Report_DB(int interval)
        {
            if (interval == 0)
                interval = 50000;  // 5s
            //使用匿名方法，建立帶有參數的委派
            System.Threading.Thread Thread_Timer_Report_DB = new System.Threading.Thread
            (
               delegate (object value)
               {
                   int Interval = Convert.ToInt32(value);
                   timer_report_DB = new Timer(new TimerCallback(DB_TimerTask), null, 1000, Interval);
               }
            );
            Thread_Timer_Report_DB.Start(interval);
        }

        //----------- Threading Timer Function implement --------------
        static void Builde_Update_DB_Information(object timerState)
        {
            try
            {
                using (var db = new IOT_DbContext())
                {
                    var vEDC_Label_Result = db.IOT_DEVICE_EDC_LABEL.AsQueryable().ToList();
                    foreach (IOT_DEVICE_EDC_LABEL _EDC_Label_key in vEDC_Label_Result)
                    {
                        ConcurrentDictionary<string, int> _Sub_EDC_Labels = Program._EDC_Label_Data.GetOrAdd(_EDC_Label_key.device_id, new ConcurrentDictionary<string, int>());
                        _Sub_EDC_Labels.AddOrUpdate(_EDC_Label_key.data_name, _EDC_Label_key.data_index, (key, oldvalue) => _EDC_Label_key.data_index);
                        Program._EDC_Label_Data.AddOrUpdate(_EDC_Label_key.device_id, _Sub_EDC_Labels, (key, oldvalue) => _Sub_EDC_Labels);

                        //---------------------------------------------------
                        ConcurrentDictionary<string, Tuple<double, double>> _Sub_EDC_Spec = Program._EDC_Item_Spec.GetOrAdd(_EDC_Label_key.device_id, new ConcurrentDictionary<string, Tuple<double, double>>());
                        _Sub_EDC_Spec.AddOrUpdate(_EDC_Label_key.data_name, Tuple.Create(_EDC_Label_key.lspec, _EDC_Label_key.uspec), (key, oldvalue) => Tuple.Create(_EDC_Label_key.lspec, _EDC_Label_key.uspec));
                        Program._EDC_Item_Spec.AddOrUpdate(_EDC_Label_key.device_id, _Sub_EDC_Spec, (key, oldvalue) => _Sub_EDC_Spec);

                    }

                    var vIOT_Device_Result = db.IOT_DEVICE.AsQueryable().ToList();
                    foreach (IOT_DEVICE _IOT_Device_key in vIOT_Device_Result)
                    {
                        Program._IOT_Device.AddOrUpdate(_IOT_Device_key.device_id, _IOT_Device_key, (key, oldvalue) => _IOT_Device_key);
                    }

                    var vIOT_EDC_Gls_Info_Result = db.IOT_EDC_GLS_INFO.AsQueryable().ToList();
                    foreach (IOT_EDC_GLS_INFO _IOT_EDC_Gls_Info_key in vIOT_EDC_Gls_Info_Result)
                    {
                        Program._IOT_EDC_GlassInfo.AddOrUpdate(_IOT_EDC_Gls_Info_key.sub_eqp_id, _IOT_EDC_Gls_Info_key, (key, oldvalue) => _IOT_EDC_Gls_Info_key);
                    }

                    if (_Sync_EDC_Label_Data.Count != 0)
                    {
                        foreach (KeyValuePair<string, ConcurrentDictionary<string, int>> kvp in _Sync_EDC_Label_Data)
                        {
                            ConcurrentDictionary<string, int> _Process;
                            string Key = kvp.Key;
                            _Sync_EDC_Label_Data.TryRemove(Key, out _Process);

                            foreach (KeyValuePair<string, int> _EDC_item in _Process)
                            {
                                IOT_DEVICE_EDC_LABEL oIoT_Device_EDC_label = new IOT_DEVICE_EDC_LABEL();
                                oIoT_Device_EDC_label.device_id = Key;
                                oIoT_Device_EDC_label.data_name = _EDC_item.Key;
                                oIoT_Device_EDC_label.data_index = _EDC_item.Value;
                                oIoT_Device_EDC_label.clm_date_time = DateTime.Now;
                                oIoT_Device_EDC_label.clm_user = "system";
                                db.Add(oIoT_Device_EDC_label);

                            }
                        }
                        db.SaveChanges();
                    }

                }

                if (Load_DB_Finished == false)
                {
                    Console.WriteLine("Download DB Finished Starting Processing");
                    Console.WriteLine("==========================================");
                }

                log.InfoFormat("[{0}] {2}-({1})", "Program", MethodBase.GetCurrentMethod().Name, "Update_DB Finished");
                Load_DB_Finished = true;
            }
            catch (Exception ex)
            {
                log.ErrorFormat("[{0}] {2}-({1})", "Program", MethodBase.GetCurrentMethod().Name, "Update_DB Faild ex : " + ex.Message);
                Console.WriteLine(ex.Message);
            }

        }

        static void DB_TimerTask(object timerState)
        {
            try
            {
                using (var db = new IOT_DbContext())
                {
                    Link_sensor_Data _msg = null;
                    while (_IOT_DB.TryDequeue(out _msg))
                    {
                        IOT_DEVICE Device = null;
                        _IOT_Device.TryGetValue(_msg.PointID, out Device);

                        IOT_DEVICE_EDC oIoT_DeviceEDC = new IOT_DEVICE_EDC();

                        oIoT_DeviceEDC.device_id = _msg.PointID;
                        oIoT_DeviceEDC.data_value_01 = _msg.RPM.ToString();
                        oIoT_DeviceEDC.data_value_02 = _msg.OA.ToString();
                        oIoT_DeviceEDC.data_value_03 = _msg.OA_S.ToString();
                        oIoT_DeviceEDC.data_value_04 = _msg.Peak.ToString();
                        oIoT_DeviceEDC.data_value_05 = _msg.Peak_S.ToString();
                        oIoT_DeviceEDC.data_value_06 = _msg.PtoP.ToString();
                        oIoT_DeviceEDC.data_value_07 = _msg.PtoP_S.ToString();
                        oIoT_DeviceEDC.data_value_08 = _msg.CF.ToString();
                        oIoT_DeviceEDC.data_value_09 = _msg.CF_S.ToString();
                        oIoT_DeviceEDC.data_value_10 = _msg.B01.ToString();
                        oIoT_DeviceEDC.data_value_11 = _msg.B01_S.ToString();
                        oIoT_DeviceEDC.data_value_12 = _msg.B02.ToString();
                        oIoT_DeviceEDC.data_value_13 = _msg.B02_S.ToString();
                        oIoT_DeviceEDC.data_value_14 = _msg.B03.ToString();
                        oIoT_DeviceEDC.data_value_15 = _msg.B03_S.ToString();
                        oIoT_DeviceEDC.data_value_16 = _msg.B04.ToString();
                        oIoT_DeviceEDC.data_value_17 = _msg.B04_S.ToString();
                        oIoT_DeviceEDC.data_value_18 = _msg.B05.ToString();
                        oIoT_DeviceEDC.data_value_19 = _msg.B05_S.ToString();
                        oIoT_DeviceEDC.data_value_20 = _msg.B06.ToString();
                        oIoT_DeviceEDC.data_value_21 = _msg.B06_S.ToString();
                        oIoT_DeviceEDC.data_value_22 = _msg.B07.ToString();
                        oIoT_DeviceEDC.data_value_23 = _msg.B07_S.ToString();
                        oIoT_DeviceEDC.data_value_24 = _msg.B08.ToString();
                        oIoT_DeviceEDC.data_value_25 = _msg.B08_S.ToString();
                        oIoT_DeviceEDC.data_value_26 = _msg.B09.ToString();
                        oIoT_DeviceEDC.data_value_27 = _msg.B09_S.ToString();
                        oIoT_DeviceEDC.data_value_28 = _msg.B10.ToString();
                        oIoT_DeviceEDC.data_value_29 = _msg.B10_S.ToString();

                        //---------Add New Feture  20191101 -------
                        if (!String.IsNullOrEmpty(_msg.BridgeUUID))
                        {
                            oIoT_DeviceEDC.data_value_30 = _msg.BridgeUUID.ToString();
                        }

                        oIoT_DeviceEDC.data_value_31 = _msg.RSSI.ToString();
                        oIoT_DeviceEDC.data_value_32 = _msg.Channel.ToString();
                        if (!String.IsNullOrEmpty(_msg.NodeUUID))
                        {
                            oIoT_DeviceEDC.data_value_33 = _msg.NodeUUID.ToString();
                        }

                        oIoT_DeviceEDC.clm_date_time = _msg.TimeStamp;
                        oIoT_DeviceEDC.clm_user = "SYSADM";
                        oIoT_DeviceEDC.AddDB(db, Device, oIoT_DeviceEDC);

                        Program.log.InfoFormat("[{0}] {2}-({1})", "DB_TimerTask", MethodBase.GetCurrentMethod().Name, "Insert DB Finished Device : " + _msg.PointID);
                    }

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("[{0}] {2}-({1})", "Program", MethodBase.GetCurrentMethod().Name, "Update_DB Faild ex : " + ex.Message);
                Console.WriteLine(ex.Message);
                _IOT_DB.Clear(); // kill all IOT DB Body 
                log.ErrorFormat("[{0}] {2}-({1})", "Program", MethodBase.GetCurrentMethod().Name, "Becouse Update_DB Faild so Clear All IOT DB Queue");
            }

        }

        static void EDC_TimerTask(object timerState)
        {
            try
            {
                Parallel.ForEach(_IOT_EDC, kvp =>
                {
                  if (kvp.Value.RPM.Count() != 0)
                  {
                    string result = string.Empty;
                    string EDC_File_Name = string.Empty;
                    result = kvp.Value.Export(kvp.Key, out EDC_File_Name);

                    if (result != string.Empty && EDC_File_Name != string.Empty)
                    {
                        //string EDC_Directory = Path.Combine(Config["EDC"]["Path"], kvp.Key.ToString());
                        string FilePath = Path.Combine(Config["EDC"]["Path"], EDC_File_Name);
                        _Write_EDC_File.Enqueue(Tuple.Create(FilePath, result));
                    }
                    else
                    {
                        log.ErrorFormat("[{0}] {2}-({1})", "Program", MethodBase.GetCurrentMethod().Name, "EDC File Error");
                    }

                  }

                });
            }
            catch (Exception ex)
            {
                log.ErrorFormat("[{0}] {2}-({1})", "Program", MethodBase.GetCurrentMethod().Name, "Export EDC Faild ex : " + ex.Message);
                Console.WriteLine(ex.Message);
            }

        }

        static void ReportEDC()
        {
            if (_Write_EDC_File.Count >0 )
            { 
                Tuple<string, string> _msg = null;
                while (_Write_EDC_File.TryDequeue(out _msg))
                {
                    string save_file_path = _msg.Item1;
                    string EDC_Data = _msg.Item2;
                    try
                    {
                        if (!Directory.Exists(Path.GetDirectoryName(save_file_path)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(save_file_path));
                        }
                        string temp_name = save_file_path + ".tmp";
                        using (FileStream fs = System.IO.File.Open(temp_name, FileMode.Create))
                        {
                            using (StreamWriter EDCWriter = new StreamWriter(fs, Encoding.UTF8))
                            {
                                EDCWriter.Write(EDC_Data);
                                EDCWriter.Flush();
                                fs.Flush();

                            }
                        }

                        if (System.IO.File.Exists(save_file_path))
                            System.IO.File.Delete(save_file_path);
                        while (System.IO.File.Exists(save_file_path))
                            Thread.Sleep(1);
                        System.IO.File.Move(temp_name, save_file_path);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        log.ErrorFormat("[{0}] {2}-({1})", "Program", MethodBase.GetCurrentMethod().Name, "Write EDC File Faild ex : " + ex.Message);
                    }
                }
            }

        }


        static bool OverTrialPeriod()
        {
            using (var db = new IOT_DbContext())
            {

                IOT_DEVICE_TYPE oT_DEVICE_TYPE = new IOT_DEVICE_TYPE();
                Boolean bOver = oT_DEVICE_TYPE.isOverTrialPeriod(db);
                if (bOver)
                {
                    return true;
                }
                else
                {
                    return false;

                }

            }
        }
           
    }

}

