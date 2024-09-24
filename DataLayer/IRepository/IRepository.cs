using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(string email);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task<User> GetByEmpIdAsync(string empId);
        Task DeleteAsync(int id);
        Task<User> AuthenticateAsync(string email, string password);
        Task<User> GetUserByEmailAsync(string email);
        Task<Education> GetEducationDetailsAsync(int userId);
        Task<User> GetLastRegisteredUserAsync();
        Task<User> GetLoggedInUserEmail();
        Task<BankInformation> GetUserBankInformations(int userId);
    }
}
