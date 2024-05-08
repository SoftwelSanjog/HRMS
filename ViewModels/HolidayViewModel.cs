using HRMS.Migrations;
using HRMS.Models;
using System.ComponentModel;

namespace HRMS.ViewModels
{
    public class HolidayViewModel:UserActivity
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
        public Holiday Holiday { get; set; }
        public List<Holiday> Holidays { get;}
    }
}
