using CoreManagement;
using CoreManagement.Models;
using life_is_feudal_your_own.utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace life_is_feudal_your_own.Controllers
{
    public class OrderFormProductController : Controller
    {
        // GET: OrderFormProduct
        public ActionResult Index()
        {
            return View();
        }
   
        public ActionResult SaveOrderFormProduct(OrderFormProduct product)
        {
            using(var db = new LifeIsFeudalDb())
            {
                db.OrderFormProducts.Add(product);
                db.SaveChanges();
                return Json(product, JsonRequestBehavior.AllowGet);
            }
        }
    }
}