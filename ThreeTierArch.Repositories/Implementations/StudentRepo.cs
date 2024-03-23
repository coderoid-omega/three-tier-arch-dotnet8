using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeTierArch.Entities;
using ThreeTierArch.Repositories.Interfaces;

namespace ThreeTierArch.Repositories.Implementations
{
    public class StudentRepo : IStudentRepo
    {
        private readonly ApplicationDbContext _context;
        public StudentRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task DeleteStudent(Student student)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Student>> GetAllStudent()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student?> GetStudentById(int id)
        {
            var student = await _context.Students.Where(m => m.Id == id).FirstOrDefaultAsync();
            return student;
        }

        public async Task SaveStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStudent(Student student)
        {
            _context.Update(student);
            await _context.SaveChangesAsync();

        }
    }
}
