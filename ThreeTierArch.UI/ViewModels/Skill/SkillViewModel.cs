using System.ComponentModel.DataAnnotations;
using ThreeTierArch.UI.Validations;

namespace ThreeTierArch.UI.ViewModels.Skill
{
    public class SkillViewModel
    {
        public int Id { get; set; }

        [Required]
        [UpperCaseValidator(ErrorMessage ="Pehla letter uppercase hona chahiye")]
        public string Title { get; set; }
    }
}
