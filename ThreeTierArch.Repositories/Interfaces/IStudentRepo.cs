
using ThreeTierArch.Entities;

namespace ThreeTierArch.Repositories.Interfaces
{
    public interface IStudentRepo
    {
        public Task SaveStudent(Student student);
        public Task UpdateStudent(Student student);
        public Task DeleteStudent(Student student);
        public Task<IEnumerable<Student>> GetAllStudent();
        public Task<Student?> GetStudentById(int id);
    }
}
