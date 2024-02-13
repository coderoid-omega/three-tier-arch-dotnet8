
using System.Threading.Tasks;
using ThreeTierArch.Entities;

namespace ThreeTierArch.Repositories.Interfaces
{
    public interface IStateRepo
    {
        public Task SaveState(State state);
        public Task UpdateState(State state);
        public Task DeleteState(State state);
        public Task<IEnumerable<State>> GetAllState();
        public Task<State?> GetStateById(int id);
    }
}
