using DepressionTestLib.DBContext;
using DepressionTestLib.Helpers;
using DepressionTestLib.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace DepressionTestLib.Data
{
    public class UserManager

    {
        DepressionTestDBContext db;
        UserManager<User> userManager;

        SignInManager<User> signInManager;
        private bool res;

        public UserManager(DepressionTestDBContext db, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<LoginResult> Login(UserLoginRequest loginRequest)
        {

            LoginResult res = new LoginResult();

           
            var findbyname = await userManager.FindByNameAsync(loginRequest.UserName);

            if (findbyname != null)
            {
                
                var r = await signInManager.PasswordSignInAsync(findbyname, loginRequest.Password, true, lockoutOnFailure: false);

                if (r.Succeeded == true)
                {
                    res.Message = "Success";
                    res.IsSuccess = true;
                    res.UserId = findbyname.Id;
                    res.RoleName = findbyname.RoleName;
                    res.FirstName = findbyname.FirstName;
                    res.LastName = findbyname.LastName;
                 

                    //gen jwt token
                    res.Token = GetToken(loginRequest.UserName,findbyname.Id);
                    return res;

                }
                else
                {
                    res.Message = "Can't login";
                    res.UserId = null;
                    res.IsSuccess = false;

                    return res;
                }
            }
            else
            {
                res.Message = "Can't login";
                res.UserId = null;
                res.IsSuccess = false;
                return res;
            }
        }
        public string GetToken(string user,string key)
        {
            
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            

            //string guid = Guid.NewGuid().ToString();
            var mykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(mykey, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddDays(1);
            var token = new JwtSecurityToken(
                issuer: user,
                audience: user,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<Result> AddUser(AddUserRequest userRequest)
        {
            Result res = new Result();
            User user = new User
            {
                FirstName = userRequest.FirstName,
                LastName = userRequest.LastName,
                Faculty = userRequest.Faculty,
                Year = userRequest.Year,
                UserName = userRequest.UserName,
                Email = userRequest.Email,
                DateOfBirth = userRequest.DateOfBirth,
                Age = userRequest.Age,


            };
            var checkUserExist = await userManager.FindByNameAsync(user.UserName);
            var checkEmailExist = db.Users.FirstOrDefault(f => f.Email == user.Email);
            if (checkUserExist == null && checkEmailExist == null)
            {
                var result = await userManager.CreateAsync(user, userRequest.Password);


                if (result.Succeeded)
                {

                    var find = await userManager.FindByNameAsync(userRequest.UserName);


                    var role = await userManager.AddToRoleAsync(find, userRequest.RoleName);


                    res.Message = "Success";

                    res.IsSuccess = true;

                    return res;

                }
                else
                {
                    res.Message = "Can't add user";

                    res.IsSuccess = false;

                    return res;

                }

            }
            else
            {


                res.Message = "UserName Existing Or Email Existing";

                res.IsSuccess = false;

                return res;

            }
        }
        //public List<User> GetUser()
        //{
        //    List<User> list = new List<User>();
        //    return list;
        //}

        public User GetCurrentUser(string userId)
        {
            User user = db.User.Where(f => f.Id == userId).FirstOrDefault();
            return user;
        }

       
        //public Result EditUser(EditUserRequest editUserRequest) 
        //{
        //    Result res = new Result();

        //    User updateUser = db.Users.Where(f => f.Id == editUserRequest.Id).FirstOrDefault();
        //    updateUser.FirstName = editUserRequest.FirstName;
        //    updateUser.LastName = editUserRequest.LastName;
        //    updateUser.Email = editUserRequest.Email;
        //    updateUser.Age = editUserRequest.Age;
        //    updateUser.DateOfBirth = editUserRequest.DateOfBirth;
        //    updateUser.Year = editUserRequest.Year;
        //    updateUser.Faculty = editUserRequest.Faculty;
        //    updateUser.Telephone = editUserRequest.Telephone;
        //    db.SaveChanges();

        //    res.Message = "Edit User success";
        //    res.IsSuccess = true;

        //    return res;
        //}

        public async Task<Result> ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            Result res = new Result();

            try {
               
                User user = await userManager.FindByIdAsync(changePasswordRequest.UserId);

               IdentityResult changePassword =  await userManager.ChangePasswordAsync(user,changePasswordRequest.OldPassword,changePasswordRequest.NewPassword);

                if (changePassword.Succeeded == true)
                {
                    res.Message = "Change Password success";
                    res.IsSuccess = true;
                    return res;
                }
                else
                {
                    res.Message = "Can't change Password";
                    res.IsSuccess = false;
                    return res;
                }

          
            }
            catch(Exception ex)
            {
                res.Message = "Can't change Password";
                res.IsSuccess = false;
                return res;
            }
        }


        public bool TokenCheck(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken read = tokenHandler.ReadJwtToken(token); //ฟังชั่นใช้อ่าน token

            if (read.ValidTo > DateTime.UtcNow) 
            {
                //ยังไม่หมดอายุ
                return false;
            }
          
            return true;
        }
    }
        
    }

