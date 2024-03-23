
namespace ThreeTierArch.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<StudentSkill> StudentSkills { get; set; } = new HashSet<StudentSkill>();
    }
}
