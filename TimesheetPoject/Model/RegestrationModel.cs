using System.ComponentModel.DataAnnotations;

namespace TimesheetPoject.Model
{
    public class RegestrationModel
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "Username is required ")]
        public string Username { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Password is required ")]
        public string Password { get; set; }

        public string HashKeyPassword { get; set; } 

        //[Required(ErrorMessage = "ConformPassword is required ")]
        public string Confirmpassword { get; set; }



    }
}
