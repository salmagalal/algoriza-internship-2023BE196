using DomainLayer.DTOs;
using RepositoryLayer.IRepository;
using ServiceLayer.ICustomServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.CustomsServices
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repository;

        public PatientService(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<PatientDetails>> GetAllPatientsAsync(int pageNumber, int pageSize, string search)
        {
            return await _repository.GetAllPatients(pageNumber, pageSize, search);
        }
    }
}
