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
    public class SkillRepo : ISkillRepo
    {
        private readonly ApplicationDbContext _context;

        public SkillRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task DeleteSkill(Skill skill)
        {
            _context.Remove(skill);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Skill>> GetAllSkill()
        {
            return await _context.Skills.ToListAsync();
        }

        public async Task<Skill?> GetSkillById(int id)
        {
            var skill = await _context.Skills.Where(m => m.Id == id).FirstOrDefaultAsync();
            return skill;
        }

        public async Task SaveSkill(Skill skill)
        {
            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSkill(Skill skill)
        {
            _context.Skills.Update(skill);
            await _context.SaveChangesAsync();
        }
    }
}
