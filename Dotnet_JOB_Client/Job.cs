using System;
using System.Collections.Generic;
using System.Linq;
using YohoIoT2018.Models;
using System.Reflection;
using System.Collections.Concurrent;
using System.IO;

namespace Dotnet_JOB_Client
{


    public class Link_sensor_Data
    {
        // For 2019/11/01 V5 Updata Check
        public string BridgeUUID;
        public double RSSI;
        public int Channel;
        public string NodeUUID;
        //--------------------------

        public string PointID;
        public DateTime TimeStamp;
        public double RPM;
        public double OA;
        public double OA_S;
        public double Peak;
        public double Peak_S;
        public double PtoP;
        public double PtoP_S;
        public double CF;
        public double CF_S;
        public double B01;
        public double B01_S;
        public double B02;
        public double B02_S;
        public double B03;
        public double B03_S;
        public double B04;
        public double B04_S;
        public double B05;
        public double B05_S;
        public double B06;
        public double B06_S;
        public double B07;
        public double B07_S;
        public double B08;
        public double B08_S;
        public double B09;
        public double B09_S;
        public double B10;
        public double B10_S;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class sensor_Data
    {
        public string value;
        public string device_id;
        public string control;
        public sensor_Data(  string _device_id, string _value)
        {
            value = _value;
            device_id = _device_id;
            control = "";
        }
    }

    public class cls_Collect_Manager
    {
        public cls_Collect_Manager()
        {

        }
        public cls_Collect_Manager(string device_id, string value)
        {
            AddData(device_id, value);
        }

        public cls_Collect_Manager(string tmpPointID, DateTime tmpDatetitme)
        {
            _PointID = tmpPointID;
            _Datetitme = tmpDatetitme;

        }

        public string _PointID;
        public DateTime _Datetitme;

        public List<sensor_Data> Data = new List<sensor_Data>();
        public void AddData(string device_id, string value)
        {
            Data.Add(new sensor_Data( device_id, value));

        }
    }


    public class clsEDC_Info
    {
        public clsEDC_Info()
        {

            RPM.Clear();
            OA.Clear();
            OA_S.Clear();
            Peak.Clear();
            Peak_S.Clear();
            PtoP.Clear();
            PtoP_S.Clear();
            CF.Clear();
            CF_S.Clear();
            B01.Clear();
            B01_S.Clear();
            B02.Clear();
            B02_S.Clear();
            B03.Clear();
            B03_S.Clear();
            B04.Clear();
            B04_S.Clear();
            B05.Clear();
            B05_S.Clear();
            B06.Clear();
            B06_S.Clear();
            B07.Clear();
            B07_S.Clear();
            B08.Clear();
            B08_S.Clear();
            B09.Clear();
            B09_S.Clear();
            B10.Clear();
            B10_S.Clear();


            //===== Modify 2019 11 01 =====
            BridgeUUID.Clear();
            RSSI.Clear();
            Channel.Clear();
            NodeUUID.Clear();
        }

        public List<double> RPM = new List<double>();
        public List<double> OA = new List<double>();
        public List<double> OA_S = new List<double>();
        public List<double> Peak = new List<double>();
        public List<double> Peak_S = new List<double>();
        public List<double> PtoP = new List<double>();
        public List<double> PtoP_S = new List<double>();
        public List<double> CF = new List<double>();
        public List<double> CF_S = new List<double>();
        public List<double> B01 = new List<double>();
        public List<double> B01_S = new List<double>();
        public List<double> B02 = new List<double>();
        public List<double> B02_S = new List<double>();
        public List<double> B03 = new List<double>();
        public List<double> B03_S = new List<double>();
        public List<double> B04 = new List<double>();
        public List<double> B04_S = new List<double>();
        public List<double> B05 = new List<double>();
        public List<double> B05_S = new List<double>();
        public List<double> B06 = new List<double>();
        public List<double> B06_S = new List<double>();
        public List<double> B07 = new List<double>();
        public List<double> B07_S = new List<double>();
        public List<double> B08 = new List<double>();
        public List<double> B08_S = new List<double>();
        public List<double> B09 = new List<double>();
        public List<double> B09_S = new List<double>();
        public List<double> B10 = new List<double>();
        public List<double> B10_S = new List<double>();

        //---------- 2019 11 01  Modify ------------------

        public List<string> BridgeUUID = new List<string>();
        public List<double> RSSI = new List<double>();
        public List<double> Channel = new List<double>();
        public List<string> NodeUUID = new List<string>();
        //------------------------------------------




        public void add_EDC_Data(Link_sensor_Data _temp)
        {
            RPM.Add(Convert.ToDouble(_temp.RPM));
            OA.Add(Convert.ToDouble(_temp.OA));
            OA_S.Add(Convert.ToDouble(_temp.OA_S));
            Peak.Add(Convert.ToDouble(_temp.Peak));
            Peak_S.Add(Convert.ToDouble(_temp.Peak_S));
            PtoP.Add(Convert.ToDouble(_temp.PtoP));
            PtoP_S.Add(Convert.ToDouble(_temp.PtoP_S));
            CF.Add(Convert.ToDouble(_temp.CF));
            CF_S.Add(Convert.ToDouble(_temp.CF_S));
            B01.Add(Convert.ToDouble(_temp.B01));
            B01_S.Add(Convert.ToDouble(_temp.B01_S));
            B02.Add(Convert.ToDouble(_temp.B02));
            B02_S.Add(Convert.ToDouble(_temp.B02_S));
            B03.Add(Convert.ToDouble(_temp.B03));
            B03_S.Add(Convert.ToDouble(_temp.B03_S));
            B04.Add(Convert.ToDouble(_temp.B04));
            B04_S.Add(Convert.ToDouble(_temp.B04_S));
            B05.Add(Convert.ToDouble(_temp.B05));
            B05_S.Add(Convert.ToDouble(_temp.B05_S));
            B06.Add(Convert.ToDouble(_temp.B06));
            B06_S.Add(Convert.ToDouble(_temp.B06_S));
            B07.Add(Convert.ToDouble(_temp.B07));
            B07_S.Add(Convert.ToDouble(_temp.B07_S));
            B08.Add(Convert.ToDouble(_temp.B08));
            B08_S.Add(Convert.ToDouble(_temp.B08_S));
            B09.Add(Convert.ToDouble(_temp.B09));
            B09_S.Add(Convert.ToDouble(_temp.B09_S));
            B10.Add(Convert.ToDouble(_temp.B10));
            B10_S.Add(Convert.ToDouble(_temp.B10_S));

            //=================================


            //---------- 2019 11 01  Modify ------------------
            BridgeUUID.Add(_temp.BridgeUUID);
            RSSI.Add(Convert.ToDouble(_temp.RSSI));
            Channel.Add(Convert.ToDouble(_temp.Channel));
            NodeUUID.Add(_temp.NodeUUID);

        }

        public string Export(string _PointID, out string EDC_Path )
        {

            EDC_Report EDC = new EDC_Report(_PointID, DateTime.Now);
            string Return_EDC_String = string.Empty;
            string TempStr = string.Empty;
            int lspec_count = 0, uspec_count = 0, OOS_count = 0;

            // by chris 20190401 add spec check list 
            ConcurrentDictionary<string, Tuple<double, double>> Spcecheck = Program._EDC_Item_Spec[_PointID];


            //========== 2019 11 01  Add New Items =======
            //0.1 --------  Add Bridge UUID  ------------
            EDC_Report.IARYClass _bridge_uuid = EDC.CreateIARYClass();
            _bridge_uuid.ITEM_NAME = "Bridge_UUID";
            _bridge_uuid.ITEM_TYPE = "X";
            _bridge_uuid.ITEM_VALUE = BridgeUUID.FirstOrDefault();

            //=======================================================
            //0.2 --------  Add RSSI MAX AVG ------------
            EDC_Report.IARYClass _RSSI_MAX_data = EDC.CreateIARYClass();
            _RSSI_MAX_data.ITEM_NAME = "RSSI_MAX";
            _RSSI_MAX_data.ITEM_TYPE = "X";
            TempStr = RSSI.Max().ToString("#0.######");
            if (TempStr.Length > 16)
                _RSSI_MAX_data.ITEM_VALUE = TempStr.Substring(0, 16);
            else
                _RSSI_MAX_data.ITEM_VALUE = TempStr;

            EDC_Report.IARYClass _RSSI_AVG_data = EDC.CreateIARYClass();
            _RSSI_AVG_data.ITEM_NAME = "RSSI_AVG";
            _RSSI_AVG_data.ITEM_TYPE = "X";
            TempStr = RSSI.Average().ToString("#0.######");
            if (TempStr.Length > 16)
                _RSSI_AVG_data.ITEM_VALUE = TempStr.Substring(0, 16);
            else
                _RSSI_AVG_data.ITEM_VALUE = TempStr;


            //=======================================================
            //0.3 --------  Add Channel  ------------
            EDC_Report.IARYClass _channel = EDC.CreateIARYClass();
            _channel.ITEM_NAME = "Channel";
            _channel.ITEM_TYPE = "X";
            _channel.ITEM_VALUE = Channel.FirstOrDefault().ToString();


            //=======================================================
            //0.4 --------  Add Channel  ------------
            EDC_Report.IARYClass _nodeuuid = EDC.CreateIARYClass();
            _nodeuuid.ITEM_NAME = "NodeUUID";
            _nodeuuid.ITEM_TYPE = "X";
            _nodeuuid.ITEM_VALUE = NodeUUID.FirstOrDefault().ToString();

            //=======================================================
            //1. --------  Add total count of edc data list ------------
            EDC_Report.IARYClass _data_count = EDC.CreateIARYClass();
            _data_count.ITEM_NAME = "TOTAL_COUNT";
            _data_count.ITEM_TYPE = "X";
            TempStr = RPM.Count.ToString("#0.######"); // 計算 Count 總共幾筆
            _data_count.ITEM_VALUE = TempStr;

            //=======================================================
            //2. --------  RPM data  MAX AVG OOS------------
            EDC_Report.IARYClass _RPM_MAX_data = EDC.CreateIARYClass();
            _RPM_MAX_data.ITEM_NAME = "RPM_MAX";
            _RPM_MAX_data.ITEM_TYPE = "X";
            TempStr = RPM.Max().ToString("#0.######");
            if (TempStr.Length > 16)
                _RPM_MAX_data.ITEM_VALUE = TempStr.Substring(0, 16);
            else
                _RPM_MAX_data.ITEM_VALUE = TempStr;

            EDC_Report.IARYClass _RPM_AVG_data = EDC.CreateIARYClass();
            _RPM_AVG_data.ITEM_NAME = "RPM_AVG";
            _RPM_AVG_data.ITEM_TYPE = "X";
            TempStr = RPM.Average().ToString("#0.######");
            if (TempStr.Length > 16)
                _RPM_AVG_data.ITEM_VALUE = TempStr.Substring(0, 16);
            else
                _RPM_AVG_data.ITEM_VALUE = TempStr;

            //OOS
            lspec_count = 0; uspec_count = 0;
            if (Spcecheck["RPM"].Item1 != -999999 && Spcecheck["RPM"].Item1 != 999999)
                 lspec_count = RPM.Count(p => p < Spcecheck["RPM"].Item1);

            if (Spcecheck["RPM"].Item2 != -999999 && Spcecheck["RPM"].Item2 != 999999)
                 uspec_count = RPM.Count(p => p > Spcecheck["RPM"].Item2);

            OOS_count = lspec_count + uspec_count;

            EDC_Report.IARYClass _RPM_OOS_data = EDC.CreateIARYClass();
            _RPM_OOS_data.ITEM_NAME = "RPM_OOS";
            _RPM_OOS_data.ITEM_TYPE = "X";
            TempStr = OOS_count.ToString();
            _RPM_OOS_data.ITEM_VALUE = TempStr;

            RPM.Clear();

            //=======================================================
            //3. --------  OA data  MAX AVG OOS------------

            EDC_Report.IARYClass _OA_MAX_data = EDC.CreateIARYClass();
            _OA_MAX_data.ITEM_NAME = "OA_MAX";
            _OA_MAX_data.ITEM_TYPE = "X";
            TempStr = OA.Max().ToString("#0.######");
            if (TempStr.Length > 16)
                _OA_MAX_data.ITEM_VALUE = TempStr.Substring(0, 16);
            else
                _OA_MAX_data.ITEM_VALUE = TempStr;

            // _OA_MAX_data.ITEM_VALUE = OA.Max().ToString("f10");
            EDC_Report.IARYClass _OA_AVG_data = EDC.CreateIARYClass();
            _OA_AVG_data.ITEM_NAME = "OA_AVG";
            _OA_AVG_data.ITEM_TYPE = "X";
            TempStr = OA.Average().ToString("#0.######");
            if (TempStr.Length > 16)
                _OA_AVG_data.ITEM_VALUE = TempStr.Substring(0, 16);
            else
                _OA_AVG_data.ITEM_VALUE = TempStr;

            //OOS
            lspec_count = 0; uspec_count = 0;
            if (Spcecheck["OA"].Item1 != -999999 && Spcecheck["OA"].Item1 != 999999)
                lspec_count = OA.Count(p => p < Spcecheck["OA"].Item1);

            if (Spcecheck["OA"].Item2 != -999999 && Spcecheck["OA"].Item2 != 999999)
                uspec_count = OA.Count(p => p > Spcecheck["OA"].Item2);

            OOS_count = lspec_count + uspec_count;
            EDC_Report.IARYClass _OA_OOS_data = EDC.CreateIARYClass();
            _OA_OOS_data.ITEM_NAME = "OA_OOS";
            _OA_OOS_data.ITEM_TYPE = "X";
            TempStr = OOS_count.ToString();
            _OA_OOS_data.ITEM_VALUE = TempStr;

            OA.Clear();

            //=======================================================
            //4. --------  OA_S data  MAX AVG OOS------------

            EDC_Report.IARYClass _OA_S_MAX_data = EDC.CreateIARYClass();
            _OA_S_MAX_data.ITEM_NAME = "OA_S_MAX";
            _OA_S_MAX_data.ITEM_TYPE = "X";
            _OA_S_MAX_data.ITEM_VALUE = OA_S.Max().ToString("#0.###");
            EDC_Report.IARYClass _OA_S_AVG_data = EDC.CreateIARYClass();
            _OA_S_AVG_data.ITEM_NAME = "OA_S_AVG";
            _OA_S_AVG_data.ITEM_TYPE = "X";
            _OA_S_AVG_data.ITEM_VALUE = OA_S.Average().ToString("#0.###");

            lspec_count = 0; uspec_count = 0;
            if (Spcecheck["OA_S"].Item1 != -999999 && Spcecheck["OA_S"].Item1 != 999999)
                lspec_count = OA_S.Count(p => p < Spcecheck["OA_S"].Item1);

            if (Spcecheck["OA_S"].Item2 != -999999 && Spcecheck["OA_S"].Item2 != 999999)
                uspec_count = OA_S.Count(p => p > Spcecheck["OA_S"].Item2);

            OOS_count = lspec_count + uspec_count;
            EDC_Report.IARYClass _OA_S_OOS_data = EDC.CreateIARYClass();
            _OA_S_OOS_data.ITEM_NAME = "OA_S_OOS";
            _OA_S_OOS_data.ITEM_TYPE = "X";
            TempStr = OOS_count.ToString();
            _OA_S_OOS_data.ITEM_VALUE = TempStr;

            OA_S.Clear();

            //=======================================================
            //5. --------  Peak data  MAX AVG OOS------------

            EDC_Report.IARYClass _Peak_MAX_data = EDC.CreateIARYClass();
            _Peak_MAX_data.ITEM_NAME = "Peak_MAX";
            _Peak_MAX_data.ITEM_TYPE = "X";
            TempStr = Peak.Max().ToString("#0.######");
            if (TempStr.Length > 16)
                _Peak_MAX_data.ITEM_VALUE = TempStr.Substring(0, 16);
            else
                _Peak_MAX_data.ITEM_VALUE = TempStr;

            EDC_Report.IARYClass _Peak_AVG_data = EDC.CreateIARYClass();
            _Peak_AVG_data.ITEM_NAME = "Peak_AVG";
            _Peak_AVG_data.ITEM_TYPE = "X";
            TempStr = Peak.Average().ToString("#0.######");
            if (TempStr.Length > 16)
                _Peak_AVG_data.ITEM_VALUE = TempStr.Substring(0, 16);
            else
                _Peak_AVG_data.ITEM_VALUE = TempStr;

            //OOS
            lspec_count = 0; uspec_count = 0;
            if (Spcecheck["Peak"].Item1 != -999999 && Spcecheck["Peak"].Item1 != 999999)
                lspec_count = Peak.Count(p => p < Spcecheck["Peak"].Item1);

            if (Spcecheck["Peak"].Item2 != -999999 && Spcecheck["Peak"].Item2 != 999999)
                uspec_count = Peak.Count(p => p > Spcecheck["Peak"].Item2);

            OOS_count = lspec_count + uspec_count;
            EDC_Report.IARYClass _Peak_OOS_data = EDC.CreateIARYClass();
            _Peak_OOS_data.ITEM_NAME = "Peak_OOS";
            _Peak_OOS_data.ITEM_TYPE = "X";
            TempStr = OOS_count.ToString();
            _Peak_OOS_data.ITEM_VALUE = TempStr;

            Peak.Clear();


            //=======================================================
            //6. --------  Peak_S data  MAX AVG OOS------------

            EDC_Report.IARYClass _Peak_S_MAX_data = EDC.CreateIARYClass();
            _Peak_S_MAX_data.ITEM_NAME = "Peak_S_MAX";
            _Peak_S_MAX_data.ITEM_TYPE = "X";
            _Peak_S_MAX_data.ITEM_VALUE = Peak_S.Max().ToString("#0.###");
            EDC_Report.IARYClass _Peak_S_AVG_data = EDC.CreateIARYClass();
            _Peak_S_AVG_data.ITEM_NAME = "Peak_S_AVG";
            _Peak_S_AVG_data.ITEM_TYPE = "X";
            _Peak_S_AVG_data.ITEM_VALUE = Peak_S.Average().ToString("#0.###");
            //------------------------------------------------------
            lspec_count = 0; uspec_count = 0;
            if (Spcecheck["Peak_S"].Item1 != -999999 && Spcecheck["Peak_S"].Item1 != 999999)
                lspec_count = Peak_S.Count(p => p < Spcecheck["Peak_S"].Item1);

            if (Spcecheck["Peak_S"].Item2 != -999999 && Spcecheck["Peak_S"].Item2 != 999999)
                uspec_count = Peak_S.Count(p => p > Spcecheck["Peak_S"].Item2);

            OOS_count = lspec_count + uspec_count;
            EDC_Report.IARYClass _Peak_S_OOS_data = EDC.CreateIARYClass();
            _Peak_S_OOS_data.ITEM_NAME = "Peak_S_OOS";
            _Peak_S_OOS_data.ITEM_TYPE = "X";
            TempStr = OOS_count.ToString();
            _Peak_S_OOS_data.ITEM_VALUE = TempStr;

            Peak_S.Clear();

            //=======================================================
            //7. --------  PtoP data  MAX AVG OOS------------

            EDC_Report.IARYClass _PtoP_MAX_data = EDC.CreateIARYClass();
            _PtoP_MAX_data.ITEM_NAME = "PtoP_MAX";
            _PtoP_MAX_data.ITEM_TYPE = "X";
            TempStr = PtoP.Max().ToString("#0.######");
            if (TempStr.Length > 16)
                _PtoP_MAX_data.ITEM_VALUE = TempStr.Substring(0, 16);
            else
                _PtoP_MAX_data.ITEM_VALUE = TempStr;

            EDC_Report.IARYClass _PtoP_AVG_data = EDC.CreateIARYClass();
            _PtoP_AVG_data.ITEM_NAME = "PtoP_AVG";
            _PtoP_AVG_data.ITEM_TYPE = "X";
            TempStr = PtoP.Average().ToString("#0.######");
            if (TempStr.Length > 16)
                _PtoP_AVG_data.ITEM_VALUE = TempStr.Substring(0, 16);
            else
                _PtoP_AVG_data.ITEM_VALUE = TempStr;

            //OOS
            lspec_count = 0; uspec_count = 0;
            if (Spcecheck["PtoP"].Item1 != -999999 && Spcecheck["PtoP"].Item1 != 999999)
                lspec_count = PtoP.Count(p => p < Spcecheck["PtoP"].Item1);

            if (Spcecheck["PtoP"].Item2 != -999999 && Spcecheck["PtoP"].Item2 != 999999)
                uspec_count = PtoP.Count(p => p > Spcecheck["PtoP"].Item2);

            OOS_count = lspec_count + uspec_count;
            EDC_Report.IARYClass _PtoP_OOS_data = EDC.CreateIARYClass();
            _PtoP_OOS_data.ITEM_NAME = "PtoP_OOS";
            _PtoP_OOS_data.ITEM_TYPE = "X";
            TempStr = OOS_count.ToString();
            _PtoP_OOS_data.ITEM_VALUE = TempStr;
            
            PtoP.Clear();

            //=======================================================
            //8. --------  PtoP_S data  MAX AVG OOS------------

            EDC_Report.IARYClass _PtoP_S_MAX_data = EDC.CreateIARYClass();
            _PtoP_S_MAX_data.ITEM_NAME = "PtoP_S_MAX";
            _PtoP_S_MAX_data.ITEM_TYPE = "X";
            _PtoP_S_MAX_data.ITEM_VALUE = PtoP_S.Max().ToString("#0.###");
            EDC_Report.IARYClass _PtoP_S_AVG_data = EDC.CreateIARYClass();
            _PtoP_S_AVG_data.ITEM_NAME = "PtoP_S_AVG";
            _PtoP_S_AVG_data.ITEM_TYPE = "X";
            _PtoP_S_AVG_data.ITEM_VALUE = PtoP_S.Average().ToString("#0.###");

            //OOS-
            lspec_count = 0; uspec_count = 0;
            if (Spcecheck["PtoP_S"].Item1 != -999999 && Spcecheck["PtoP_S"].Item1 != 999999)
                lspec_count = PtoP_S.Count(p => p < Spcecheck["PtoP_S"].Item1);

            if (Spcecheck["PtoP_S"].Item2 != -999999 && Spcecheck["PtoP_S"].Item2 != 999999)
                uspec_count = PtoP_S.Count(p => p > Spcecheck["PtoP_S"].Item2);

            OOS_count = lspec_count + uspec_count;
            EDC_Report.IARYClass _PtoP_S_OOS_data = EDC.CreateIARYClass();
            _PtoP_S_OOS_data.ITEM_NAME = "PtoP_S_OOS";
            _PtoP_S_OOS_data.ITEM_TYPE = "X";
            TempStr = OOS_count.ToString();
            _PtoP_S_OOS_data.ITEM_VALUE = TempStr;

            PtoP_S.Clear();

            //=======================================================
            //9. --------  CF data  MAX AVG OOS------------

            EDC_Report.IARYClass _CF_MAX_data = EDC.CreateIARYClass();
            _CF_MAX_data.ITEM_NAME = "CF_MAX";
            _CF_MAX_data.ITEM_TYPE = "X";
            TempStr = CF.Max().ToString("#0.######");
            if (TempStr.Length > 16)
                _CF_MAX_data.ITEM_VALUE = TempStr.Substring(0, 16);
            else
                _CF_MAX_data.ITEM_VALUE = TempStr;

            EDC_Report.IARYClass _CF_AVG_data = EDC.CreateIARYClass();
            _CF_AVG_data.ITEM_NAME = "CF_AVG";
            _CF_AVG_data.ITEM_TYPE = "X";
            TempStr = CF.Average().ToString("#0.######");
            if (TempStr.Length > 16)
                _CF_AVG_data.ITEM_VALUE = TempStr.Substring(0, 16);
            else
                _CF_AVG_data.ITEM_VALUE = TempStr;
            //OOS
            lspec_count = 0; uspec_count = 0;
            if (Spcecheck["CF"].Item1 != -999999 && Spcecheck["CF"].Item1 != 999999)
                lspec_count = CF.Count(p => p < Spcecheck["CF"].Item1);

            if (Spcecheck["CF"].Item2 != -999999 && Spcecheck["CF"].Item2 != 999999)
                uspec_count = CF.Count(p => p > Spcecheck["CF"].Item2);

            OOS_count = lspec_count + uspec_count;
            EDC_Report.IARYClass _CF_OOS_data = EDC.CreateIARYClass();
            _CF_OOS_data.ITEM_NAME = "CF_OOS";
            _CF_OOS_data.ITEM_TYPE = "X";
            TempStr = OOS_count.ToString();
            _CF_OOS_data.ITEM_VALUE = TempStr;

            CF.Clear();

            //=======================================================
            //10. --------  CF_S data  MAX AVG OOS------------

            EDC_Report.IARYClass _CF_S_MAX_data = EDC.CreateIARYClass();
            _CF_S_MAX_data.ITEM_NAME = "CF_S_MAX";
            _CF_S_MAX_data.ITEM_TYPE = "X";
            _CF_S_MAX_data.ITEM_VALUE = CF_S.Max().ToString("#0.###");
            EDC_Report.IARYClass _CF_S_AVG_data = EDC.CreateIARYClass();
            _CF_S_AVG_data.ITEM_NAME = "CF_S_AVG";
            _CF_S_AVG_data.ITEM_TYPE = "X";
            _CF_S_AVG_data.ITEM_VALUE = CF_S.Average().ToString("#0.###");

            //OOS
            lspec_count = 0; uspec_count = 0;
            if (Spcecheck["CF_S"].Item1 != -999999 && Spcecheck["CF_S"].Item1 != 999999)
                lspec_count = CF_S.Count(p => p < Spcecheck["CF_S"].Item1);

            if (Spcecheck["CF_S"].Item2 != -999999 && Spcecheck["CF_S"].Item2 != 999999)
                uspec_count = CF_S.Count(p => p > Spcecheck["CF_S"].Item2);

            OOS_count = lspec_count + uspec_count;
            EDC_Report.IARYClass _CF_S_OOS_data = EDC.CreateIARYClass();
            _CF_S_OOS_data.ITEM_NAME = "CF_S_OOS";
            _CF_S_OOS_data.ITEM_TYPE = "X";
            TempStr = OOS_count.ToString();
            _CF_S_OOS_data.ITEM_VALUE = TempStr;

            CF_S.Clear();

            //=======================================================
            //11. --------  B01 data  MAX AVG OOS------------

            EDC_Report.IARYClass _B01_MAX_data = EDC.CreateIARYClass();
            _B01_MAX_data.ITEM_NAME = "B01_MAX";
            _B01_MAX_data.ITEM_TYPE = "X";
            TempStr = B01.Max().ToString("#0.######");
            if (TempStr.Length > 16)
                _B01_MAX_data.ITEM_VALUE = TempStr.Substring(0, 16);
            else
                _B01_MAX_data.ITEM_VALUE = TempStr;

            EDC_Report.IARYClass _B01_AVG_data = EDC.CreateIARYClass();
            _B01_AVG_data.ITEM_NAME = "B01_AVG";
            _B01_AVG_data.ITEM_TYPE = "X";
            TempStr = B01.Average().ToString("#0.######");
            if (TempStr.Length > 16)
                _B01_AVG_data.ITEM_VALUE = TempStr.Substring(0, 16);
            else
                _B01_AVG_data.ITEM_VALUE = TempStr;

            //OOS
            lspec_count = 0; uspec_count = 0;
            if (Spcecheck["B01"].Item1 != -999999 && Spcecheck["B01"].Item1 != 999999)
                lspec_count = B01.Count(p => p < Spcecheck["B01"].Item1);

            if (Spcecheck["B01"].Item2 != -999999 && Spcecheck["B01"].Item2 != 999999)
                uspec_count = B01.Count(p => p > Spcecheck["B01"].Item2);

            OOS_count = lspec_count + uspec_count;
            EDC_Report.IARYClass _B01_OOS_data = EDC.CreateIARYClass();
            _B01_OOS_data.ITEM_NAME = "B01_OOS";
            _B01_OOS_data.ITEM_TYPE = "X";
            TempStr = OOS_count.ToString();
            _B01_OOS_data.ITEM_VALUE = TempStr;
            
            B01.Clear();
            //=======================================================
            //12. --------  B01_S data  MAX AVG OOS------------

            EDC_Report.IARYClass _B01_S_MAX_data = EDC.CreateIARYClass();
            _B01_S_MAX_data.ITEM_NAME = "B01_S_MAX";
            _B01_S_MAX_data.ITEM_TYPE = "X";
            _B01_S_MAX_data.ITEM_VALUE = B01_S.Max().ToString("#0.###");
            EDC_Report.IARYClass _B01_S_AVG_data = EDC.CreateIARYClass();
            _B01_S_AVG_data.ITEM_NAME = "B01_S_AVG";
            _B01_S_AVG_data.ITEM_TYPE = "X";
            _B01_S_AVG_data.ITEM_VALUE = B01_S.Average().ToString("#0.###");

            //OOS
            lspec_count = 0; uspec_count = 0;
            if (Spcecheck["B01_S"].Item1 != -999999 && Spcecheck["B01_S"].Item1 != 999999)
                lspec_count = B01_S.Count(p => p < Spcecheck["B01_S"].Item1);

            if (Spcecheck["B01_S"].Item2 != -999999 && Spcecheck["B01_S"].Item2 != 999999)
                uspec_count = B01_S.Count(p => p > Spcecheck["B01_S"].Item2);

            OOS_count = lspec_count + uspec_count;
            EDC_Report.IARYClass _B01_S_OOS_data = EDC.CreateIARYClass();
            _B01_S_OOS_data.ITEM_NAME = "B01_S_OOS";
            _B01_S_OOS_data.ITEM_TYPE = "X";
            TempStr = OOS_count.ToString();
            _B01_S_OOS_data.ITEM_VALUE = TempStr;

            B01_S.Clear();
            //=======================================================
            //13. --------  B02 data  MAX AVG OOS------------

            EDC_Report.IARYClass _B02_MAX_data = EDC.CreateIARYClass();
            _B02_MAX_data.ITEM_NAME = "B02_MAX";
            _B02_MAX_data.ITEM_TYPE = "X";
            TempStr = B02.Max().ToString("#0.######");
            if (TempStr.Length > 16)
                _B02_MAX_data.ITEM_VALUE = TempStr.Substring(0, 16);
            else
                _B02_MAX_data.ITEM_VALUE = TempStr;

            EDC_Report.IARYClass _B02_AVG_data = EDC.CreateIARYClass();
            _B02_AVG_data.ITEM_NAME = "B02_AVG";
            _B02_AVG_data.ITEM_TYPE = "X";
            TempStr = B02.Average().ToString("#0.######");
            if (TempStr.Length > 16)
                _B02_AVG_data.ITEM_VALUE = TempStr.Substring(0, 16);
            else
                _B02_AVG_data.ITEM_VALUE = TempStr;

            //OOS
            lspec_count = 0; uspec_count = 0;
            if (Spcecheck["B02"].Item1 != -999999 && Spcecheck["B02"].Item1 != 999999)
                lspec_count = B02.Count(p => p < Spcecheck["B02"].Item1);

            if (Spcecheck["B02"].Item2 != -999999 && Spcecheck["B02"].Item2 != 999999)
                uspec_count = B02.Count(p => p > Spcecheck["B02"].Item2);

            OOS_count = lspec_count + uspec_count;
            EDC_Report.IARYClass _B02_OOS_data = EDC.CreateIARYClass();
            _B02_OOS_data.ITEM_NAME = "B02_OOS";
            _B02_OOS_data.ITEM_TYPE = "X";
            TempStr = OOS_count.ToString();
            _B02_OOS_data.ITEM_VALUE = TempStr;

            B02.Clear();
            //=======================================================
            //14. --------  B02_S data  MAX AVG OOS------------

            EDC_Report.IARYClass _B02_S_MAX_data = EDC.CreateIARYClass();
            _B02_S_MAX_data.ITEM_NAME = "B02_S_MAX";
            _B02_S_MAX_data.ITEM_TYPE = "X";
            _B02_S_MAX_data.ITEM_VALUE = B02_S.Max().ToString("#0.###");
            EDC_Report.IARYClass _B02_S_AVG_data = EDC.CreateIARYClass();
            _B02_S_AVG_data.ITEM_NAME = "B02_S_AVG";
            _B02_S_AVG_data.ITEM_TYPE = "X";
            _B02_S_AVG_data.ITEM_VALUE = B02_S.Average().ToString("#0.###");

            //OOS
            lspec_count = 0; uspec_count = 0;
            if (Spcecheck["B02_S"].Item1 != -999999 && Spcecheck["B02_S"].Item1 != 999999)
                lspec_count = B02_S.Count(p => p < Spcecheck["B02_S"].Item1);

            if (Spcecheck["B02_S"].Item2 != -999999 && Spcecheck["B02_S"].Item2 != 999999)
                uspec_count = B02_S.Count(p => p > Spcecheck["B02_S"].Item2);

            OOS_count = lspec_count + uspec_count;
            EDC_Report.IARYClass _B02_S_OOS_data = EDC.CreateIARYClass();
            _B02_S_OOS_data.ITEM_NAME = "B02_S_OOS";
            _B02_S_OOS_data.ITEM_TYPE = "X";
            TempStr = OOS_count.ToString();
            _B02_S_OOS_data.ITEM_VALUE = TempStr;
            
            B02_S.Clear();

            //=======================================================
            //15. --------  B03 data  MAX AVG OOS------------

            EDC_Report.IARYClass _B03_MAX_data = EDC.CreateIARYClass();
            _B03_MAX_data.ITEM_NAME = "B03_MAX";
            _B03_MAX_data.ITEM_TYPE = "X";
            TempStr = B03.Max().ToString("#0.######");
            if (TempStr.Length > 16)
                _B03_MAX_data.ITEM_VALUE = TempStr.Substring(0, 16);
            else
                _B03_MAX_data.ITEM_VALUE = TempStr;
            
            EDC_Report.IARYClass _B03_AVG_data = EDC.CreateIARYClass();
            _B03_AVG_data.ITEM_NAME = "B03_AVG";
            _B03_AVG_data.ITEM_TYPE = "X";
            TempStr = B03.Average().ToString("#0.######");
            if (TempStr.Length > 16)
                _B03_AVG_data.ITEM_VALUE = TempStr.Substring(0, 16);
            else
                _B03_AVG_data.ITEM_VALUE = TempStr;

            //OOS
            lspec_count = 0; uspec_count = 0;
            if (Spcecheck["B03"].Item1 != -999999 && Spcecheck["B03"].Item1 != 999999)
                lspec_count = B03.Count(p => p < Spcecheck["B03"].Item1);

            if (Spcecheck["B03"].Item2 != -999999 && Spcecheck["B03"].Item2 != 999999)
                uspec_count = B03.Count(p => p > Spcecheck["B03"].Item2);

            OOS_count = lspec_count + uspec_count;
            EDC_Report.IARYClass _B03_OOS_data = EDC.CreateIARYClass();
            _B03_OOS_data.ITEM_NAME = "B03_OOS";
            _B03_OOS_data.ITEM_TYPE = "X";
            TempStr = OOS_count.ToString();
            _B03_OOS_data.ITEM_VALUE = TempStr;
           
            B03.Clear();

            //=======================================================
            //16. --------  B03_S data  MAX AVG OOS------------

            EDC_Report.IARYClass _B03_S_MAX_data = EDC.CreateIARYClass();
            _B03_S_MAX_data.ITEM_NAME = "B03_S_MAX";
            _B03_S_MAX_data.ITEM_TYPE = "X";
            _B03_S_MAX_data.ITEM_VALUE = B03_S.Max().ToString("#0.###");
            EDC_Report.IARYClass _B03_S_AVG_data = EDC.CreateIARYClass();
            _B03_S_AVG_data.ITEM_NAME = "B03_S_AVG";
            _B03_S_AVG_data.ITEM_TYPE = "X";
            _B03_S_AVG_data.ITEM_VALUE = B03_S.Average().ToString("#0.###");

            //OOS
            lspec_count = 0; uspec_count = 0;
            if (Spcecheck["B03_S"].Item1 != -999999 && Spcecheck["B03_S"].Item1 != 999999)
                lspec_count = B03_S.Count(p => p < Spcecheck["B03_S"].Item1);

            if (Spcecheck["B03_S"].Item2 != -999999 && Spcecheck["B03_S"].Item2 != 999999)
                uspec_count = B03_S.Count(p => p > Spcecheck["B03_S"].Item2);

            OOS_count = lspec_count + uspec_count;
            EDC_Report.IARYClass _B03_S_OOS_data = EDC.CreateIARYClass();
            _B03_S_OOS_data.ITEM_NAME = "B03_S_OOS";
            _B03_S_OOS_data.ITEM_TYPE = "X";
            TempStr = OOS_count.ToString();
            _B03_S_OOS_data.ITEM_VALUE = TempStr;
            
            B03_S.Clear();

            //=======================================================
            //17. --------  B04 data  MAX AVG OOS------------

            EDC_Report.IARYClass _B04_MAX_data = EDC.CreateIARYClass();
            _B04_MAX_data.ITEM_NAME = "B04_MAX";
            _B04_MAX_data.ITEM_TYPE = "X";
            TempStr = B04.Max().ToString("#0.######");
            if (TempStr.Length > 16)
                _B04_MAX_data.ITEM_VALUE = TempStr.Substring(0, 16);
            else
                _B04_MAX_data.ITEM_VALUE = TempStr;

            EDC_Report.IARYClass _B04_AVG_data = EDC.CreateIARYClass();
            _B04_AVG_data.ITEM_NAME = "B04_AVG";
            _B04_AVG_data.ITEM_TYPE = "X";
            TempStr = B04.Average().ToString("#0.######");
            if (TempStr.Length > 16)
                _B04_AVG_data.ITEM_VALUE = TempStr.Substring(0, 16);
            else
                _B04_AVG_data.ITEM_VALUE = TempStr;

            //OOS
            lspec_count = 0; uspec_count = 0;
            if (Spcecheck["B04"].Item1 != -999999 && Spcecheck["B04"].Item1 != 999999)
                lspec_count = B04.Count(p => p < Spcecheck["B04"].Item1);

            if (Spcecheck["B04"].Item2 != -999999 && Spcecheck["B04"].Item2 != 999999)
                uspec_count = B04.Count(p => p > Spcecheck["B04"].Item2);

            OOS_count = lspec_count + uspec_count;
            EDC_Report.IARYClass _B04_OOS_data = EDC.CreateIARYClass();
            _B04_OOS_data.ITEM_NAME = "B04_OOS";
            _B04_OOS_data.ITEM_TYPE = "X";
            TempStr = OOS_count.ToString();
            _B04_OOS_data.ITEM_VALUE = TempStr;

            B04.Clear();

            //=======================================================
            //18. --------  B04_S data  MAX AVG OOS------------

            EDC_Report.IARYClass _B04_S_MAX_data = EDC.CreateIARYClass();
            _B04_S_MAX_data.ITEM_NAME = "B04_S_MAX";
            _B04_S_MAX_data.ITEM_TYPE = "X";
            _B04_S_MAX_data.ITEM_VALUE = B04_S.Max().ToString("#0.###");
            EDC_Report.IARYClass _B04_S_AVG_data = EDC.CreateIARYClass();
            _B04_S_AVG_data.ITEM_NAME = "B04_S_AVG";
            _B04_S_AVG_data.ITEM_TYPE = "X";
            _B04_S_AVG_data.ITEM_VALUE = B04_S.Average().ToString("#0.###");

            //OOS
            lspec_count = 0; uspec_count = 0;
            if (Spcecheck["B04_S"].Item1 != -999999 && Spcecheck["B04_S"].Item1 != 999999)
                lspec_count = B04_S.Count(p => p < Spcecheck["B04_S"].Item1);

            if (Spcecheck["B04_S"].Item2 != -999999 && Spcecheck["B04_S"].Item2 != 999999)
                uspec_count = B04_S.Count(p => p > Spcecheck["B04_S"].Item2);

            OOS_count = lspec_count + uspec_count;
            EDC_Report.IARYClass _B04_S_OOS_data = EDC.CreateIARYClass();
            _B04_S_OOS_data.ITEM_NAME = "B04_S_OOS";
            _B04_S_OOS_data.ITEM_TYPE = "X";
            TempStr = OOS_count.ToString();
            _B04_S_OOS_data.ITEM_VALUE = TempStr;
            
            B04_S.Clear();

            //=======================================================
            //19. --------  B05 data  MAX AVG OOS------------

            EDC_Report.IARYClass _B05_MAX_data = EDC.CreateIARYClass();
            _B05_MAX_data.ITEM_NAME = "B05_MAX";
            _B05_MAX_data.ITEM_TYPE = "X";
            TempStr = B05.Max().ToString("#0.######");
            if (TempStr.Length > 16)
                _B05_MAX_data.ITEM_VALUE = TempStr.Substring(0, 16);
            else
                _B05_MAX_data.ITEM_VALUE = TempStr;

            EDC_Report.IARYClass _B05_AVG_data = EDC.CreateIARYClass();
            _B05_AVG_data.ITEM_NAME = "B05_AVG";
            _B05_AVG_data.ITEM_TYPE = "X";
            TempStr = B05.Average().ToString("#0.######");
            if (TempStr.Length > 16)
                _B05_AVG_data.ITEM_VALUE = TempStr.Substring(0, 16);
            else
                _B05_AVG_data.ITEM_VALUE = TempStr;

            //OOS
            lspec_count = 0; uspec_count = 0;
            if (Spcecheck["B05"].Item1 != -999999 && Spcecheck["B05"].Item1 != 999999)
                lspec_count = B05.Count(p => p < Spcecheck["B05"].Item1);

            if (Spcecheck["B05"].Item2 != -999999 && Spcecheck["B05"].Item2 != 999999)
                uspec_count = B05.Count(p => p > Spcecheck["B05"].Item2);

            OOS_count = lspec_count + uspec_count;
            EDC_Report.IARYClass _B05_OOS_data = EDC.CreateIARYClass();
            _B05_OOS_data.ITEM_NAME = "B05_OOS";
            _B05_OOS_data.ITEM_TYPE = "X";
            TempStr = OOS_count.ToString();
            _B05_OOS_data.ITEM_VALUE = TempStr;
            
            B05.Clear();

            //=======================================================
            //20. --------  B05_S data  MAX AVG OOS------------

            EDC_Report.IARYClass _B05_S_MAX_data = EDC.CreateIARYClass();
            _B05_S_MAX_data.ITEM_NAME = "B05_S_MAX";
            _B05_S_MAX_data.ITEM_TYPE = "X";
            _B05_S_MAX_data.ITEM_VALUE = B05_S.Max().ToString("#0.###");
            EDC_Report.IARYClass _B05_S_AVG_data = EDC.CreateIARYClass();
            _B05_S_AVG_data.ITEM_NAME = "B05_S_AVG";
            _B05_S_AVG_data.ITEM_TYPE = "X";
            _B05_S_AVG_data.ITEM_VALUE = B05_S.Average().ToString("#0.###");

            //OOS
            lspec_count = 0; uspec_count = 0;
            if (Spcecheck["B05_S"].Item1 != -999999 && Spcecheck["B05_S"].Item1 != 999999)
                lspec_count = B05_S.Count(p => p < Spcecheck["B05_S"].Item1);

            if (Spcecheck["B05_S"].Item2 != -999999 && Spcecheck["B05_S"].Item2 != 999999)
                uspec_count = B05_S.Count(p => p > Spcecheck["B05_S"].Item2);

            OOS_count = lspec_count + uspec_count;
            EDC_Report.IARYClass _B05_S_OOS_data = EDC.CreateIARYClass();
            _B05_S_OOS_data.ITEM_NAME = "B05_S_OOS";
            _B05_S_OOS_data.ITEM_TYPE = "X";
            TempStr = OOS_count.ToString();
            _B05_S_OOS_data.ITEM_VALUE = TempStr;
            
            B05_S.Clear();

            //=======================================================
            //21. --------  B06 data  MAX AVG OOS------------

            EDC_Report.IARYClass _B06_MAX_data = EDC.CreateIARYClass();
            _B06_MAX_data.ITEM_NAME = "B06_MAX";
            _B06_MAX_data.ITEM_TYPE = "X";
            TempStr = B06.Max().ToString("#0.######");
            if (TempStr.Length > 16)
                _B06_MAX_data.ITEM_VALUE = TempStr.Substring(0, 16);
            else
                _B06_MAX_data.ITEM_VALUE = TempStr;

            EDC_Report.IARYClass _B06_AVG_data = EDC.CreateIARYClass();
            _B06_AVG_data.ITEM_NAME = "B06_AVG";
            _B06_AVG_data.ITEM_TYPE = "X";
            TempStr = B06.Average().ToString("#0.######");
            if (TempStr.Length > 16)
                _B06_AVG_data.ITEM_VALUE = TempStr.Substring(0, 16);
            else
                _B06_AVG_data.ITEM_VALUE = TempStr;

            //OOS
            lspec_count = 0; uspec_count = 0;
            if (Spcecheck["B06"].Item1 != -999999 && Spcecheck["B06"].Item1 != 999999)
                lspec_count = B06.Count(p => p < Spcecheck["B06"].Item1);

            if (Spcecheck["B06"].Item2 != -999999 && Spcecheck["B06"].Item2 != 999999)
                uspec_count = B06.Count(p => p > Spcecheck["B06"].Item2);

            OOS_count = lspec_count + uspec_count;
            EDC_Report.IARYClass _B06_OOS_data = EDC.CreateIARYClass();
            _B06_OOS_data.ITEM_NAME = "B06_OOS";
            _B06_OOS_data.ITEM_TYPE = "X";
            TempStr = OOS_count.ToString();
            _B06_OOS_data.ITEM_VALUE = TempStr;
            
            B06.Clear();

            //=======================================================
            //22. --------  B06_S data  MAX AVG OOS------------

            EDC_Report.IARYClass _B06_S_MAX_data = EDC.CreateIARYClass();
            _B06_S_MAX_data.ITEM_NAME = "B06_S_MAX";
            _B06_S_MAX_data.ITEM_TYPE = "X";
            _B06_S_MAX_data.ITEM_VALUE = B06_S.Max().ToString("#0.###");
            EDC_Report.IARYClass _B06_S_AVG_data = EDC.CreateIARYClass();
            _B06_S_AVG_data.ITEM_NAME = "B06_S_AVG";
            _B06_S_AVG_data.ITEM_TYPE = "X";
            _B06_S_AVG_data.ITEM_VALUE = B06_S.Average().ToString("#0.###");

            //OOS
            lspec_count = 0; uspec_count = 0;
            if (Spcecheck["B06_S"].Item1 != -999999 && Spcecheck["B06_S"].Item1 != 999999)
                lspec_count = B06_S.Count(p => p < Spcecheck["B06_S"].Item1);

            if (Spcecheck["B06_S"].Item2 != -999999 && Spcecheck["B06_S"].Item2 != 999999)
                uspec_count = B06_S.Count(p => p > Spcecheck["B06_S"].Item2);

            OOS_count = lspec_count + uspec_count;
            EDC_Report.IARYClass _B06_S_OOS_data = EDC.CreateIARYClass();
            _B06_S_OOS_data.ITEM_NAME = "B06_S_OOS";
            _B06_S_OOS_data.ITEM_TYPE = "X";
            TempStr = OOS_count.ToString();
            _B06_S_OOS_data.ITEM_VALUE = TempStr;
            
            B06_S.Clear();

            //=======================================================
            //23. --------  B07 data  MAX AVG OOS------------

            EDC_Report.IARYClass _B07_MAX_data = EDC.CreateIARYClass();
            _B07_MAX_data.ITEM_NAME = "B07_MAX";
            _B07_MAX_data.ITEM_TYPE = "X";
            TempStr = B07.Max().ToString("#0.######");
            if (TempStr.Length > 16)
                _B07_MAX_data.ITEM_VALUE = TempStr.Substring(0, 16);
            else
                _B07_MAX_data.ITEM_VALUE = TempStr;

            EDC_Report.IARYClass _B07_AVG_data = EDC.CreateIARYClass();
            _B07_AVG_data.ITEM_NAME = "B07_AVG";
            _B07_AVG_data.ITEM_TYPE = "X";
            TempStr = B07.Average().ToString("#0.######");
            if (TempStr.Length > 16)
                _B07_AVG_data.ITEM_VALUE = TempStr.Substring(0, 16);
            else
                _B07_AVG_data.ITEM_VALUE = TempStr;

            //OOS
            lspec_count = 0; uspec_count = 0;
            if (Spcecheck["B07"].Item1 != -999999 && Spcecheck["B07"].Item1 != 999999)
                lspec_count = B07.Count(p => p < Spcecheck["B07"].Item1);

            if (Spcecheck["B07"].Item2 != -999999 && Spcecheck["B07"].Item2 != 999999)
                uspec_count = B07.Count(p => p > Spcecheck["B07"].Item2);

            OOS_count = lspec_count + uspec_count;
            EDC_Report.IARYClass _B07_OOS_data = EDC.CreateIARYClass();
            _B07_OOS_data.ITEM_NAME = "B07_OOS";
            _B07_OOS_data.ITEM_TYPE = "X";
            TempStr = OOS_count.ToString();
            _B07_OOS_data.ITEM_VALUE = TempStr;
            
            B07.Clear();

            //=======================================================
            //24. --------  B07_S data  MAX AVG OOS------------

            //var q = db.Products.Count(p => !p.Discontinued);
            //B07_S.Count(p => p > 10);

            EDC_Report.IARYClass _B07_S_MAX_data = EDC.CreateIARYClass();
            _B07_S_MAX_data.ITEM_NAME = "B07_S_MAX";
            _B07_S_MAX_data.ITEM_TYPE = "X";
            _B07_S_MAX_data.ITEM_VALUE = B07_S.Max().ToString("#0.###");
            EDC_Report.IARYClass _B07_S_AVG_data = EDC.CreateIARYClass();
            _B07_S_AVG_data.ITEM_NAME = "B07_S_AVG";
            _B07_S_AVG_data.ITEM_TYPE = "X";
            _B07_S_AVG_data.ITEM_VALUE = B07_S.Average().ToString("#0.###");

            //OOS
            lspec_count = 0; uspec_count = 0;
            if (Spcecheck["B07_S"].Item1 != -999999 && Spcecheck["B07_S"].Item1 != 999999)
                lspec_count = B07_S.Count(p => p < Spcecheck["B07_S"].Item1);

            if (Spcecheck["B07_S"].Item2 != -999999 && Spcecheck["B07_S"].Item2 != 999999)
                uspec_count = B07_S.Count(p => p > Spcecheck["B07_S"].Item2);

            OOS_count = lspec_count + uspec_count;
            EDC_Report.IARYClass _B07_S_OOS_data = EDC.CreateIARYClass();
            _B07_S_OOS_data.ITEM_NAME = "B07_S_OOS";
            _B07_S_OOS_data.ITEM_TYPE = "X";
            TempStr = OOS_count.ToString();
            _B07_S_OOS_data.ITEM_VALUE = TempStr;

            B07_S.Clear();

            //=======================================================
            //25. --------  B08 data  MAX AVG OOS------------

            EDC_Report.IARYClass _B08_MAX_data = EDC.CreateIARYClass();
            _B08_MAX_data.ITEM_NAME = "B08_MAX";
            _B08_MAX_data.ITEM_TYPE = "X";
            TempStr = B08.Max().ToString("#0.######");
            if (TempStr.Length > 16)
                _B08_MAX_data.ITEM_VALUE = TempStr.Substring(0, 16);
            else
                _B08_MAX_data.ITEM_VALUE = TempStr;

            EDC_Report.IARYClass _B08_AVG_data = EDC.CreateIARYClass();
            _B08_AVG_data.ITEM_NAME = "B08_AVG";
            _B08_AVG_data.ITEM_TYPE = "X";
            TempStr = B08.Average().ToString("#0.######");
            if (TempStr.Length > 16)
                _B08_AVG_data.ITEM_VALUE = TempStr.Substring(0, 16);
            else
                _B08_AVG_data.ITEM_VALUE = TempStr;
            //--------------------------------------------------
            lspec_count = 0; uspec_count = 0;
            if (Spcecheck["B08"].Item1 != -999999 && Spcecheck["B08"].Item1 != 999999)
                lspec_count = B08.Count(p => p < Spcecheck["B08"].Item1);

            if (Spcecheck["B08"].Item2 != -999999 && Spcecheck["B08"].Item2 != 999999)
                uspec_count = B08.Count(p => p > Spcecheck["B08"].Item2);

            OOS_count = lspec_count + uspec_count;
            EDC_Report.IARYClass _B08_OOS_data = EDC.CreateIARYClass();
            _B08_OOS_data.ITEM_NAME = "B08_OOS";
            _B08_OOS_data.ITEM_TYPE = "X";
            TempStr = OOS_count.ToString();
            _B08_OOS_data.ITEM_VALUE = TempStr;
            
            B08.Clear();

            //=======================================================
            //26. --------  B08_S data  MAX AVG OOS------------

            EDC_Report.IARYClass _B08_S_MAX_data = EDC.CreateIARYClass();
            _B08_S_MAX_data.ITEM_NAME = "B08_S_MAX";
            _B08_S_MAX_data.ITEM_TYPE = "X";
            _B08_S_MAX_data.ITEM_VALUE = B08_S.Max().ToString("#0.###");
            EDC_Report.IARYClass _B08_S_AVG_data = EDC.CreateIARYClass();
            _B08_S_AVG_data.ITEM_NAME = "B08_S_AVG";
            _B08_S_AVG_data.ITEM_TYPE = "X";
            _B08_S_AVG_data.ITEM_VALUE = B08_S.Average().ToString("#0.###");

            //OOS
            lspec_count = 0; uspec_count = 0;
            if (Spcecheck["B08_S"].Item1 != -999999 && Spcecheck["B08_S"].Item1 != 999999)
                lspec_count = B08_S.Count(p => p < Spcecheck["B08_S"].Item1);

            if (Spcecheck["B08_S"].Item2 != -999999 && Spcecheck["B08_S"].Item2 != 999999)
                uspec_count = B08_S.Count(p => p > Spcecheck["B08_S"].Item2);

            OOS_count = lspec_count + uspec_count;
            EDC_Report.IARYClass _B08_S_OOS_data = EDC.CreateIARYClass();
            _B08_S_OOS_data.ITEM_NAME = "B08_S_OOS";
            _B08_S_OOS_data.ITEM_TYPE = "X";
            TempStr = OOS_count.ToString();
            _B08_S_OOS_data.ITEM_VALUE = TempStr;
            
            B08_S.Clear();

            //=======================================================
            //27. --------  B09 data  MAX AVG OOS------------

            EDC_Report.IARYClass _B09_MAX_data = EDC.CreateIARYClass();
            _B09_MAX_data.ITEM_NAME = "B09_MAX";
            _B09_MAX_data.ITEM_TYPE = "X";
            TempStr = B09.Max().ToString("#0.######");
            if (TempStr.Length > 16)
                _B09_MAX_data.ITEM_VALUE = TempStr.Substring(0, 16);
            else
                _B09_MAX_data.ITEM_VALUE = TempStr;

            EDC_Report.IARYClass _B09_AVG_data = EDC.CreateIARYClass();
            _B09_AVG_data.ITEM_NAME = "B09_AVG";
            _B09_AVG_data.ITEM_TYPE = "X";
            TempStr = B09.Average().ToString("#0.######");
            if (TempStr.Length > 16)
                _B09_AVG_data.ITEM_VALUE = TempStr.Substring(0, 16);
            else
                _B09_AVG_data.ITEM_VALUE = TempStr;

            //OOS
            lspec_count = 0; uspec_count = 0;
            if (Spcecheck["B09"].Item1 != -999999 && Spcecheck["B09"].Item1 != 999999)
                lspec_count = B09.Count(p => p < Spcecheck["B09"].Item1);

            if (Spcecheck["B09"].Item2 != -999999 && Spcecheck["B09"].Item2 != 999999)
                uspec_count = B09.Count(p => p > Spcecheck["B09"].Item2);

            OOS_count = lspec_count + uspec_count;
            EDC_Report.IARYClass _B09_OOS_data = EDC.CreateIARYClass();
            _B09_OOS_data.ITEM_NAME = "B09_OOS";
            _B09_OOS_data.ITEM_TYPE = "X";
            TempStr = OOS_count.ToString();
            _B09_OOS_data.ITEM_VALUE = TempStr;
            
            B09.Clear();

            //=======================================================
            //28. --------  B09_S data  MAX AVG OOS------------

            EDC_Report.IARYClass _B09_S_MAX_data = EDC.CreateIARYClass();
            _B09_S_MAX_data.ITEM_NAME = "B09_S_MAX";
            _B09_S_MAX_data.ITEM_TYPE = "X";
            _B09_S_MAX_data.ITEM_VALUE = B09_S.Max().ToString("#0.###");
            EDC_Report.IARYClass _B09_S_AVG_data = EDC.CreateIARYClass();
            _B09_S_AVG_data.ITEM_NAME = "B09_S_AVG";
            _B09_S_AVG_data.ITEM_TYPE = "X";
            _B09_S_AVG_data.ITEM_VALUE = B09_S.Average().ToString("#0.###");

            //OOS
            lspec_count = 0; uspec_count = 0;
            if (Spcecheck["B09_S"].Item1 != -999999 && Spcecheck["B09_S"].Item1 != 999999)
                lspec_count = B09_S.Count(p => p < Spcecheck["B09_S"].Item1);

            if (Spcecheck["B09_S"].Item2 != -999999 && Spcecheck["B09_S"].Item2 != 999999)
                uspec_count = B09_S.Count(p => p > Spcecheck["B09_S"].Item2);

            OOS_count = lspec_count + uspec_count;
            EDC_Report.IARYClass _B09_S_OOS_data = EDC.CreateIARYClass();
            _B09_S_OOS_data.ITEM_NAME = "B09_S_OOS";
            _B09_S_OOS_data.ITEM_TYPE = "X";
            TempStr = OOS_count.ToString();
            _B09_S_OOS_data.ITEM_VALUE = TempStr;
            
            B09_S.Clear();

            //=======================================================
            //29. --------  B010 data  MAX AVG OOS------------

            EDC_Report.IARYClass _B10_MAX_data = EDC.CreateIARYClass();
            _B10_MAX_data.ITEM_NAME = "B10_MAX";
            _B10_MAX_data.ITEM_TYPE = "X";
            TempStr = B10.Max().ToString("#0.######");
            if (TempStr.Length > 16)
                _B10_MAX_data.ITEM_VALUE = TempStr.Substring(0, 16);
            else
                _B10_MAX_data.ITEM_VALUE = TempStr;

            EDC_Report.IARYClass _B10_AVG_data = EDC.CreateIARYClass();
            _B10_AVG_data.ITEM_NAME = "B10_AVG";
            _B10_AVG_data.ITEM_TYPE = "X";
            TempStr = B10.Average().ToString("#0.######");
            if (TempStr.Length > 16)
                _B10_AVG_data.ITEM_VALUE = TempStr.Substring(0, 16);
            else
                _B10_AVG_data.ITEM_VALUE = TempStr;

            //OOS
            lspec_count = 0; uspec_count = 0;
            if (Spcecheck["B10"].Item1 != -999999 && Spcecheck["B10"].Item1 != 999999)
                lspec_count = B10.Count(p => p < Spcecheck["B10"].Item1);

            if (Spcecheck["B10"].Item2 != -999999 && Spcecheck["B10"].Item2 != 999999)
                uspec_count = B10.Count(p => p > Spcecheck["B10"].Item2);

            OOS_count = lspec_count + uspec_count;
            EDC_Report.IARYClass _B10_OOS_data = EDC.CreateIARYClass();
            _B10_OOS_data.ITEM_NAME = "B10_OOS";
            _B10_OOS_data.ITEM_TYPE = "X";
            TempStr = OOS_count.ToString();
            _B10_OOS_data.ITEM_VALUE = TempStr;

            B10.Clear();

            //=======================================================
            //30. --------  B010_S data  MAX AVG OOS------------

            EDC_Report.IARYClass _B10_S_MAX_data = EDC.CreateIARYClass();
            _B10_S_MAX_data.ITEM_NAME = "B10_S_MAX";
            _B10_S_MAX_data.ITEM_TYPE = "X";
            _B10_S_MAX_data.ITEM_VALUE = B10_S.Max().ToString("#0.###");
            EDC_Report.IARYClass _B10_S_AVG_data = EDC.CreateIARYClass();
            _B10_S_AVG_data.ITEM_NAME = "B10_S_AVG";
            _B10_S_AVG_data.ITEM_TYPE = "X";
            _B10_S_AVG_data.ITEM_VALUE = B10_S.Average().ToString("#0.###");

            //OOS
            lspec_count = 0; uspec_count = 0;
            if (Spcecheck["B10_S"].Item1 != -999999 && Spcecheck["B10_S"].Item1 != 999999)
                lspec_count = B10_S.Count(p => p < Spcecheck["B10_S"].Item1);

            if (Spcecheck["B10_S"].Item2 != -999999 && Spcecheck["B10_S"].Item2 != 999999)
                uspec_count = B10_S.Count(p => p > Spcecheck["B10_S"].Item2);

            OOS_count = lspec_count + uspec_count;
            EDC_Report.IARYClass _B10_S_OOS_data = EDC.CreateIARYClass();
            _B10_S_OOS_data.ITEM_NAME = "B10_S_OOS";
            _B10_S_OOS_data.ITEM_TYPE = "X";
            TempStr = OOS_count.ToString();
            _B10_S_OOS_data.ITEM_VALUE = TempStr;
            
            B10_S.Clear();
             
            IOT_DEVICE Device = null;
            Program._IOT_Device.TryGetValue(_PointID, out Device);
            if (Device == null)
            {
                Console.WriteLine("Device Not exist  so Skip This Process Device", _PointID);
                Program.log.DebugFormat("[{0}] {2}-({1})", "Program", MethodBase.GetCurrentMethod().Name, "Device Not Exist so Skip This Process Device"+ _PointID.ToString());
                EDC_Path = string.Empty;
                return string.Empty;
            }

            IOT_EDC_GLS_INFO EDC_Gls_Info = null;
            Program._IOT_EDC_GlassInfo.TryGetValue(Device.sub_eqp_id , out EDC_Gls_Info);
            if (EDC_Gls_Info == null)
            {
            Console.WriteLine("Glass_Info not Exist so Skip This Process Device "+ Device.sub_eqp_id.ToString());
            Program.log.DebugFormat("[{0}] {2}-({1})", "Program", MethodBase.GetCurrentMethod().Name, "Glass_Info not Exist so Skip This Process Device"+ Device.sub_eqp_id);
            EDC_Path = string.Empty;
            return string.Empty;
            }
             
            EDC.Update_EDC_Header(EDC_Gls_Info);
            string EDC_Path_Base = string.Empty;


            // avoid user setting below 6 character
            if (EDC_Gls_Info.eqp_id.Length > 6)
                EDC_Path_Base = EDC_Gls_Info.eqp_id.Substring(0, 6) + "00";
            else
                EDC_Path_Base = EDC_Gls_Info.eqp_id;

            string EDC_Path_tail = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + EDC_Gls_Info.eqp_id + "_" + EDC_Gls_Info.glass_id + ".xml";
            EDC_Path = Path.Combine(EDC_Path_Base, EDC_Path_tail);

            Return_EDC_String = EDC.Report_EDC();
            return Return_EDC_String;

        }

    }

    public class IOT_Alert
    {
        public string device_id { get; set; }
        public string alert_code { get; set; }
        public string alert_msg { get; set; }
        public string alert_level { get; set; }
        public string alert_time { get; set; }
        public string rpt_user { get; set; }
    }

    public class IOT_Status
    {
        public string device_id { get; set; }
        public string device_type { get; set; }
        public string device_status { get; set; }
        public string report_time { get; set; }
    }

    public class IOT_Register
    {
        public string device_id { get; set; }
        
    }

}
