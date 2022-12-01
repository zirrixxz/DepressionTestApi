using DepressionTestLib.Data;
using DepressionTestLib.Helpers;
using DepressionTestLib.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DepressionTestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        UserManager UserManager;
        public UserController(UserManager userManager)
        {

            this.UserManager = userManager;
        }
        [HttpPost]
        public async Task AddUser(AddUserRequest userRequest)
        {
            await UserManager.AddUser(userRequest);
        }
        [HttpPost]
        public async Task<bool> Login(UserLoginRequest loginRequest)
        {
            return await UserManager.Login(loginRequest);
        }
    }
}
