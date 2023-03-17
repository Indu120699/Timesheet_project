using System.ComponentModel.DataAnnotations;

namespace TimesheetPoject.Model
{
    public class LoginModel
    {
        public int Id { get; set; } 
        public string Username { get; set; }

        [EmailAddress]
        public string Email { get; set; }   
        public string Password { get; set; }
        public string Confirmpassword { get; set; }
    }
}
