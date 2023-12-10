
using DomainLayer.DTOs;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.IRepository
{
    public interface IDoctorRepository : IBaseRepository<DomainLayer.Models.Doctor>
    {
        Task AddDoctorWithSpecializationAsync(DomainLayer.DTOs.Doctor doctor);
        IQueryable<DoctorDetails> GetDoctorById(int Id);
    }
}
