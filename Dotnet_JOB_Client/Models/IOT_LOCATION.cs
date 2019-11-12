using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace YohoIoT2018.Models
{
    public class IOT_LOCATION
    {
        public int id { get; set; }
        [DisplayName("位置")]
        public string location { get; set; }
        [DisplayName("描述")]
        public string location_desc { get; set; }
        [DisplayName("修改時間")]
        public DateTime clm_date_time { get; set; }
        [DisplayName("修改者")]
        public string clm_user { get; set; }
    }
}
