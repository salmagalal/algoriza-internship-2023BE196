﻿using DomainLayer.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTOs
{
    public class UpdateDoctor
    {
        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        [RegularExpression(@"^01[0125]\d{8}$", ErrorMessage = "Invalid Phone Number.")]
        public string? PhoneNumber { get; set; }

        public Gender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public byte[]? Image { get; set; }

        public string? Specialization { get; set; }
    }
}
