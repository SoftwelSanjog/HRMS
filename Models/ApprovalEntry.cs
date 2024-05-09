using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Models
{
    public class ApprovalEntry
    {
        public int Id { get; set; }
        [DisplayName("Record ID")]
        public int RecordId { get; set; }
        [DisplayName("Document Type")]
        public int DocumentTypeId { get; set; }
        public SystemCodeDetail DocumentType { get; set; }
        [DisplayName("Sequence No")]
        public int SequenceNo { get; set; } //Approvals
        [DisplayName("Approver Name")]
        public string ApproverId { get; set; } // Approvers
        public ApplicationUser Approver { get; set; }
        public int StatusId { get; set; } //Status of the document
        public SystemCodeDetail Status { get; set; }
        [DisplayName("Date Sent for Approval")]
        public DateTime DateSentForApproval { get;set; } //date sent for approval
        [DisplayName("Last Modified On")]
        public DateTime LastModifiedOn { get;set; } //the action of the approvers
        [DisplayName("Last Modified By")]
        public string LastModifiedById { get;set; }
        public ApplicationUser LastModifiedBy { get; set; }
        [DisplayName("Comments")]
        public string Comments { get; set; }

    }
}
