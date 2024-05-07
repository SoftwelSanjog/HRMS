namespace HRMS.Models
{
    public class LeaveAdjustmentEntry
    {
        public int Id { set; get; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public decimal NoOfDays { get; set; }
        public DateTime LeaveAdjustmentDate { get; set;}
        public DateTime? LeaveStartDate { get; set;}
        public DateTime? LeaveEndDate { get; set;}
        public string  AdjustmentDescription { get; set;}
        public int AdjustmentTypeId { get; set;}
        public SystemCodeDetail AdjustmentType { get; set;} 
    }
}
