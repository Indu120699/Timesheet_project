using System.ComponentModel.DataAnnotations;

namespace TimesheetPoject.Model
{
    public class RegistrationModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        //[Required(ErrorMessage = "Username is required ")]
        public string Username { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public DateTime DateOfJoin { get; set; }

        //[Required(ErrorMessage = "Password is required ")]
        public string Password { get; set; }

        public string HashKeyPassword { get; set; } 

        //[Required(ErrorMessage = "ConformPassword is required ")]
        public string Confirmpassword { get; set; }



    }
}
