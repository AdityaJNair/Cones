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

        enum Flavours { Chocolate, Vanilla, Strawberry }
        enum Toppings { Chocolate_sprinkles }
        enum Scoops { Single, Double, Triple }

        [JsonProperty(PropertyName = "flavour")]
        Flavours _flavours { get; set; }

        [JsonProperty(PropertyName = "scoops")]
        Scoops _scoops { get; set; }

        [JsonProperty(PropertyName = "toppings")]
        Toppings toppings { get; set; }

        [JsonProperty(PropertyName = "createdAt")]
        public DateTime date { get; set; }

        public IceCreamOrders(string userId)
        {
            this.userId = userId;
        }
    }
}
