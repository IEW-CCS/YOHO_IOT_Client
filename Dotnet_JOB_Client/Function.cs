using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Reflection;
using YohoIoT2018.Models;


namespace Dotnet_JOB_Client
{

    public class Function_Table_Base
    {
        private ConcurrentDictionary<string, Action> action_table = new ConcurrentDictionary<string, Action>();
        public void Register(string Function_key, Action method)
        {
            this.action_table.AddOrUpdate(Function_key, method, (key, oldvalue) => method);
        }
        public Action GetAction(string Function_Key)
        {
            Action action = null;
            if (this.action_table.ContainsKey(Function_Key))
            {
                action = this.action_table[Function_Key];
            }
            return action;
        }
    }

    public abstract class Function_Table
    {
        public Function_Table()
        {
            BuildFunctionTable();
        }
        protected Function_Table_Base m_Function_Table = new Function_Table_Base();
        protected abstract void BuildFunctionTable();
    }

    public class cls_Function_List : Function_Table
    {
        private string _Device_ID;
        private Link_sensor_Data _Collect_data;
        private IOT_DEVICE _Device_BODY;

        public cls_Function_List() { }
        public cls_Function_List(Link_sensor_Data Collect, IOT_DEVICE Device_BODY)
        {
            _Device_ID = Collect.PointID;
            _Collect_data = Collect;
            _Device_BODY = Device_BODY;
            BuildFunctionTable();
        }

        public void ThreadPool_Proc(string msg)
        {
            try
            {
                IOT_DEVICE Device = null;
                Link_sensor_Data IOT_Device_EDC = Newtonsoft.Json.JsonConvert.DeserializeObject<Link_sensor_Data>(msg.ToString());
                Program._IOT_Device.TryGetValue(IOT_Device_EDC.PointID, out Device);

                if (Device == null)
                {
                    Console.WriteLine("Device_ID Not Exist so Skip This Process Device <" + IOT_Device_EDC.PointID + ">");
                    Program.log.DebugFormat("[{0}] {2}-({1})", "Program", MethodBase.GetCurrentMethod().Name, "Device_ID Not Exist so Skip This Process Device <" + IOT_Device_EDC.PointID + ">");
                    return;
                }

                _Device_ID = IOT_Device_EDC.PointID;
                _Collect_data = IOT_Device_EDC;
                _Device_BODY = Device;
                BuildFunctionTable();
                Active();
            }
            catch (Exception ex)
            {
           
                 Program.log.ErrorFormat("[{0}] {2}-({1})", "Program", "ThreadPool_Proc", "Process Faild ex : " + ex.Message + " Msg : "+ msg );
                 Console.WriteLine(ex.Message);
            }
        }

        public void Active()
        {
            Program.log.InfoFormat("[{0}] {2}-({1})", GetType().Name, MethodBase.GetCurrentMethod().Name, "Active Function Device : " + _Device_ID);
            var actions = new List<Action>();
            actions.Add(this.m_Function_Table.GetAction("EDC"));
            actions.Add(this.m_Function_Table.GetAction("DB"));
          
            Parallel.Invoke(actions.ToArray());
        }
        protected override void BuildFunctionTable()
        {
            this.m_Function_Table.Register("EDC", InsertEDC);
            this.m_Function_Table.Register("DB", InsertDB);
          
        }
        private void InsertEDC()
        {
            try
            {
                clsEDC_Info TodoQueue = Program._IOT_EDC.GetOrAdd(_Device_ID, new clsEDC_Info());
                TodoQueue.add_EDC_Data(_Collect_data);
                Program._IOT_EDC.AddOrUpdate(_Device_ID, TodoQueue, (key, oldvalue) => TodoQueue);
                Program.log.InfoFormat("[{0}] {2}-({1})", GetType().Name, MethodBase.GetCurrentMethod().Name, "Insert EDC Struct Device : " + _Device_ID);
            }

            catch (Exception ex)
            {
                Program.log.ErrorFormat("[{0}] {2}-({1})", GetType().Name, MethodBase.GetCurrentMethod().Name, "Insert EDC Faild Deiver : " + _Device_ID + " ex : " + ex.Message);
                Console.WriteLine(ex.Message);
            }

        }
        private void InsertDB()
        {
            if (Program._EDC_Label_Data.ContainsKey(_Collect_data.PointID) == false)
                Create_EDC_Label_Data(_Collect_data.PointID);
            else if (Program._EDC_Label_Data[_Collect_data.PointID].Values.Count != Program.item_names.Length)
                Update_EDC_Label_Data(_Collect_data.PointID);

            try
            {
                Link_sensor_Data temp_link_sensor = new Link_sensor_Data();
                temp_link_sensor = (Link_sensor_Data)_Collect_data.Clone();
                Program._IOT_DB.Enqueue(temp_link_sensor);
                Program.log.InfoFormat("[{0}] {2}-({1})", GetType().Name, MethodBase.GetCurrentMethod().Name, "Insert DB Struct Device : " + _Device_ID);

            }
            catch (Exception ex)
            {
                Program.log.ErrorFormat("[{0}] {2}-({1})", GetType().Name, MethodBase.GetCurrentMethod().Name, "Insert DB Faild Deiver : " + _Device_ID + " ex : " + ex.Message);
                Console.WriteLine(ex.Message);
            }
        }


        private void Create_EDC_Label_Data(string PointID)
        {

            int Count = 1;
            ConcurrentDictionary<string, int> _Add_EDC_Labels = new ConcurrentDictionary<string, int>();
            foreach (string item_name in Program.item_names)
            {
                _Add_EDC_Labels.AddOrUpdate(item_name, Count, (key, oldvalue) => Count);
                Count++;
            }
            Program._EDC_Label_Data.AddOrUpdate(_Collect_data.PointID, _Add_EDC_Labels, (key, oldvalue) => _Add_EDC_Labels);
            Program._Sync_EDC_Label_Data.AddOrUpdate(_Collect_data.PointID, _Add_EDC_Labels, (key, oldvalue) => _Add_EDC_Labels);

        }
        private void Update_EDC_Label_Data(string PointID)
        {
            ConcurrentDictionary<string, int> _Current_Device_EDC_Label = Program._EDC_Label_Data.GetOrAdd(PointID, new ConcurrentDictionary<string, int>());
            ConcurrentDictionary<string, int> _Sync_Device_EDC_Label = new ConcurrentDictionary<string, int>();

            foreach (string EDC_items in Program.item_names)
            {
                if (_Current_Device_EDC_Label.ContainsKey(EDC_items) == false)
                {
                    int _insert_index = _Current_Device_EDC_Label.Count + 1;
                    _Current_Device_EDC_Label.AddOrUpdate(EDC_items, _insert_index, (key, oldvalue) => _insert_index);
                    _Sync_Device_EDC_Label.AddOrUpdate(EDC_items, _insert_index, (key, oldvalue) => _insert_index);
                }
            }

            Program._EDC_Label_Data.AddOrUpdate(_Collect_data.PointID, _Current_Device_EDC_Label, (key, oldvalue) => _Current_Device_EDC_Label);
            Program._Sync_EDC_Label_Data.AddOrUpdate(_Collect_data.PointID, _Sync_Device_EDC_Label, (key, oldvalue) => _Sync_Device_EDC_Label);

        }
    }
}
