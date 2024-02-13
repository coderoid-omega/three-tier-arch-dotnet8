
using Microsoft.EntityFrameworkCore;
using ThreeTierArch.Entities;
using ThreeTierArch.Repositories.Interfaces;

namespace ThreeTierArch.Repositories.Implementations
{
    public class StateRepo : IStateRepo
    {
        private readonly ApplicationDbContext _context;
        public StateRepo(ApplicationDbContext dbContext) { 
            _context = dbContext;
        }
        public async Task DeleteState(State state)
        {
            _context.States.Remove(state);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<State>> GetAllState()
        {
            return await _context.States.Include(m => m.Country).ToListAsync();
        }

        public async Task<State?> GetStateById(int id)
        {
            return await _context.States.Include(m => m.Country).FirstOrDefaultAsync(x => x.Id == id);    
        }

        public async Task SaveState(State state)
        {
            _context.States.Add(state);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateState(State state)
        {
            _context.States.Update(state);
            await _context.SaveChangesAsync();
        }
    }
}
