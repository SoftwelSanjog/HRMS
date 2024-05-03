using System.ComponentModel.DataAnnotations;

namespace HRMS.Models
{
    public class Employee : UserActivity
    {
        
        public int Id { get; set; }
        [Key]
        public string EmpId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {MiddleName}  {LastName}";
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Country { get; set; } 
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Cluster {  get; set; }
        public string Designation { get; set; }

    }
}
