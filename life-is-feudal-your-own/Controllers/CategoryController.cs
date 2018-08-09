using CoreManagement.Models;
using CoreManagement;
using life_is_feudal_your_own.utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace life_is_feudal_your_own.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult All()
        {
            using(var db = new LifeIsFeudalDb())
            {
                var ret = db.Categories.ToList().Select(x=>new {
                    id = x.Id,
                    name = x.Name
                });
                return Json(ret, JsonRequestBehavior.AllowGet);
            }
            
        }
        public ActionResult GetSubCategoriesById(long id)
        {
            using (var db = new LifeIsFeudalDb())
            {
                var ret = db.Items.Where(x => x.Category_Id == id).Select(x => new
                {
                    id = x.SubCategory.Id,
                    name = x.SubCategory.Name
                }).ToList().Distinct();
                return Json(ret, JsonRequestBehavior.AllowGet);
            }
        }
    }
}