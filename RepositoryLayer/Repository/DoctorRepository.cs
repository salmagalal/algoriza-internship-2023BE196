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

namespace RepositoryLayer.Repository
{
    public class DoctorRepository : BaseRepository<DomainLayer.Models.Doctor>, IDoctorRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DoctorRepository(ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : base(context)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task AddDoctorWithSpecializationAsync(DomainLayer.DTOs.Doctor doctor)
        {
            try
            {
                var FoundSpecialization = _context.Specializations.FirstOrDefault(s => s.Name == doctor.Specialization);
                if (FoundSpecialization != null)
                {
                    ApplicationUser user = new()
                    {
                        UserName = doctor.UserName,
                        Email = doctor.Email,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        FirstName = doctor.FirstName,
                        LastName = doctor.LastName,
                        PhoneNumber = doctor.PhoneNumber,
                        DateOfBirth = doctor.DateOfBirth,
                        Gender = doctor.Gender,
                        Image = doctor.Image
                    };
                    var result = await _userManager.CreateAsync(user, doctor.Password);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, ApplicationRoles.Doctor);
                        await _context.Doctors.AddAsync(
                            new DomainLayer.Models.Doctor 
                            { 
                                UserId = _context.Users.Where(u => u.UserName == doctor.UserName)
                                .Select(u => u.Id).FirstOrDefault(),

                                SpecializationId = _context.Specializations.Where(s => s.Name == doctor.Specialization)
                                .Select(u => u.Id).FirstOrDefault()
                            });
                        await SaveAsync();
                    }
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public IQueryable<DoctorDetails> GetDoctorById(int Id)
        {
           try
            {
                DomainLayer.Models.Doctor doctor = GetById(Id);
                if (doctor != null)
                {
                    var doctorDetails = _context.Doctors.Where(d => d.Id == Id).Join(_context.Specializations,
                        d => d.SpecializationId, s => s.Id,
                        (d, s) => new
                        {
                            Doctor = d,
                            Specialization = s
                        })
                        .Join(_context.ApplicationUsers,
                        j => j.Doctor.UserId, a=> a.Id,
                        (j, a) => new DoctorDetails
                        {
                            Image = a.Image,
                            FullName = a.FullName,
                            Email = a.Email,
                            PhoneNumber = a.PhoneNumber,
                            Specialization = j.Specialization.Name,
                            Gender = a.Gender,
                            DateOfBirth= a.DateOfBirth,
                        });
                    return doctorDetails;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
