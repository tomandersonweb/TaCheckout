using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KataCheckout.Domain
{
    public interface ICheckout
    {
        /// <summary>
        /// Scans an individual item into the checkout 
        /// </summary>
        /// <param name="item"></param>
        void Scan(string item);

        /// <summary>
        /// Gets total price including discount for all items placed in checkout
        /// </summary>
        /// <returns></returns>
        int GetTotalPrice();
    }
}
