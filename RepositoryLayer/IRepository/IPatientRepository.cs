using DomainLayer.DTOs;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.IRepository
{
    public interface IPatientRepository : IBaseRepository<Patient>
    {
        Task<List<PatientDetails>> GetAllPatients(int pageNumber, int pageSize, string search);
    }
}
