

namespace ThreeTierArch.Entities
{
    public class StudentSkill
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}
