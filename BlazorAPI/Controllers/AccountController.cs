using BusinessLayer.Interfaces;
using DataLayer.DataModel;
using DataLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        //private readonly string _issuer;
        //private readonly string _audience;
        //private readonly string _secretKey;

        public AccountController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
            //_issuer = configuration["Jwt:Issuer"];
            //_audience = configuration["Jwt:Audience"];
            //_secretKey = configuration["Jwt:SecretKey"];
        }

        // POST: api/Account/Login
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponseModel>> Login([FromBody] LoginModel loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest("Invalid login model");
            }

            var user = await _userService.AuthenticateAsync(loginModel.Email, loginModel.Password);
            if (user == null)
            {
                return NotFound("Invalid credentials");
            }

            var token = _userService.GenerateJwtToken(user.email, user.name);

            var response = new LoginResponseModel
            {
                Email = user.email,
                UserName = user.name,
                Token = token // Include token in response
            };

            return Ok(response);
        }


        // POST: api/Account/Login
        //[HttpPost("Login")]
        //public async Task<ActionResult<LoginResponseModel>> Login([FromBody] LoginModel loginModel)
        //{
        //    var user = await _userService.AuthenticateAsync(loginModel.Email, loginModel.Password);

        //    if (user == null)
        //    {
        //        return NotFound("Invalid credentials");
        //    }
        //    var response = new LoginResponseModel
        //    {
        //        Email = user.email,
        //        UserName = user.name, 
        //    };

        //    return Ok(response);
        //}

        //[HttpPost("login")]
        //public IActionResult Login([FromBody] LoginModel model)
        //{
        //    // Authenticate user (this should be replaced with actual authentication logic)
        //    if (model.Email == "test@example.com" && model.Password == "password")
        //    {
        //        var claims = new[]
        //        {
        //        new Claim(ClaimTypes.Email, model.Email),
        //        new Claim(ClaimTypes.Name, "Test User")
        //    };

        //        var token = new JwtSecurityToken(
        //            issuer: _configuration["Jwt:Issuer"],
        //            audience: _configuration["Jwt:Audience"],
        //            claims: claims,
        //            expires: DateTime.UtcNow.AddHours(1),
        //            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])), SecurityAlgorithms.HmacSha256)
        //        );

        //        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        //    }

        //    return Unauthorized();
        //}

        // POST: api/Account/Register
        [HttpPost("Register")]
        public async Task<ActionResult<RegisterResponseModel>> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if user with the same email already exists
                var existingUser = await _userService.GetUserByEmailAsync(model.Email);
                if (existingUser != null)
                {
                    // Return a specific message if the user already exists
                    ModelState.AddModelError("Email", "Already have an account.");
                    return BadRequest(ModelState);
                }

                if (!string.IsNullOrEmpty(model.EmergencyContactDetails) && !IsValidJson(model.EmergencyContactDetails))
                {
                    return BadRequest("Invalid JSON format for EmergencyContactDetails");
                }

                // Fetch the last emp_id from the database
                var lastUser = await _userService.GetLastRegisteredUserAsync();
                string newEmpId;

                if (lastUser != null && !string.IsNullOrEmpty(lastUser.emp_id))
                {
                    // Parse the last emp_id and increment it
                    long lastEmpId = long.Parse(lastUser.emp_id);
                    newEmpId = (lastEmpId + 1).ToString();
                }
                else
                {
                    // If no users are present, start with the first emp_id
                    newEmpId = "1001001";
                }

                var token = _userService.GenerateJwtToken(model.Email, model.Username);

                var user = new User
                {
                    email = model.Email,
                    password = model.Password,
                    emergency_contact_details = model.EmergencyContactDetails,
                    //emergency_contact_details = "{\"name\": \"Jane Doe\", \"phone\": \"123-456-7890\"}",
                    emp_id = newEmpId,
                    name = model.Username,
                    status = "Active"
                };

                try
                {
                    await _userService.AddUserAsync(user);
                    var response = new RegisterResponseModel
                    {
                        Email = model.Email,
                        UserName = model.Username,
                        Token = token
                    };
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }
            return BadRequest(ModelState);
        }

        private static bool IsValidJson(string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput)) return false;
            strInput = strInput.Trim();
            if (strInput.StartsWith("{") && strInput.EndsWith("}") || // For object
                strInput.StartsWith("[") && strInput.EndsWith("]"))   // For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException)
                {
                    return false;
                }
            }
            return false;
        }


    }
}
