using System.ComponentModel.DataAnnotations;

namespace HRMS.Models
{
    public class Bank : UserActivity
    {
        [Key]
        public int Id { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }
        public string Branch { get; set; }
    }
}
