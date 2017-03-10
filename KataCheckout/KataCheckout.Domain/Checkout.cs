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

        public Checkout(Stock stock)
        {
            _stock = stock.StockList;
            _cart = new List<Item>();
            _specialPrices = stock.SpecialPrices;
        }

        public int GetTotalPrice()
        {
            var groupedItems = _cart.GroupBy(x => x).Select(item => new { item.Key, qty = item.Count() });

            foreach (var item in groupedItems)
            {
                var dsc = _specialPrices.Where(x => x.Item.SKU == item.Key.SKU).SingleOrDefault();

                if (dsc != null)
                    _totalPrice += dsc.CalculateDiscountedPrice(item.Key, item.qty);
                else
                    _totalPrice += item.Key.UnitPrice * item.qty;
            }

            return _totalPrice;
        }

        public void Scan(string item)
        {
            if (string.IsNullOrEmpty(item))
                throw new ArgumentNullException("Invalid scanned item");

            var stockItem = _stock.Where(x => x.SKU == item).SingleOrDefault();

            if (stockItem == null)
                throw new ArgumentOutOfRangeException("Item scanned is not in the stock list");

            _cart.Add(stockItem);
        }
    }
}
