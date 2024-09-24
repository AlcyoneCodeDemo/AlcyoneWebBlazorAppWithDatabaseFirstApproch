using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interfaces;
using DataLayer.IRepository;
using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using DataLayer.DataModel;

namespace BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Holiday> _holidayRepository;
        private readonly IRepository<BankInformation> _bankInformationRepository;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;
        public UserService(IRepository<User> userRepository, IRepository<Holiday> holidayRepository, IRepository<BankInformation> bankInformationRepository, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _userRepository = userRepository;
            _holidayRepository = holidayRepository;
            _bankInformationRepository = bankInformationRepository;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }
        public async Task<IEnumerable<Holiday>> GetHolidaysAsync() 
        {
            return await _holidayRepository.GetAllAsync();
        }
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }
        public async Task<User> GetUserByIdAsync(string email)
        {
            return await _userRepository.GetByIdAsync(email);
        }
        public async Task AddUserAsync(User user)
        {
            await _userRepository.AddAsync(user);
        }
        public async Task UpdateUserAsync(UserData user)
        {
            if (user.EmpId == null)
            {
                throw new ArgumentException("Employee ID cannot be null");
            }

            // Find the existing user by emp_id
            var existingUser = await _userRepository.GetByEmpIdAsync(user.EmpId);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            // Update the existing user with new values
            existingUser.emp_id = user.EmpId;
            existingUser.status = user.Status;
            existingUser.name = user.Name;
            existingUser.role = user.Role;
            existingUser.contact = user.Contact;
            existingUser.blood_group = user.BloodGroup;

            // Save changes
            await _userRepository.UpdateAsync(existingUser);
        }
        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }
        public async Task<User> AuthenticateAsync(string email, string password)
        {
            // Example authentication logic
            var user = await _userRepository.AuthenticateAsync(email, password);
            return user;
        }
        public async Task<User> GetLastRegisteredUserAsync() { 
            var user = await _userRepository.GetLastRegisteredUserAsync();
            return user;
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            // Ensure the email is not null or empty
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Email cannot be null or empty", nameof(email));

            var user = await _userRepository.GetUserByEmailAsync(email);
            return user;
        }
        public async Task<Education> GetEducationDetailsAsync(int userId)
        {
            // Ensure the email is not null or empty
            if (userId==null)
                throw new ArgumentException("userId cannot be null or empty", nameof(userId));

            var educationDetails = await _userRepository.GetEducationDetailsAsync(userId);
            return educationDetails;
        }
        public string GenerateJwtToken(string email, string userName)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, userName)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public async Task<User> GetLoggedInUserEmail()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var userEmail = httpContext.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (!string.IsNullOrEmpty(userEmail))
            {
                var userFromDb = await _context.Users.FirstOrDefaultAsync(u => u.email == userEmail);
                return userFromDb;
            }

            return null;
        }
        public async Task<BankInformation> GetUserBankInformations(int userId)
        {
            //var userBankInfo = await _context.BankInformation.FirstOrDefaultAsync(u => u.user_id == userId);
            var userBankInfo = await _bankInformationRepository.GetUserBankInformations(userId);
            if (userBankInfo != null)
            {
                return userBankInfo;
            }
            return null;
        }

    }
}
