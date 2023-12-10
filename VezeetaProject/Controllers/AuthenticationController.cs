using DomainLayer.Consts;
using DomainLayer.Models;
using DomainLayer.Models.Authentication.Login;
using DomainLayer.Models.Authentication.Register;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer;
using ServiceLayer.ICustomServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace VezeetaWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        public AuthenticationController(UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            IEmailService emailService, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _configuration = configuration;
        }
        

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterUser registerPatient)
        {
            var patientExist = await _userManager.FindByEmailAsync(registerPatient.Email);
            if (patientExist != null) 
            {
                return StatusCode(StatusCodes.Status403Forbidden,
                    new Response { Status = "Error", Message = "User already exists!" });
            }

            ApplicationUser user = new()
            {
                UserName = registerPatient.UserName,
                Email = registerPatient.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = registerPatient.FirstName,
                LastName = registerPatient.LastName,
                PhoneNumber = registerPatient.PhoneNumber,
                DateOfBirth = registerPatient.DateOfBirth,
                Gender = registerPatient.Gender,
                Image = registerPatient.Image
            };

            var result = await _userManager.CreateAsync(user, registerPatient.Password);

            if( !result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "User failed to create." });
            }
     
            await _userManager.AddToRoleAsync(user, ApplicationRoles.Patient);
            return StatusCode(StatusCodes.Status201Created,
                    new Response { Status = "Success", Message = "User created Successfully." });
        }

        [HttpGet]
        public IActionResult TestEmail()
        {
            var message =
                new Message(new string[]
                {"salmaahmedgalal@gmail.com"}, "Test", "<h1>This email from vezeeta to test</h1>");

            _emailService.SendEmail(message);
            return StatusCode(StatusCodes.Status200OK,
                new Response { Status = "Success", Message = "Email Sent Successfully." });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUser loginUser)
        {
            var user = await _userManager.FindByNameAsync(loginUser.UserName);
            if(user!=null && await _userManager.CheckPasswordAsync(user,loginUser.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                var userRoles = await _userManager.GetRolesAsync(user);
                foreach(var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var jwtToken = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    expiration = jwtToken.ValidTo
                });
            }
            return Unauthorized();
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
    }
}
