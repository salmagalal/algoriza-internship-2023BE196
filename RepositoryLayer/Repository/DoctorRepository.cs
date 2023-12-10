using DomainLayer.Consts;
using DomainLayer.Data;
using DomainLayer.DTOs;
using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public List<DoctorDetails> GetAllDoctors(int pageNumber, int pageSize, string search)
        {
            try
            {
                IQueryable<DoctorDetails> doctorDetailsQuery = _context.Doctors.Join(_context.Specializations,
                    d => d.SpecializationId, s => s.Id,
                    (d, s) => new
                    {
                        Doctor = d,
                        Specialization = s
                    })
                    .Join(_context.ApplicationUsers,
                    j => j.Doctor.UserId, a => a.Id,
                    (j, a) => new DoctorDetails
                    {
                        Image = a.Image,
                        FullName = a.FullName,
                        Email = a.Email,
                        PhoneNumber = a.PhoneNumber,
                        Specialization = j.Specialization.Name,
                        Gender = a.Gender,
                        DateOfBirth = a.DateOfBirth,
                    });

                if (!string.IsNullOrEmpty(search))
                {
                    doctorDetailsQuery = doctorDetailsQuery.Where(d =>
                    d.FullName.Contains(search) ||
                    d.Specialization.Contains(search) ||
                    d.Email.Contains(search));
                }
                else
                {
                    doctorDetailsQuery = doctorDetailsQuery.OrderBy(d => d.FullName);
                }

                List<DoctorDetails> doctorDetails = doctorDetailsQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

                return doctorDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteDoctorById(int Id)
        {
            try
            {
                DomainLayer.Models.Doctor doctor = GetById(Id);
                if (doctor != null)
                {
                    ApplicationUser user = _context.Users.FirstOrDefault(u => u.Id == doctor.UserId);
                    await _userManager.DeleteAsync(user);
                    Save();

                    return true; 
                }
                return false;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateDoctor(int id, UpdateDoctor updatedDoctor)
        {
            try
            {
                DomainLayer.Models.Doctor existingDoctor = _context.Doctors.Include(d => d.ApplicationUser).FirstOrDefault(d => d.Id == id);
                if (existingDoctor != null)
                {
                    existingDoctor.ApplicationUser.FirstName = updatedDoctor.FirstName ?? existingDoctor.ApplicationUser.FirstName;
                    existingDoctor.ApplicationUser.LastName = updatedDoctor.LastName ?? existingDoctor.ApplicationUser.LastName;
                    existingDoctor.ApplicationUser.UserName = updatedDoctor.UserName ?? existingDoctor.ApplicationUser.UserName;
                    existingDoctor.ApplicationUser.Email = updatedDoctor.Email ?? existingDoctor.ApplicationUser.Email;
                    existingDoctor.ApplicationUser.PhoneNumber = updatedDoctor.PhoneNumber ?? existingDoctor.ApplicationUser.PhoneNumber;
                    if (updatedDoctor.Gender != null)
                    {
                        existingDoctor.ApplicationUser.Gender = updatedDoctor.Gender;
                    }

                    if (updatedDoctor.DateOfBirth != null)
                    {
                        existingDoctor.ApplicationUser.DateOfBirth = updatedDoctor.DateOfBirth;
                    }

                    await SaveAsync();

                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
