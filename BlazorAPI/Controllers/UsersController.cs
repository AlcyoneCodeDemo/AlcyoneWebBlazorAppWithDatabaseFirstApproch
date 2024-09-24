using BusinessLayer.Interfaces;
using DataLayer.DataModel;
using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users/GetAllUser
        [HttpGet("GetAllUser")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // GET: api/Users/GetAllUser
        [HttpGet("GetHolidays")]
        public async Task<ActionResult<IEnumerable<Holiday>>> GetHolidays()
        {
            var holidays = await _userService.GetHolidaysAsync();
            return Ok(holidays);
        }

        // GET: api/Users/GetUsers/5
        [HttpGet("GetUsers/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/UpdateUserProfile
        [HttpPut("UpdateUserProfile")]
        public async Task<ActionResult<UserData>> UpdateUserProfile(UserData user)
        {
            if (user == null)
            {
                return BadRequest("User data is null");
            }
            try
            {
                if (user.Status == "1")
                {
                    user.Status = "Active";
                }
                else if (user.Status == "2")
                {
                    user.Status = "Inactive";
                }
                else {
                    user.Status = "Suspended";
                }
                await _userService.UpdateUserAsync(user);
                return Ok("Updated Successful"); // Return Ok status when the update is successful
            }
            catch (KeyNotFoundException)
            {
                return NotFound("User not found");
            }
            catch (DbUpdateException dbEx)
            {
                // Log the full exception details
                var innerException = dbEx.InnerException?.InnerException?.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database update error: {innerException}");
            }

        }

        // POST: api/Users/Authenticate
        [HttpPost("Authenticate")]
        public async Task<ActionResult<User>> Authenticate(LoginModel loginModel)
        {
            var user = await _userService.AuthenticateAsync(loginModel.Email, loginModel.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(user);
        }

        // GET: api/Users/GetLoggedInUser
        [Authorize]
        [HttpGet("GetLoggedInUser")]
        public async Task<ActionResult<UserData>> GetLoggedInUser()
        {
            var user = await _userService.GetLoggedInUserEmail();
            if (string.IsNullOrEmpty(user.email))
            {
                return Unauthorized();
            }

            var userInformations = await _userService.GetUserByEmailAsync(user.email);
            if (userInformations == null)
            {
                return NotFound();
            }

            var userData = new UserData
            {
                Email = user.email,
                Name = user.name,
                Status = user.status,
                EmpId = user.emp_id,
                BloodGroup = user.blood_group,
                Role = user.role,
                Contact = user.contact,
                EmergencyContact = user.emergency_contact,
                Nationality = user.nationality,
                Address = user.address,
                Zipcode = user.zipcode,
                Religion = user.religion,
                Marital_status = user.marital_status,
                Country = user.country,
                State = user.state,
                user_id = user.user_id,
            };

            return Ok(userData);
        }

        // GET: api/Users/GetLoggedInUser
        [HttpGet("GetEducationDetails/{userId}")]
        public async Task<ActionResult<EducationData>> GetEducationDetails(int userId)
        {
            //var user = await _userService.GetLoggedInUserEmail();
            //if (string.IsNullOrEmpty(user.email))
            //{
            //    return Unauthorized();
            //}

            //var userInformations = await _userService.GetUserByEmailAsync(user.email);
            //if (userInformations == null)
            //{
            //    return NotFound();
            //}

            var educationDetails = await _userService.GetEducationDetailsAsync(userId);

            return Ok(educationDetails);
        }

    }
}
