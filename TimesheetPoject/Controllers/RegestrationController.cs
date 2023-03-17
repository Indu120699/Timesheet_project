using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using TimesheetPoject.Context_Timesheet;
using TimesheetPoject.Interface;
using TimesheetPoject.Model;

namespace TimesheetPoject.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class RegestrationController : ControllerBase
    {
        private readonly ITimesheetInterface _timesheetInterface;
        private readonly Timesheet_Context _timesheet_Context;

        public RegestrationController(ITimesheetInterface timesheetInterface, Timesheet_Context timesheet_Context) 
        {
            _timesheetInterface = timesheetInterface;
            _timesheet_Context=timesheet_Context;
        }
        [HttpPost("Regestration")]
        public IActionResult Regestration(RegestrationModel regestrationModel)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(regestrationModel.Password);
            regestrationModel.HashKeyPassword = passwordHash;

            string pattern = "^[^0-9]*$";
            if (regestrationModel.Username == "" || !Regex.IsMatch(regestrationModel.Username, pattern))
            {
                return BadRequest("User name cannot be empty  and  user name cannot contain number");
            }
            string Passwordpattern1 = "^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$";
            if (regestrationModel.Password == ""|| !Regex.IsMatch(regestrationModel.Password, Passwordpattern1))
            {
                return BadRequest("Password should contain first letter should capital letter and one special symbol");
            }
            if (regestrationModel.Password!=regestrationModel.Confirmpassword)
            {
                return BadRequest("Conform Password should match with Password");
            }

            return Ok(_timesheetInterface.Regester(regestrationModel));
        }
        [HttpPost("Login")]
        public IActionResult Login(LoginModel loginModel)
        {
            var name = _timesheet_Context.Register.FirstOrDefault(i => i.Username== loginModel.Username);
            if (name== null)
            {
                return BadRequest("Username Not Existed..!!");
            }
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(loginModel.Password);
            var password = _timesheet_Context.Register.FirstOrDefault(i => i.HashKeyPassword== passwordHash);
            if (password!= null)
            {
                return BadRequest("Wrong Password");
            }
            return Ok(_timesheetInterface.Login(loginModel));
        }
        [HttpPut("Reset Password")]
        public IActionResult ResetPassword(LoginModel loginModel)
        {
            var name = _timesheet_Context.Register.FirstOrDefault(i => i.Username== loginModel.Username);
            if (name== null)
            {
                return BadRequest("Username Not Existed..!!");
            }
            string Passwordpattern1 = "^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$";
            if (loginModel.Password == ""|| !Regex.IsMatch(loginModel.Password, Passwordpattern1))
            {
                return BadRequest("Password should contain first letter should capital letter and one special symbol");
            }
            if (loginModel.Password!=loginModel.Confirmpassword)
            {
                return BadRequest("Conform Password should match with Password");
            }
            return Ok(_timesheetInterface.ResetPassword(loginModel));
        }
    }
}
