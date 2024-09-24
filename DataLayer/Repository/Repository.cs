using DataLayer.IRepository;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> GetByIdAsync(string email)
        {
            return await _dbSet.FindAsync(email);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByEmpIdAsync(string empId)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.emp_id == empId);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<User> AuthenticateAsync(string email, string password)
        {
            // Example authentication logic
            var user = await _context.Users.FirstOrDefaultAsync(u => u.email == email && u.password == password);
            return user;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            // Query the database to find the user by email
            var user =  await _context.Users
                .SingleOrDefaultAsync(u => u.email.ToLower() == email.ToLower());
            return user;
        }

        public async Task<Education> GetEducationDetailsAsync(int userId)
        {
            // Query the database to find the user by email
            var educationDetails = await _context.Education
                .SingleOrDefaultAsync(u => u.user_id == userId);
            return educationDetails;
        }
        public async Task<User> GetLastRegisteredUserAsync()
        {
            var user = await _context.Users
                .OrderByDescending(u => u.user_id)
                .FirstOrDefaultAsync();
            return user;
        }
        public async Task<ClaimsPrincipal> GetUserClaimsAsync(string userId)
        {
            // Fetch user and create claims
            var user = await _context.Users.FindAsync(userId);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.email)
            };
            var identity = new ClaimsIdentity(claims, "custom");
            var principal = new ClaimsPrincipal(identity);
            return principal;
        }
        public async Task<User> GetLoggedInUserEmail() {

            var userEmail = await _context.Users.FindAsync(ClaimTypes.Email);
            return userEmail;
        }
        public async Task<BankInformation> GetUserBankInformations(int userId)
        {
            //var userBankInfo = await _context.BankInformation.FindAsync(userId);
            var userBankInfo = await _context.BankInformation
    .Where(b => b.user_id == userId) // Ensure you are using the correct column name
    .FirstOrDefaultAsync();
            return userBankInfo;
        }
    }
}
