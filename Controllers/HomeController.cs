using Pinzoe_Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Pinzoe_Client.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PlanIndex()
        {
            return PartialView("_PlanIndex");
        }
        public ActionResult Food(int? id)
        {
            IEnumerable<FoodSet> fs;
            if (id == null)
            {
                using (PinzoeMFEntities db = new PinzoeMFEntities())
                {
                    fs = (from a in db.FoodSet
                          where a.FoodCategory_Id == 1
                          select a).ToList();
                    return PartialView("_FoodIndex", fs);
                }
            }
            else
            {
                using (PinzoeMFEntities db = new PinzoeMFEntities())
                {
                    fs = (from a in db.FoodSet
                          where a.FoodCategory_Id == id
                          select a).ToList();
                    return PartialView("_FoodIndex", fs);
                }
            }
        }
        //[HttpPost]
        //public ActionResult Food(int id) {
        //    using (PinzoeMFEntities db = new PinzoeMFEntities())
        //    {
        //        FoodSet foodCategoryQuery = db.FoodSet.Where(x => x.FoodCategory_Id == id).FirstOrDefault();
        //        return PartialView("_FoodIndex", foodCategoryQuery);
        //    }
        //}
        public ActionResult Team()
        {
            return PartialView("_Team");
        }
    }
}