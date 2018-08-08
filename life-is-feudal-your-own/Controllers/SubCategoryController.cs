using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoreManagement;
using life_is_feudal_your_own.utils;
using Newtonsoft.Json;

namespace life_is_feudal_your_own.Controllers
{
    public class SubCategoryController : Controller
    {
        // GET: SubCategory
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult All()
        {
            using(var db = new LifeIsFeudalDb())
            {
                var ret = db.SubCategories.ToList();
                return Json(ret, JsonRequestBehavior.AllowGet);
            }            
        }
    }
}