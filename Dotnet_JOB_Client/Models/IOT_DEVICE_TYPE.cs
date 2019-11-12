using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;



namespace YohoIoT2018.Models
{
    public class IOT_DEVICE_TYPE
    {
        public int id { get; set; }
        [DisplayName("類型")]
        public string device_type { get; set; }
        [DisplayName("描述")]
        public string type_desc { get; set; }
        [DisplayName("順序")]
        public int order_seqno { get; set; }
        [DisplayName("色碼")]
        public string color_code { get; set; }
        [DisplayName("ICON")]
        public string material_icon { get; set; }
        [DisplayName("修改時間")]
        public DateTime clm_date_time { get; set; }
        [DisplayName("修改者")]
        public string clm_user { get; set; }

        public List<IOT_DEVICE_TYPE> getDeviceTypeOrderBySeqNO(IOT_DbContext _db)
        {
            var vIoT_DeviceTypes = _db.IOT_DEVICE_TYPE.AsQueryable();
            vIoT_DeviceTypes = vIoT_DeviceTypes.OrderBy(c => c.order_seqno);

            return (vIoT_DeviceTypes.ToList());
        }

        public Boolean isOverTrialPeriod(IOT_DbContext _db)
        {
            var vIoT_DeviceTypes = _db.IOT_DEVICE_TYPE.AsQueryable();
            vIoT_DeviceTypes = vIoT_DeviceTypes.OrderBy(c => c.clm_date_time);

            var oIot_DeviceType = vIoT_DeviceTypes.FirstOrDefault();
            if (oIot_DeviceType != null)
            {
                if (oIot_DeviceType.clm_date_time == null)
                {
                    return (true);
                }
                else
                {
                    DateTime oldDate = oIot_DeviceType.clm_date_time;
                    DateTime newDate = DateTime.Now;
                    // Difference in days, hours, and minutes.
                    TimeSpan ts = newDate - oldDate;
                    // Difference in days.
                    int differenceInDays = ts.Days;
                    if (differenceInDays > 30)
                    {
                        return (true);
                    }
                }
            }

            return (false);
        }
    }
}
