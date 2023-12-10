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

        public List<DoctorDetails> GetAllDoctors(int pageNumber, int pageSize, string search)
        {
            return _repository.GetAllDoctors(pageNumber, pageSize, search); 
        }


        public async Task<bool> DeleteDoctorAsync(int id)
        {
            return await _repository.DeleteDoctorById(id);
        }

        public async Task<bool> UpdateDoctorAsync(int id, UpdateDoctor doctor)
        {
            return await _repository.UpdateDoctor(id, doctor);
        }
    }
}
