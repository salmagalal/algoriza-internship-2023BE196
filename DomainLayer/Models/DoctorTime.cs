using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class DoctorTime
    {
        public int DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public Doctor? Doctor { get; set; }

        public int TimeId { get; set; }

        [ForeignKey("TimeId")]
        public Time? Time { get; set; }

        public bool IsBooked { get; set; }
    }
}
