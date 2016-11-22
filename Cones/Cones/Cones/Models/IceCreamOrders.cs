using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cones.Models
{

    public class IceCreamOrders
    {

        [JsonProperty(PropertyName = "Id")]
        public string ID { get; set; }
        [JsonProperty(PropertyName = "userId")]
        public string userId;
        [JsonProperty(PropertyName = "flavour")]
        public string flavour { get; set; }
        [JsonProperty(PropertyName = "date")]
        public DateTime date { get; set; }

        public IceCreamOrders(string userId, DateTime date, string flavour)
        {
            this.userId = userId;
            this.flavour = flavour;
            this.date = date;
        }
    }
}
