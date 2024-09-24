using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.DataModel;
using BusinessLayer.Interfaces;
using DataLayer.IRepository;
using DataLayer.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using DataLayer;

namespace BusinessLayer.Services
{
    public class BankInformationService : IBankInformationService
    {
        //private readonly IRepository<User> _userRepository;
        private readonly IRepository<BankInformation> _repository;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;

        public BankInformationService(Repository<BankInformation> repository, IUserService userService, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context) {
            _repository = repository;
            _userService = userService;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _context = context;

        }

        public async Task<BankInformation> GetUserBankInformations(int userId)
        {
            var userBankInfo = await _repository.GetUserBankInformations(userId);
            if (userBankInfo!=null) { 
                
                return userBankInfo;
            }
            return null;
        }
    }
}
