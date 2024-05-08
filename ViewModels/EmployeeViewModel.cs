using HRMS.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace HRMS.ViewModels
{
    public class EmployeeViewModel
    {
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

        [DisplayName("Address")]
        public string Address { get; set; }
        [DisplayName("Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        [DisplayName("Cluster")]
        public int ClusterId { get; set; }
        [DisplayName("Designation")]
        public int DesignationId { get; set; }

        [DisplayName("Bank Account Number")]
        public string BankAccountNumber { get; set; }
        public int BankId { get; set; }
        [DisplayName("Join Date")]
        public DateTime JoinDate { get; set; }
        [DisplayName("Contract End Date")]
        public DateTime ContractEndDate { get; set; }
        [DisplayName("Photo")]
        public string? ProfilePictureURL { get; set; }
        [DisplayName("Gender")]
        public int? GenderId { get; set; }
        [DisplayName("Company Email")]
        public string? CompanyEmail { get; set; }
        [DisplayName("Reason For Termination")]
        public int? ReasonForTerminationId { get; set; }
        [DisplayName("Employee Terms")]
        public int? EmploymentTermsId { get; set; }
        [DisplayName("Status")]
        public int? EmployeeStatusId { get; set; }
        [DisplayName("Allocated Leave")]
        public Decimal? AllocatedLeaveDays { get; set; }
        [DisplayName("Leave Balance")]
        public Decimal? LeaveOutStandingBalance { get; set; }
        public Employee Employee { get; set; }
        public List<Employee> Employees { get; set;}

    }
}
