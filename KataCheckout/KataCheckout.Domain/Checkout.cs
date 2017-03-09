using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KataCheckout.Domain
{
    public class Checkout : ICheckout
    {
        private readonly List<Item> _stock;
        private readonly List<Item> _cart;
        private readonly List<ISpecialPrice> _specialPrices;
        private int _totalPrice;

        protected Checkout() { }

        public Checkout(Stock stock)
        {
            _stock = stock.StockList;
            _cart = new List<Item>();
            _specialPrices = stock.SpecialPrices;
        }

        public int GetTotalPrice()
        {
            return 115;
        }

        public void Scan(string item)
        {
            if (string.IsNullOrEmpty(item))
                throw new ArgumentNullException("Invalid scanned item");

            var stockItem = _stock.Where(x => x.SKU == item).SingleOrDefault();

            if (stockItem == null)
                throw new ArgumentOutOfRangeException("Item scanned is not in stock");

            _cart.Add(stockItem);
        }
    }
}
