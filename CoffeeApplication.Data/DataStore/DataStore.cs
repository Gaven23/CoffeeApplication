using CoffeeApplication.Data.Entities;

namespace CoffeeApplication.Data.DataStore
{
    public partial class DataStore : IDataStore
    {
        private readonly ApplicationDbContext _dbContext;

        public DataStore(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
