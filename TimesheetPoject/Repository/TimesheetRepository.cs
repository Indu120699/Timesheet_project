﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using TimesheetPoject.Context_Timesheet;
using TimesheetPoject.Interface;
using TimesheetPoject.Model;

namespace TimesheetPoject.Repository
{
    public class TimesheetRepository : ControllerBase, ITimesheetInterface
    {
        private readonly Timesheet_Context _timesheet_Context;
        private readonly IConfiguration _configuration;

        public TimesheetRepository(Timesheet_Context timesheet_Context,IConfiguration configuration) 
        {
            _timesheet_Context = timesheet_Context;
            _configuration = configuration;
        }
        public IActionResult Regester(RegestrationModel regestrationModel)
        {
            _timesheet_Context.Register.Add(regestrationModel);
            _timesheet_Context.SaveChanges();
            return Ok("Regestered Successfully");
        } 

        public IActionResult Login(LoginModel loginModel)
        {
            
            List<Claim> claims = new List<Claim>
            {
                    new Claim(ClaimTypes.Name, loginModel.Username)
            };
            var newKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
           _configuration.GetSection("AppSettings:Token").Value!));
            var creds = new SigningCredentials(newKey, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(jwt);
        }
        public IActionResult ResetPassword(LoginModel loginModel)
        {
            var name = _timesheet_Context.Register.FirstOrDefault(i => i.Username== loginModel.Username);
            name.Username = loginModel.Username;
            name.Password= loginModel.Password;
            name.Email=  loginModel.Email;
            name.Confirmpassword = loginModel.Confirmpassword;
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(loginModel.Password);
            name.HashKeyPassword = passwordHash;
            _timesheet_Context.Register.Update(name);
            _timesheet_Context.SaveChanges();
            return Ok("Password Reset Successfully");
        }
    }
}
