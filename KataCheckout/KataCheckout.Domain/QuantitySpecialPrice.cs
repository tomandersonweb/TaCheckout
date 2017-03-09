using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KataCheckout.Domain
{
    public class QuantitySpecialPrice : ISpecialPrice
    {
        protected QuantitySpecialPrice() { }

        public QuantitySpecialPrice(Item item, int discountedQuantity, int discountedPrice)
        {
            Item = item;
            DiscountedQuantity = discountedQuantity;
            DiscountedPrice = discountedPrice;
        }

        public Item Item { get; protected set; }
        public int DiscountedQuantity { get; protected set; }
        public int DiscountedPrice { get; protected set; }

        public int CalculateDiscountedPrice(Item item, int quantity)
        {
            if (quantity < DiscountedQuantity)
                return quantity * Item.UnitPrice;

            var qtyNormalPrice = (quantity % DiscountedQuantity) * Item.UnitPrice;
            var qtyDiscountPrice = (quantity / DiscountedQuantity) * DiscountedPrice;

            return qtyNormalPrice + qtyDiscountPrice;
        }
    }
}
