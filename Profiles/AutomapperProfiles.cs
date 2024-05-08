using AutoMapper;
using HRMS.Models;
using HRMS.ViewModels;

namespace HRMS.Profiles
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<Employee,EmployeeViewModel>().ReverseMap();
        }
    }
}
