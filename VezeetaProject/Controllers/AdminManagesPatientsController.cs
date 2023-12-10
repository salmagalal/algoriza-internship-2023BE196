using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.CustomsServices;
using ServiceLayer.ICustomServices;

namespace VezeetaProject.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminManagesPatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public AdminManagesPatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet("SearchPatients")]
        public async Task<IActionResult> GetAllPatients(int pageNumber, int pageSize, string? search)
        {
            try
            {
                var patients =  await _patientService.GetAllPatientsAsync(pageNumber, pageSize, search);
                if (patients.Any())
                {
                    return Ok(patients);
                }
                else
                {
                    return NotFound("No patients found");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
