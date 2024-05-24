using CoffeeApplication.Data.Entities;
using CoffeeApplication.HttpClients;
using CoffeeApplication.Interfaces;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace CoffeeApplication.DataServices
{
    public class OrderDataService : IDataStore
    {
        private readonly CoffeeApiHttpClient _httpClient;
        public OrderDataService(CoffeeApiHttpClient coffeeApiHttpClient)
        {
            _httpClient = coffeeApiHttpClient;
        }
        public Task<Order> GetOrderAsync(Order order, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var uri = $"api/v1/Order/GetOrders/{id}";

            return await _httpClient.HttpClient.GetFromJsonAsync<IEnumerable<Order>>(uri) ?? Enumerable.Empty<Order>();

        }



        public Task<IEnumerable<Product>> GetProdutAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task SaveDiscountAsync(Discount discount)
        {
            throw new NotImplementedException();
        }



        public async Task SaveOrderAsync(string profileId, Models.Order order)
        {
            var uri = "api/v1/Order/PostOrder";

            try
            {
                order.Id = profileId;
                order.Discount = new Discount()
                {
                    EndDate = DateTime.Now,
                    StartDate = DateTime.Now,
                    Percentage = 2,
                    Orders = new List<Order>()

                };
                HttpResponseMessage response = await _httpClient.HttpClient.PostAsJsonAsync(uri, order);
                if (response.IsSuccessStatusCode)
                {
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                
                }
            }
            catch (HttpRequestException ex)
            {

                throw;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }


        public Task SaveProductAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
