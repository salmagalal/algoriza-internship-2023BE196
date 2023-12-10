using DomainLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ICustomServices
{
    public interface IDoctorService
    {
        Task AddDoctorAsync(Doctor doctor);

        IQueryable<DoctorDetails> GetDoctorById(int Id);
    }
}
