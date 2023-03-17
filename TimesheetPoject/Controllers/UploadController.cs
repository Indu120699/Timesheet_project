using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public UploadController(IUploadInterface iupload)
        {
            _Iupload = iupload;
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
    }
}
