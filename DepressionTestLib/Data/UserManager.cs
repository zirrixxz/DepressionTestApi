using DepressionTestLib.DBContext;
using DepressionTestLib.Helpers;
using DepressionTestLib.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DepressionTestLib.Data
{
    public class UserManager

    {
        DepressionTestDBContext db;
        UserManager<User> userManager;

         SignInManager<User> signInManager;
        public UserManager(DepressionTestDBContext db, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<bool> Login(UserLoginRequest loginRequest)
        {
         
            var findbyname = await userManager.FindByNameAsync(loginRequest.UserName);
            // var findbyname = await userManager.FindByNameAsync(user.UserName);
            if (findbyname != null)
            {


                var r = await signInManager.PasswordSignInAsync(findbyname, loginRequest.Password, true, lockoutOnFailure: false);
                if (r.Succeeded == true)
                {

                    return true;
     
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public async Task AddUser(AddUserRequest userRequest)
        {
            User user = new User
            {
                FirstName = userRequest.FirstName,
                LastName= userRequest.LastName,
                Faculty = userRequest.Faculty,
                Year= userRequest.Year,
                UserName= userRequest.UserName,
                Email = userRequest.Email,
                DateOfBirth= userRequest.DateOfBirth,
                Age= userRequest.Age,


            };
            var checkUserExist = await userManager.FindByNameAsync(user.UserName);
            var checkEmailExist = db.Users.FirstOrDefault(f => f.Email == user.Email);
            if (checkUserExist == null && checkEmailExist == null)
            {
                var result = await userManager.CreateAsync(user, userRequest.Password);


                if (result.Succeeded)
                {

                    var find = await userManager.FindByNameAsync(userRequest.UserName);
                    //assign role
                    if (userRequest.RoleName == "Admin")
                    {
                        var role = await userManager.AddToRoleAsync(find, userRequest.RoleName);

                    }

                    //ลงorg id 1
                  

                    //r.Result = "Success";
                    //r.User = user;
                    //r.Success = true;

                    //return r;

                }
                else
                {
                    var res = result;
                    var rrr = "";
                    //r.Result = result.Errors.Select(f => f.Description).FirstOrDefault();
                    //r.User = user;
                    //r.Success = false;
                    //SystemLog.SaveLog(account.ActionBy, "User", "Create username : " + account.UserName + " " + r.Result, false);
                    //return r;
                }

            }
            else
            {
                //r.Result = "UserName Existing Or Email Existing";
                //r.User = user;
                //r.Success = false;
                //SystemLog.SaveLog(account.ActionBy, "User", "Create username : " + account.UserName + " " + r.Result, false);


                //return r;
            }
        }
        //public List<User> GetUser()
        //{
        //    List<User> list = new List<User>();
        //    return list;
        //}

        public void EditUser(EditUser editUser) //student
        {

        }

        public void DeleteUser(int id) //admin
        {

        }

        
    }
}
