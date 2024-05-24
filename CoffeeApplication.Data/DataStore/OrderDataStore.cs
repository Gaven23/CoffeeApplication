using CoffeeApplication.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoffeeApplication.Data.DataStore
{
    partial class DataStore
    {

        public Task<Order> GetOrderAsync(Order order, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task SaveOrderAsync(Order order)
        {
            var newOrder = new Order
            {
                Id = order.Id,
                DiscountId = order.DiscountId,
                ProductId = order.ProductId,
                Quantity = order.Quantity,
                Price = order.Price,

            };

            _dbContext.Order.Add(newOrder);

            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<Order>> GetOrdersAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Order.Include(e => e.Product).Include(e => e.Discount).ToListAsync(cancellationToken);
        }

        public Task<IEnumerable<Product>> GetProdutAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task SaveDiscountAsync(Discount discount)
        {
            throw new NotImplementedException();
        }

        public Task SaveProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

    
    }
}
