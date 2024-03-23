
using ThreeTierArch.Entities;

namespace ThreeTierArch.Repositories.Interfaces
{
    public interface ISkillRepo
    {
        public Task SaveSkill(Skill skill);
        public Task UpdateSkill(Skill skill);
        public Task DeleteSkill(Skill skill);
        public Task<IEnumerable<Skill>> GetAllSkill();
        public Task<Skill?> GetSkillById(int id);
    }
}
