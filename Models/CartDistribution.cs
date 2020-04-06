using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pinzoe_Client.Models
{
    [Serializable]
    
    public class CartDistribution
    {
        private List<CartItemSet> cartItemSet;

        public CartDistribution()
        {
            this.cartItemSet = new List<CartItemSet>();
        }

    
    }
}