using DomainLayer.DTOs;
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
    public class AdminController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        public AdminController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        [HttpGet("patients")]
        public IEnumerable<string> Get()
        {
            return new List<string> {"Salma", "Habiba", "Farida"};
        }

        [HttpPost("AddNewDoctor")]
        public async Task<IActionResult> CreateDoctor(Doctor doctor)
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
        public IActionResult GetDoctor(int  doctorId)
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
    }
}
