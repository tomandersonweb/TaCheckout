using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using KataCheckout.Domain;
using System.Collections.Generic;
using System.Linq;

namespace KataCheckout.Tests
{
    [TestClass]
    public class KataCheckoutTests
    {
        private Stock _stock;

        public KataCheckoutTests()
        {
            var stockList = new List<Item>
                {
                    new Item { SKU = "A", UnitPrice = 50 },
                    new Item { SKU = "B", UnitPrice = 30 },
                    new Item { SKU = "C", UnitPrice = 20 },
                    new Item { SKU = "D", UnitPrice = 15 }
                };

            var specialPrices = new List<ISpecialPrice>() {
                    new QuantitySpecialPrice(stockList.Where(x => x.SKU == "A").SingleOrDefault(), discountedPrice : 130, discountedQuantity : 3) { },
                    new QuantitySpecialPrice(stockList.Where(x => x.SKU == "B").SingleOrDefault(), discountedPrice : 45, discountedQuantity : 2 ) { }
                    };

            _stock = new Stock(stockList, specialPrices);
        }

        [TestMethod]
        public void OneOfEachItemScanned()
        {
            // Add all items to the cart
            var basket = new Checkout(_stock);
            basket.Scan("A");
            basket.Scan("B");
            basket.Scan("C");
            basket.Scan("D");
            var totalPrice = basket.GetTotalPrice();

            totalPrice.ShouldBeOneOf(115);
        }

        [TestMethod]
        public void ThreeAItemScanned()
        {
            // Add three A items to the checkout
            var basket = new Checkout(_stock);
            basket.Scan("A");
            basket.Scan("A");
            basket.Scan("A");
            var totalPrice = basket.GetTotalPrice();

            totalPrice.ShouldBeOneOf(130);
        }
    }
}
