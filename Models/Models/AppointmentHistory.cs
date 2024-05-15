using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.Models
{
    public class AppointmentHistory
    {
        public int AppointmentHistoryId { get; set; }
        public string AppointmentId { get; set; }
        public DateTime ChangeDate { get; set; }
        public string ChangeType { get; set; }
        public string Changes { get; set; }

        [JsonIgnore]
        public Appointment OriginalAppointment { get; set; }

    }
}
