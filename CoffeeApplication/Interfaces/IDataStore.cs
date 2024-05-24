using CoffeeApplication.Data.Entities;
using CoffeeApplication.Models;
using Order = CoffeeApplication.Data.Entities.Order;

namespace CoffeeApplication.Interfaces
{
    public interface IDataStore
    {
        Task<Order> GetOrderAsync(Order order, CancellationToken cancellationToken = default);
        Task<IEnumerable<Order>> GetOrdersAsync(Guid id, CancellationToken cancellationToken = default);
        Task SaveOrderAsync(string profileId, Models.Order order);
        Task SaveProductAsync(Product product);
        Task<IEnumerable<Product>> GetProdutAsync(CancellationToken cancellationToken = default);
        Task SaveDiscountAsync(Discount discount);
    }
}
