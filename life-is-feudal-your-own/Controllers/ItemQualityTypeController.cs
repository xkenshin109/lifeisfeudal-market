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
    public class ItemQualityTypeController : Controller
    {
        // GET: ItemQualityType
        public ActionResult All()
        {
            using(var db = new LifeIsFeudalDb())
            {
                var ret = db.ItemQualityTypes.ToList().Select(x => new
                {
                    id = x.Id,
                    name = x.Name,
                    sell_multiplier = x.SellMultiplier,
                    buy_multiplier = x.BuyMultiplier,
                    updated_at = x.Updated
                });
                return Json(ret, JsonRequestBehavior.AllowGet);
            }
        }
    }
}