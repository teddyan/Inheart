using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pinzoe_Client.Models;

namespace Pinzoe_Client.Controllers
{
    public class CartController : Controller
    {
        private PinzoeMFEntities db = new PinzoeMFEntities();

        // GET: Cart
        public ActionResult Index()
        {
            var cartItemSet = db.CartItemSet.Include(c => c.CartSet).Include(c => c.ProductSet).Where(x=>x.Cart_Id==1);
            return View(cartItemSet.ToList());
        }

        // GET: Cart/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItemSet cartItemSet = db.CartItemSet.Find(id);
            if (cartItemSet == null)
            {
                return HttpNotFound();
            }
            return View(cartItemSet);
        }

        // GET: Cart/Create
        public ActionResult Create()
        {
            ViewBag.Cart_Id = new SelectList(db.CartSet, "Id", "Id");
            ViewBag.Product_Id = new SelectList(db.ProductSet, "Id", "Name");
            return View();
        }

        // POST: Cart/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Count,Subtotal,Cart_Id,Product_Id")] CartItemSet cartItemSet)
        {
            if (ModelState.IsValid)
            {
                db.CartItemSet.Add(cartItemSet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cart_Id = new SelectList(db.CartSet, "Id", "Id", cartItemSet.Cart_Id);
            ViewBag.Product_Id = new SelectList(db.ProductSet, "Id", "Name", cartItemSet.Product_Id);
            return View(cartItemSet);
        }

        // GET: Cart/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItemSet cartItemSet = db.CartItemSet.Find(id);
            if (cartItemSet == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cart_Id = new SelectList(db.CartSet, "Id", "Id", cartItemSet.Cart_Id);
            ViewBag.Product_Id = new SelectList(db.ProductSet, "Id", "Name", cartItemSet.Product_Id);
            return View(cartItemSet);
        }

        // POST: Cart/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Count,Subtotal,Cart_Id,Product_Id")] CartItemSet cartItemSet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cartItemSet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cart_Id = new SelectList(db.CartSet, "Id", "Id", cartItemSet.Cart_Id);
            ViewBag.Product_Id = new SelectList(db.ProductSet, "Id", "Name", cartItemSet.Product_Id);
            return View(cartItemSet);
        }

        // GET: Cart/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItemSet cartItemSet = db.CartItemSet.Find(id);
            if (cartItemSet == null)
            {
                return HttpNotFound();
            }
            return View(cartItemSet);
        }

        // POST: Cart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CartItemSet cartItemSet = db.CartItemSet.Find(id);
            IEnumerable<PickedItemSet> pickedItemSet = db.PickedItemSet.Where(x => x.CartItem_Id == id).ToList();
            if (pickedItemSet != null)
            {
                foreach (var i in pickedItemSet)
                {
                    db.PickedItemSet.Remove(i);
                }
            }
            db.CartItemSet.Remove(cartItemSet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SubTotal(int? id,int? count)
        {
            if(count==null) {
                //var cartItemSet = db.CartItemSet.Include(c => c.CartSet).Include(c => c.ProductSet).Where(x=>x.);
                CartItemSet cartItemSet = db.CartItemSet.Find(id);
                TempData["CartId"] = cartItemSet.Cart_Id;
                return PartialView("_SubTotal",cartItemSet);
            }
            else
            {
                CartItemSet cartItemSet = db.CartItemSet.Find(id);
                cartItemSet.Count = count.Value;
                cartItemSet.Subtotal = cartItemSet.ProductSet.Price * cartItemSet.Count;
                db.SaveChanges();
                Total();
                return PartialView("_SubTotal",cartItemSet);
            }
            
        }
        public ActionResult Total()
        {
            CartSet cartSet = db.CartSet.Find(1);
            var cartItemSet = db.CartItemSet.Include(c => c.CartSet).Include(c => c.ProductSet).Where(x => x.Cart_Id == cartSet.Id);
            int total = 0 ;
            foreach (var i in cartItemSet) {
                total += i.Subtotal;
            }
            cartSet.Total = total;
            db.SaveChanges();
            return PartialView("_Total", cartSet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
