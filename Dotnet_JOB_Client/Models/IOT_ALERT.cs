using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace YohoIoT2018.Models
{
    public class IOT_ALERT
    {
        public int id { get; set; }
        [DisplayName("Device ID")]
        public string device_id { get; set; }
        [DisplayName("Alert ID")]
        public string alert_id { get; set; }
        [DisplayName("Level")]
        public string alert_lvl { get; set; }
        [DisplayName("Alert Message")]
        public string alert_msg { get; set; }
        [DisplayName("警示時間")]
        public DateTime rpt_date_time { get; set; }
        [DisplayName("警示者")]
        public string rpt_user { get; set; }
        [DisplayName("檢視時間")]
        public DateTime review_date_time { get; set; }
        [DisplayName("檢視者")]
        public string review_user { get; set; }
    }
}
