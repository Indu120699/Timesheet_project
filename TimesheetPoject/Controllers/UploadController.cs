using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimesheetPoject.Context_Timesheet;
using TimesheetPoject.Interface;
using TimesheetPoject.Model;

namespace TimesheetPoject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IUploadInterface _Iupload;
        private readonly Timesheet_Context _timesheet_Context;

        public UploadController(IUploadInterface iupload, Timesheet_Context timesheet_Context)
        {
            _Iupload = iupload;
            _timesheet_Context = timesheet_Context;
        }

        [HttpPost("data")]
        public IActionResult Post(UploadModel[] entries)
        {
            return Ok(_Iupload.add(entries));
        }

        [HttpPost("info")]
        public IActionResult Post1(EmployeeModel[] entries)
        {
            return Ok(_Iupload.add1(entries));
        }


        [HttpGet("Emp")]
        public List<EmployeeModel> getemp(int userId)
        {
            return _Iupload.GetEmpDet(userId).ToList();

        }
        [HttpGet("Days")]
        public List<UploadModel> getdays(int userId)
        {
            return _Iupload.GetTimesheetsByUserId(userId).ToList();

        }


    }
}
