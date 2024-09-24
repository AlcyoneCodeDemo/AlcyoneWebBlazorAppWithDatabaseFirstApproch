using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using DataLayer.Models;

namespace BlazorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankInformationController : ControllerBase
    {
        
        private readonly IUserService _userService;
        public BankInformationController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/BankInformation/GetBankInformations
       // [Authorize]
        [HttpGet("GetBankInformations/{userId}")]
        public async Task<ActionResult<BankInformation>> GetBankInformations(int userId)
        {
            var userBankInformation = await _userService.GetUserBankInformations(userId);
            return Ok(userBankInformation);
        }
    }
}
