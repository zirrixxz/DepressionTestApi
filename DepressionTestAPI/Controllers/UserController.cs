using DepressionTestLib.Data;
using DepressionTestLib.Helpers;
using DepressionTestLib.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        public async Task<Result> AddUser([FromBody] AddUserRequest userRequest)
        {
           return await UserManager.AddUser(userRequest);
        }

        [HttpPost]
        public async Task<LoginResult> Login([FromBody] UserLoginRequest loginRequest)
        {
            return await UserManager.Login(loginRequest);
        }

        //[HttpPost]
        //public Result EditUser([FromBody] EditUserRequest editUserRequest)
        //{
        //    return UserManager.EditUser(editUserRequest);
        //}

        [HttpPost]
        public async Task<Result> ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest)
        {
            return await UserManager.ChangePassword(changePasswordRequest);
        }

        [HttpGet]
        public bool TokenCheck(string token)
        {
            return UserManager.TokenCheck(token);
        }

        [HttpGet]
        public User GetCurrentUser(string userId)
        {
            return UserManager.GetCurrentUser(userId);
        }

    }
}
