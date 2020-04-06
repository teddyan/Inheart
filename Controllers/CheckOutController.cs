using Pinzoe_Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pinzoe_Client.Controllers
{
    public class CheckOutController : Controller
    {
        private PinzoeMFEntities db = new PinzoeMFEntities();
        // GET: CheckOut
        public ActionResult Index()
        {
            IEnumerable<CartItemSet> CIS = db.CartItemSet.Where(x => x.Cart_Id == 1).ToList();
            return View(CIS);
        }
        public ActionResult ShippingSelect()
        {

            return View();
        }

        [ChildActionOnly]
        public ActionResult PickItem(int cartitemID)
        {
            IEnumerable<PickedItemSet> PIS = db.PickedItemSet.Where(x => x.CartItem_Id == cartitemID).ToList();
            return PartialView("_PickItem", PIS);
        
        }
    }
}