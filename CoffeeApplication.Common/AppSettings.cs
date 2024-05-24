using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeApplication.Common
{
    public class AppSettings
    {
        public ConnectionStrings? ConnectionStrings { get; set; }
    }

    public class ConnectionStrings
    {
        public string? CoffeeApplicationConnection { get; set; }
    }
}
