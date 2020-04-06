using Pinzoe_Client.Models;
using Pinzoe_Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pinzoe_Client.Controllers
{
    public class ProductController : Controller
    {
        private PinzoeMFEntities db = new PinzoeMFEntities();
        // GET: Product
        public ActionResult Index()
        {
            IEnumerable<ProductTypeSet> pts = db.ProductTypeSet.ToList();
            return View(pts);
        }
        public ActionResult ShowProduct(int? ProductTypeId)
        {
            IEnumerable<ProductSet> ps;
            IEnumerable<GiftboxItemSet> gis;
            if (ProductTypeId == null)
            {
                return Content("");
            }
            else
            {
                ShowProductViewModel SPVM = new ShowProductViewModel();
                ps = (from a in db.ProductSet
                      where a.ProductType_Id == ProductTypeId
                      select a).ToList();
                gis = db.GiftboxItemSet.Where(x => x.Giftbox_Id == ProductTypeId).ToList();
                SPVM.ProductSet = ps;
                SPVM.GiftboxItemSet = gis;
                return PartialView("_ShowProduct", SPVM);
            }
        }
        //public ActionResult ItemPicked(int? giftboxId)
        //{
        //    IEnumerable<GiftboxItemSet> gIS;
        //    Session["ProductId"] = giftboxId;
        //    if (giftboxId == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        //Query in GiftBoxItem which are match giftbox_Id, show all have in view 
        //        gIS = (from a in db.GiftboxItemSet
        //               where a.Giftbox_Id == giftboxId
        //               select a).ToList();
        //        //ps = (from a in db.ProductSet
        //        //      where a.ProductSet_Giftbox.Id == giftboxId
        //        //      select a).ToList();
        //    }
        //    return View(gIS);

        //}
        public ActionResult CustomizeProduct(int? giftboxId)
        {
            IEnumerable<GiftboxItemSet> gIS;
            Session["ProductId"] = giftboxId;
            if (giftboxId == null)
            {
                return HttpNotFound();
            }
            else
            {
                //Query in GiftBoxItem which are match giftbox_Id, show all have in view 
                gIS = (from a in db.GiftboxItemSet
                       where a.Giftbox_Id == giftboxId
                       select a).ToList();
                //ps = (from a in db.ProductSet
                //      where a.ProductSet_Giftbox.Id == giftboxId
                //      select a).ToList();
            }
            return View(gIS);
        }
        public ActionResult Cookies(int? FoodId)
        {
            IEnumerable<FoodSet> fS;
            FoodCategorySet fcS;
            if (FoodId == null)
            {
                fS = db.FoodSet.ToList();
                return Content("");
            }
            else
            {
                fS = db.FoodSet.Where(x => x.FoodCategory_Id == FoodId);
                fcS = db.FoodCategorySet.Find(FoodId);
                ViewBag.CookieCategoryName = fcS.Name;
                ViewBag.CookieCategoryID = fcS.Id;
                return PartialView("_Cookies", fS);
            }
        }
        //[HttpPost]
        //public ActionResult ProductComplete(int[] Cookies, int[] Count)
        //{
        //    CartItemSet cartItemSet = new CartItemSet()
        //    {
        //        //禮盒數量
        //        Count = 1,
        //        //禮盒金額[第一部分]
        //        Subtotal = 0,
        //        //購物車Id 會員登入後創建
        //        Cart_Id = 1,
        //        //將所選取的禮盒Id,轉型成int型態
        //        Product_Id = ((int)(Session["ProductId"])),

        //    };

        //    db.CartItemSet.Add(cartItemSet);
        //    db.SaveChanges();
        //    for (int i = 0; i < FoodId.Count(); i++)
        //    {
        //        PickedItemSet pickItemSet = new PickedItemSet()
        //        {
        //            //餅乾數量
        //            Count = CookieCount[i],
        //            Food_Id = FoodId[i],
        //            CartItem_Id = cartItemSet.Id
        //        };
        //        db.PickedItemSet.Add(pickItemSet);
        //        db.SaveChanges();
        //    }
        //    Session["CartItemID"] = cartItemSet.Id;
        //    return RedirectToAction("Index");
        //}
    }
}
