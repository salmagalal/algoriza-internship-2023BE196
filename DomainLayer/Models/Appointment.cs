using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        public double DoctorPrice { get; set; }

        public int DoctorTimeDoctorId { get; set; }

        public int DoctorTimeTimeId { get; set; }

        public DoctorTime? DoctorTime { get; set; }

        public Booking? Booking { get; set; }
    }
}
