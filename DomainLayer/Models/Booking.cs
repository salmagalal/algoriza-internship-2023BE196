using DomainLayer.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public int AppointmentId { get; set; }

        [ForeignKey("AppointmentId")]
        public Appointment? Appointment { get; set; }

        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient? Patient { get; set; }

        public BookingStatus Status { get; set; }

        public int CouponId { get; set; }

        [ForeignKey("CouponId")]
        public Coupon? Coupon { get; set; }

        public double FinalPrice { get; set; }

        public bool IsConfirmed { get; set; }
    }
}
