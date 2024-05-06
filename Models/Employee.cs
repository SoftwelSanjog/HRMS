using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Models
{
    public class Employee : UserActivity
    {
        [Key]
        public int Id { get; set; } 
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
        public int CountryId { get; set; }
        public Country Country { get; set; }
        [DisplayName("Address")]
        public string Address { get; set; }
        [DisplayName("Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        [DisplayName("Cluster")]
        public int ClusterId { get; set; }
        public Cluster Cluster { get; set; }
        [DisplayName("Designation")]
        public int DesignationId { get; set; }
        public Designation Designation { get; set; }

        [DisplayName("Bank Account Number")]
        public string BankAccountNumber { get; set; }
        public int BankId { get; set; }
        public Bank Bank { get; set; }
        [DisplayName("Join Date")]
        public DateTime JoinDate { get; set; }
        [DisplayName("Contract End Date")]
        public DateTime ContractEndDate { get; set; }

        public string ProfilePictureURL { get; set; }
        [DisplayName("Gender")]
        public int? GenderId { get; set; }
        public SystemCodeDetail Gender {  get; set; }

    }
}
