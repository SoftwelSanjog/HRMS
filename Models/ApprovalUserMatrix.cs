using System.ComponentModel;

namespace HRMS.Models
{
    public class ApprovalUserMatrix : UserActivity
    {

        public int Id { get; set; }
        [DisplayName("Full Name")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [DisplayName("Document Type")]
        public int DocumentTypeId { get; set; }
        public SystemCodeDetail DocumentType { get; set; }
        [DisplayName("WorkFlow User Group")]
        public int WorkFlowUserGroupId { get; set; }
        public WorkFlowUserGroup WorkFlowUserGroup { get; set; }
        public bool Active { get; set; }
    }
}
