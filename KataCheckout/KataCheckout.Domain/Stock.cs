using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KataCheckout.Domain
{
    public class Stock
    {
        protected Stock() { }
        public Stock(List<Item> stock, List<ISpecialPrice> specialPrices)
        {
            StockList = stock;
            SpecialPrices = specialPrices;
        }
        public List<Item> StockList { get; protected set; }
        public List<ISpecialPrice> SpecialPrices { get; protected set; }
    }
}
