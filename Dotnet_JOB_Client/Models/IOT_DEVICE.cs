using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YohoIoT2018.Models
{
    public class IOT_DEVICE
    {
        public int id { get; set; }
        [DisplayName("Device ID")]
        public string device_id { get; set; }
        [DisplayName("描述")]
        public string device_desc { get; set; }
        [DisplayName("類型")]
        public string device_type { get; set; }
        [DisplayName("狀態")]
        public string status { get; set; }
        [DisplayName("位置")]
        public string location { get; set; }
        [DisplayName("數值")]
        public double value { get; set; }
        [DisplayName("控制下限")]
        public double lcl { get; set; }
        [DisplayName("控制上限")]
        public double ucl { get; set; }
        [DisplayName("規格下限")]
        public double lspec { get; set; }
        [DisplayName("規格上限")]
        public double uspec { get; set; }
        [DisplayName("修改時間")]
        public DateTime clm_date_time { get; set; }
        [DisplayName("修改者")]
        public string clm_user { get; set; }
        [DisplayName("上報間隔")]
        public int rpt_interval { get; set; }
        [DisplayName("收集間隔 ID")]
        public int col_interval { get; set; }
        [DisplayName("OOC Flag")]
        public string ooc_flg { get; set; }
        [DisplayName("OOS Flag")]
        public string oos_flg { get; set; }
        [DisplayName("Alarm Flag")]
        public string alarm_flg { get; set; }
        [DisplayName("所屬機台")]
        public string eqp_id { get; set; }
        [DisplayName("所屬子機台")]
        public string sub_eqp_id { get; set; }
        [DisplayName("EDC Table NO")]
        public int device_no { get; set; }

        public IOT_DEVICE() { }
    }
}
