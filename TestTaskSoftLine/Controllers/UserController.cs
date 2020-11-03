using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TestTaskSoftline.BLL.Interfaces;
using TestTaskSoftline.BLL.Models;

namespace TestTaskSoftLine.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAsync(Guid id)
        {
            var user = await _userService.GetUserAsync(id);
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var allUsers = await _userService.GetAllUsersAsync();
            return Ok(allUsers);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync(UserModel user)
        {
            await _userService.CreateUserAsync(user);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<OkObjectResult> DeleteUserAsync(Guid id)
        {
            var result = await _userService.DeleteUserAsync(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<OkObjectResult> UpdateUserAsync(Guid id, UserModel user)
        {
            var result = await _userService.UpdateUserAsync(id, user);
            return Ok(result);
        }
    }
}
