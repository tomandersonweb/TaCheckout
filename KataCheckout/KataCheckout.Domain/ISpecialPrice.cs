using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KataCheckout.Domain
{
    public interface ISpecialPrice
    {
        Item Item { get; }
        int CalculateDiscountedPrice(Item item, int quantity);
    }
}
