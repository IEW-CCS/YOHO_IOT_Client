using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;



namespace YohoIoT2018.Models
{
    public class IOT_DEVICE_SPEC
    {
        public int id { get; set; }
        [DisplayName("類型")]
        public string device_type { get; set; }
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
    }
}
