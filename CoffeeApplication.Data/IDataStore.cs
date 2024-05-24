using CoffeeApplication.Data.Entities;

namespace CoffeeApplication.Data
{
    public interface IDataStore
    {
        Task<Order> GetOrderAsync(Order order, CancellationToken cancellationToken = default);
        Task<IEnumerable<Order>> GetOrdersAsync(CancellationToken cancellationToken = default);
        Task SaveOrderAsync(Order order);
        Task SaveProductAsync(Product product);
        Task<IEnumerable<Product>> GetProdutAsync(CancellationToken cancellationToken = default);
        Task SaveDiscountAsync(Discount discount);
    }
}
