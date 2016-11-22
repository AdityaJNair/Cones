using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cones.Models
{

    class IceCream
    {
        enum Flavours { Chocolate, Vanilla, Strawberry }
        enum Toppings { Chocolate_sprinkles }
        enum Scoops { Single, Double, Triple }

        Flavours _flavours { get; set; }
        Scoops _scoops { get; set; }
        Toppings _toppings { get; set; }

        public IceCream()
        {
        }

    }
}
