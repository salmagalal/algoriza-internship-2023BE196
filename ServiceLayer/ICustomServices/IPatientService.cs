using DomainLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ICustomServices
{
    public interface IPatientService
    {
        Task<List<PatientDetails>> GetAllPatientsAsync(int pageNumber, int pageSize, string search);
    }
}
