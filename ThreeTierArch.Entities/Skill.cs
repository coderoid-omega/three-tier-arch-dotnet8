

namespace ThreeTierArch.Entities
{
    public class Skill
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<StudentSkill> StudentSkills { get; set;} = new HashSet<StudentSkill>();
    }
}
