using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Models
{
    public class Employee : UserActivity
    {

        public int Id { get; set; }
        [Key]
        [DisplayName("Employee ID")]
        public string EmpId { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Middle Name")]
        public string? MiddleName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {MiddleName}  {LastName}";
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }
        [DisplayName("Country")]
        public string Country { get; set; }
        [DisplayName("Address")]
        public string Address { get; set; }
        [DisplayName("Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        public string Cluster { get; set; }
        public string Designation { get; set; }
        [DisplayName("Bank Account Number")]
        public string BankAccountNumber { get; set; }
        //public int BankId { get; set; }
        //public Bank Bank { get; set; }
        [DisplayName("Join Date")]
        public DateTime JoinDate { get; set; }
        [DisplayName("Contract End Date")]
        public DateTime ContractEndDate { get; set; }

        public string ProfilePictureURL { get; set; }
    }
}
