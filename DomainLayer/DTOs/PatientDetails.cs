using DomainLayer.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTOs
{
    public class PatientDetails
    {
        public string? Email { get; set; }

        public string? FullName { get; set; }

        public string? PhoneNumber { get; set; }

        public Gender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public byte[]? Image { get; set; }
    }
}
