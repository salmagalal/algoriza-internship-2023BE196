using DomainLayer.DTOs;
using DomainLayer.Models;
using RepositoryLayer.IRepository;
using ServiceLayer.ICustomServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.CustomsServices
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repository;

        public DoctorService(IDoctorRepository repository)
        {
            _repository = repository;
        }

        public async Task AddDoctorAsync(DomainLayer.DTOs.Doctor doctor)
        {
            await _repository.AddDoctorWithSpecializationAsync(doctor);
        }

        public IQueryable<DoctorDetails> GetDoctorById(int Id)
        {
             return _repository.GetDoctorById(Id);
        }

    }
}
