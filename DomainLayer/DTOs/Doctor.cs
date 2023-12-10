using DomainLayer.Models.Authentication.Register;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTOs
{
    public class Doctor : RegisterUser
    {
        [Required(ErrorMessage ="Image is required.")]
        public new byte[]? Image { get; set; }

        [Required(ErrorMessage = "specialization is required.")]
        public string? Specialization { get; set; }
    }
}
