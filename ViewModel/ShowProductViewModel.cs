using Pinzoe_Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pinzoe_Client.ViewModel
{
    public class ShowProductViewModel
    {
        public IEnumerable<ProductSet> ProductSet { get; set; }
        public IEnumerable<GiftboxItemSet> GiftboxItemSet { get; set; }
    }
}