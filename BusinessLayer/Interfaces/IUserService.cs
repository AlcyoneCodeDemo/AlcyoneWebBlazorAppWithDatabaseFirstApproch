using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.DataModel;

namespace BusinessLayer.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IEnumerable<Holiday>> GetHolidaysAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByIdAsync(string email);
        Task AddUserAsync(User user);
        //Task<bool> UserExistsAsync(string email);  // Add this method
        Task UpdateUserAsync(UserData user);
        Task DeleteUserAsync(int id);
        Task<User> AuthenticateAsync(string email, string password);
        Task<User> GetUserByEmailAsync(string email);
        Task<Education> GetEducationDetailsAsync(int userId);
        Task<User> GetLastRegisteredUserAsync();
        Task<User> GetLoggedInUserEmail();
        string GenerateJwtToken(string email, string userName);
        Task<BankInformation> GetUserBankInformations(int UserId);

    }
}
