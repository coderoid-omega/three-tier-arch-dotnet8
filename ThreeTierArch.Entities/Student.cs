
namespace ThreeTierArch.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //One to One relationship with Address and Student
        public Address PermanentAddress { get; set; }
        public ICollection<StudentSkill> StudentSkills { get; set; } = new HashSet<StudentSkill>();
    }
}
