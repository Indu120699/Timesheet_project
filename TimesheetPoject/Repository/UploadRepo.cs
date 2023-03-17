using Microsoft.AspNetCore.Mvc;
using TimesheetPoject.Context_Timesheet;
using TimesheetPoject.Interface;
using TimesheetPoject.Model;

namespace TimesheetPoject.Repository
{
    public class UploadRepo : ControllerBase, IUploadInterface
    {
        private readonly Timesheet_Context _context;



        public UploadRepo(Timesheet_Context con)
        {
            _context = con;
        }

        public IActionResult add(UploadModel[] entries)
        {
            var model = new UploadModel();

            for (var i = 0; i < entries.Length; i++)
            {
                model.user_id = entries[i].user_id;
                model.total_hours = entries[i].total_hours;
                model.Date = DateTime.Now.Date;
                model.Day = entries[i].Day;
                model.Status = entries[i].Status;
                model.month = entries[i].month;

                _context.TS_table.Add(model);
                _context.SaveChanges();
            }
            return Ok();

        }



        public IActionResult add1(EmployeeModel[] entries)
        {
            var model1 = new EmployeeModel();

            for (var i = 0; i < entries.Length; i++)
            {
                model1.Employee_Name = entries[i].Employee_Name;
                model1.Employee_Email = entries[i].Employee_Email;
                model1.Joining_date = entries[i].Joining_date;
                model1.Phone_Number = entries[i].Phone_Number;


                _context.ETS_table.Add(model1);
                _context.SaveChanges();
            }
            return Ok();
        }
    }
}

