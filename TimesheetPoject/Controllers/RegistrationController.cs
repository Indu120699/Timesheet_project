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
    public class RegistrationController : ControllerBase
    {
        private readonly ITimesheetInterface _timesheetInterface;
        private readonly Timesheet_Context _timesheet_Context;

        public RegistrationController(ITimesheetInterface timesheetInterface, Timesheet_Context timesheet_Context) 
        {
            _timesheetInterface = timesheetInterface;
            _timesheet_Context=timesheet_Context;
        }
        [HttpPost("Regestration")]
        public IActionResult Regestration(RegistrationModel regestrationModel)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(regestrationModel.Password);
            regestrationModel.HashKeyPassword = passwordHash;

            string pattern = "^JOY\\d{4}$";
            if (regestrationModel.UserId == "" || !Regex.IsMatch(regestrationModel.UserId, pattern))
            {
                return BadRequest("UserId cannot be empty  and  UserId should start with JOY followed by your 4 numbers..!!");
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
            var uid = "^JOY\\d{4}$";

;
            var name = _timesheet_Context.Register.FirstOrDefault(i => i.UserId== loginModel.UserId);
            if (name== null)
            {
                return BadRequest("UserId can't be empty..!!");
            }
            else if (!Regex.IsMatch(loginModel.UserId, uid))
            {
                return BadRequest("UserId should start with JOY followed by your 4 numbers..!!");
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
            var name = _timesheet_Context.Register.FirstOrDefault(i => i.UserId == loginModel.UserId);
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
