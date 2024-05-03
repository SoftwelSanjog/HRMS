using System.ComponentModel.DataAnnotations;

namespace HRMS.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
