using System;
using System.Collections.Generic;
using System.Text;

namespace MarketPriceGenerator_ZMQ
{
    class NewPrice
    {
        public string Id { get; set; }
        public decimal Price { get; set; }
        public string Source { get; set; }
        public override string ToString()
        {
            return $"{Id} has a new price of {Price} From {Source}";
        }
    }
}
