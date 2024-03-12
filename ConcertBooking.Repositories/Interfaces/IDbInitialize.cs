

namespace ConcertBooking.Repositories.Interfaces
{
    /// <summary>
    /// This interface will be used to set default data to be added in the system for any specific table, The logic of seeding data can be customised in Seed method
    /// </summary>
    public interface IDbInitialize
    {
        public Task Seed();
    }
}
