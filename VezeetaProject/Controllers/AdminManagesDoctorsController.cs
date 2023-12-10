using DomainLayer.DTOs;
using DomainLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using ServiceLayer.ICustomServices;

namespace VezeetaProject.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminManagesDoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        public AdminManagesDoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpPost("AddNewDoctor")]
        public async Task<IActionResult> CreateDoctor(DomainLayer.DTOs.Doctor doctor)
        {
            if (doctor != null)
            {
                await _doctorService.AddDoctorAsync(doctor);
                return Ok("Created Successfully");
            }
            else
            {
                return BadRequest("Somethingwent wrong");
            }
        }

        [HttpGet("GetDoctorById")]
        public IActionResult GetDoctor(int doctorId)
        {
            try
            {
                var doctorDetails = _doctorService.GetDoctorById(doctorId).ToList();

                if (doctorDetails != null)
                {
                    return Ok(doctorDetails);
                }
                else
                {
                    return NotFound("Doctor not found");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("SearchDoctors")]
        public IActionResult GetAllDoctors(int pageNumber, int pageSize, string? search)
        {
            try
            {
                var doctors = _doctorService.GetAllDoctors(pageNumber, pageSize, search);
                if (doctors.Any())
                {
                    return Ok(doctors);
                }
                else
                {
                    return NotFound("No doctors found");
                }
            }
            catch(Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("DeleteDoctor")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            try
            {
                bool deletionResult = await _doctorService.DeleteDoctorAsync(id);

                if (deletionResult)
                {
                    return Ok("Doctor deleted successfully");
                }
                else
                {
                    return NotFound("Doctor not found");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("EditDoctor")]
        public async Task<IActionResult> UpdateDoctor(int id, [FromBody] UpdateDoctor updatedDoctor)
        {
            try
            {
                bool updateResult = await _doctorService.UpdateDoctorAsync(id, updatedDoctor);

                if (updateResult)
                {
                    return Ok("Doctor updated successfully");
                }
                else
                {
                    return NotFound("Doctor not found");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
