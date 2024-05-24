using CoffeeApplication.Data;
using CoffeeApplication.Data.Entities;

namespace CoffeeApplication.BusinessLogic.Service
{
    public class OrderService
    {
        private readonly IDataStore _dataStore;
        public OrderService(IDataStore dataStore)
        {
            _dataStore = dataStore;

        }

        public async Task AddOrderAsync(Order order)
        {
            await _dataStore.SaveOrderAsync(order);
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(Guid Id, CancellationToken cancellationToken = default)
        {

            return await _dataStore.GetOrdersAsync(cancellationToken);
        }
    }
}
