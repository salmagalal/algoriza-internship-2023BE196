using DomainLayer.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Coupon
    {
        public int Id { get; set; }

        public DiscountType DiscountType { get; set; }

        [Required]
        public string? DiscountCode { get; set; }

        [Required]
        public double Value { get; set; }

        public List<Booking>? Booking { get; set; }

        public bool IsDeactivated { get; set; }

    }
}
