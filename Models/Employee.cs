﻿using System.ComponentModel;
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
        [DisplayName("Full Name")]
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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]

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
        [DisplayFormat(ApplyFormatInEditMode =true, DataFormatString ="{0:yyyy/MM/dd}")]
        public DateTime JoinDate { get; set; }
        [DisplayName("Contract End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime ContractEndDate { get; set; }
        [DisplayName("Photo")]
        public string? ProfilePictureURL { get; set; }
        [DisplayName("Gender")]
        public int? GenderId { get; set; }
        public SystemCodeDetail Gender { get; set; }
        [DisplayName("Company Email")]
        public string? CompanyEmail { get; set; }
        [DisplayName("Reason For Termination")]
        public int? ReasonForTerminationId { get; set; }
        public SystemCodeDetail ReasonForTermination { get; set; }
        [DisplayName("Employee Terms")]
        public int? EmploymentTermsId { get; set; }
        public SystemCodeDetail EmploymentTerms { get; set; }
        [DisplayName("Status")]
        public int? EmployeeStatusId { get; set; }
        public SystemCodeDetail EmployeeStatus { get; set; }
        [DisplayName("Allocated Leave")]
        public Decimal? AllocatedLeaveDays { get; set; }
        [DisplayName("Leave Balance")]
        public Decimal? LeaveOutStandingBalance { get; set; }

    }
}
