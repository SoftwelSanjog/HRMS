using HRMS.Models;
using System.ComponentModel;

namespace HRMS.ViewModels
{
    public class ProfileViewModel
    {
        public ICollection<SystemProfile> Profiles { get; set; }
        [DisplayName("Role")]
        public string RoleId { get; set; }
        public int TaskId { get; set; }


    }
}
