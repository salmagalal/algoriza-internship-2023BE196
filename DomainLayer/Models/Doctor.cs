using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Doctor 
    {
        public int Id { get; set; }

        [Required]
        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser? ApplicationUser { get; set; }

        [Required]
        public int SpecializationId { get; set; }

        [ForeignKey("SpecializationId")]
        public Specialization? Specialization { get; set; }

        public List<Time>? Times { get; set; }

        public List<DoctorTime>? DoctorTimes { get; set; }
    }
}
