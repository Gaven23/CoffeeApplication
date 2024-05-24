using CoffeeApplication.Data.Entities;
using CoffeeApplication.Interfaces;
using CoffeeApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using Order = CoffeeApplication.Models.Order;

namespace CoffeeApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataStore _dataStore;
        public HomeController(ILogger<HomeController> logger, IDataStore dataStore)
        {
            _logger = logger;
            _dataStore = dataStore;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreatOrder(Order order)
        {
            string userId = ((ClaimsIdentity)HttpContext.User.Identity)?.FindFirst(ClaimTypes.NameIdentifier)?.Value;


                if (order is null)
                    return BadRequest("A order must be present");

                await _dataStore.SaveOrderAsync(userId, order);

            return View(order);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
