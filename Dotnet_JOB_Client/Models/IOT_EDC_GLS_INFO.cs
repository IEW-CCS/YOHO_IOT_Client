using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace YohoIoT2018.Models
{
    public class IOT_EDC_GLS_INFO
    {
        public int id { get; set; }
        [DisplayName("類型")]
        public string device_type { get; set; }
        [DisplayName("EQP ID")]
        public string eqp_id { get; set; }
        [DisplayName("Sub EQP ID")]
        public string sub_eqp_id { get; set; }
        [DisplayName("Glass ID")]
        public string glass_id { get; set; }
        [DisplayName("Group ID")]
        public string group_id { get; set; }
        [DisplayName("Lot ID")]
        public string lot_id { get; set; }
        [DisplayName("Product ID")]
        public string product_id { get; set; }
        [DisplayName("PFCD")]
        public string pfcd { get; set; }
        [DisplayName("EC Code")]
        public string ec_code { get; set; }
        [DisplayName("Route NO")]
        public string route_no { get; set; }
        [DisplayName("Route Ver")]
        public string route_version { get; set; }
        [DisplayName("Owner")]
        public string owner { get; set; }
        [DisplayName("Recipe ID")]
        public string recipe_id { get; set; }
        [DisplayName("Operation")]
        public string operation { get; set; }
        [DisplayName("OPE ID")]
        public string ope_id { get; set; }
        [DisplayName("OPE NO")]
        public string ope_no { get; set; }
        [DisplayName("PROC ID")]
        public string proc_id { get; set; }
        [DisplayName("RTC Flag")]
        public string rtc_flag { get; set; }
        [DisplayName("PNP")]
        public string pnp { get; set; }
        [DisplayName("Chamber")]
        public string chamber { get; set; }
        [DisplayName("CASET ID")]
        public string cassette_id { get; set; }
        [DisplayName("MES Link Key")]
        public string mes_link_key { get; set; }
        [DisplayName("修改時間")]
        public DateTime clm_date_time { get; set; }
        [DisplayName("修改者")]
        public string clm_user { get; set; }
    }
}

