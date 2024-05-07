using System.ComponentModel;

namespace HRMS.Models
{
    public class Holiday : UserActivity
    {
        public int Id { set; get; }
        [DisplayName("SN")]
        public int OrderId { set; get; }
        [DisplayName("Description")]
        public string Title { set; get; }
        [DisplayName("Date")]
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        public string Description { set; get; }
    }
}
