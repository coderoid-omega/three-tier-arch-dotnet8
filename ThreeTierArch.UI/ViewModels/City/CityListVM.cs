using System.ComponentModel.DataAnnotations;

namespace ThreeTierArch.UI.ViewModels.City
{
    public class CityListVM
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "State")]
        public string StateName { get; set; }

        [Display(Name="Country")]
        public string CountryName { get; set; }
    }
}
