using DomainLayer.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Time
    {
        public int Id { get; set; }

        public Day Name { get; set; }

        [NotMapped]
        public DateTime AvailableSlot { get; set; }

        public string? TimeInHours
        {
            get { return TimeInHours; }
            set { TimeInHours = AvailableSlot.ToString("t"); }
        }

        public List<Doctor>? Doctors { get; set; }

        public List<DoctorTime>? DoctorTimes { get; set; }
    }
}
