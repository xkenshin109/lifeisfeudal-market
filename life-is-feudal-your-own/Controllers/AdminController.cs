using CoreManagement;
using CoreManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace life_is_feudal_your_own.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult All()
        {
            using(var db = new LifeIsFeudalDb())
            {
                var ret = db.Configurations.ToList().Select(x=> new {
                    id = x.Id,
                    key = x.Key,
                    value = x.Value
                });
                return Json(ret, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Save(Configuration config)
        {
            using(var db = new LifeIsFeudalDb())
            {
                db.Entry(config).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return Json(config);
            }
        }
        public ActionResult CheckAccess(string password)
        {
            if(password != "10goldbitch")
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            return Json(true);
        }
    }
}