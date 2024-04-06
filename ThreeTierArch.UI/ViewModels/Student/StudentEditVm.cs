using ThreeTierArch.Entities;

namespace ThreeTierArch.UI.ViewModels.Student
{
    public class StudentEditVm
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public Address PhysicalAddress { get; set; } = new Address();
        public List<StudentSkillCheckbox> Skills { get; set; } = new List<StudentSkillCheckbox>();
    }
}
