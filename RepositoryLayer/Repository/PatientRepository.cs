using DomainLayer.Consts;
using DomainLayer.Data;
using DomainLayer.DTOs;
using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using RepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RepositoryLayer.Repository
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PatientRepository(ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager) : base(context)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<PatientDetails>> GetAllPatients(int pageNumber, int pageSize, string search)
        {
            var patients = await _userManager.GetUsersInRoleAsync(ApplicationRoles.Patient);

            IEnumerable<ApplicationUser> patientsFilter;
            if (!string.IsNullOrEmpty(search))
            {
                 patientsFilter = patients.Where(u => u.FullName.Contains(search) || u.Email.Contains(search));
            }
            else
            {
                 patientsFilter = patients.OrderBy(p => p.FullName);
            }


            patients = patientsFilter.Skip((pageNumber - 1) * pageSize)
                               .Take(pageSize)
                               .ToList();

            var patientsDetails = patients.Select(patient => new PatientDetails
            {
                FullName = patient.FullName,
                Email = patient.Email,
                Image = patient.Image,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
                PhoneNumber = patient.PhoneNumber
            }).ToList();

            return patientsDetails;
        }
    }
}
