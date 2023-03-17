using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TimesheetPoject.Model
{
    public class UploadModel
    {
        internal readonly string Employee_Name;

        [Key]
        public int Id { get; set; }

        public string Day { get; set; }

        public string Status { get; set; }

        public int total_hours { get; set; }
       public DateTime Date { get; set; }

        public string month { get; set; }
    }
}
