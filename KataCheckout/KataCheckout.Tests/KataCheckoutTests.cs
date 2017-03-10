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
        public void OneOfEachItemScanned_ShouldBe115()
        {
            var basket = new Checkout(_stock);
            basket.Scan("A");
            basket.Scan("B");
            basket.Scan("C");
            basket.Scan("D");
            var totalPrice = basket.GetTotalPrice();

            totalPrice.ShouldBe(115);
        }

        [TestMethod]
        public void ThreeAItemScanned_ShouldBe130()
        {
            var basket = new Checkout(_stock);
            basket.Scan("A");
            basket.Scan("A");
            basket.Scan("A");
            var totalPrice = basket.GetTotalPrice();

            totalPrice.ShouldBe(130);
        }

        [TestMethod]
        public void TwoBItemScanned_ShouldBe45()
        {
            var basket = new Checkout(_stock);
            basket.Scan("B");
            basket.Scan("B");
            var totalPrice = basket.GetTotalPrice();

            totalPrice.ShouldBe(45);
        }

        [TestMethod]
        public void ThreeATwoBOneCItemScanned_ShouldBe45()
        {
            var basket = new Checkout(_stock);
            basket.Scan("A");
            basket.Scan("A");
            basket.Scan("A");
            basket.Scan("B");
            basket.Scan("B");
            basket.Scan("C");
            var totalPrice = basket.GetTotalPrice();

            totalPrice.ShouldBe(195);
        }

        [TestMethod]
        public void CalculateSpecialPriceThreeAItems_ShouldBe130()
        {
            var item = _stock.StockList.Where(x => x.SKU == "A").SingleOrDefault();
            var specialPrice = new QuantitySpecialPrice(item, discountedPrice : 130, discountedQuantity : 3) { };

            var totalPrice = specialPrice.CalculateDiscountedPrice(item, 3);

            totalPrice.ShouldBe(130);
        }

        [TestMethod]
        public void CalculateSpecialPriceThreeBItems_ShouldBe45()
        {
            var item = _stock.StockList.Where(x => x.SKU == "B").SingleOrDefault();
            var specialPrice = new QuantitySpecialPrice(item, discountedPrice: 45, discountedQuantity: 2) { };

            var totalPrice = specialPrice.CalculateDiscountedPrice(item, 2);

            totalPrice.ShouldBe(45);
        }

        [TestMethod]
        public void ScanEmtpyItem_ShouldThrow()
        {
            var basket = new Checkout(_stock);
            
            Should.Throw<ArgumentNullException>(() => basket.Scan(""));
        }

        [TestMethod]
        public void ScanItemNotInList_ShouldThrow()
        {
            var basket = new Checkout(_stock);

            Should.Throw<ArgumentOutOfRangeException>(() => basket.Scan("Z"));

        }
    }
}
