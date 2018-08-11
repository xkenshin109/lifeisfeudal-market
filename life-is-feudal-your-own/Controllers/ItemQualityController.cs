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
    public class ItemQualityController : Controller
    {
        // GET: ItemQuality
        public ActionResult All()
        {
            using(var db = new LifeIsFeudalDb())
            {
                var ret = db.ItemQualities.ToList().Select(x => new
                {
                    x.Id,
                    x.Item_Id,
                    x.SellActive,
                    x.ItemQualityType_Id,
                    x.Free,
                    x.OverridePrice,
                    x.BuyActive,
                    x.Created
                });
                return Json(ret, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetItemQualityById(long id)
        {
            using(var db = new LifeIsFeudalDb())
            {
                var ret = db.ItemQualities.FirstOrDefault(x => x.Id == id);
                var item = new
                {
                    buy_active = ret.BuyActive,
                    id = ret.Id,
                    Item_id = ret.Item_Id,
                    ItemQualityType_id = ret.ItemQualityType_Id,
                    sell_active = ret.SellActive,
                    overridePrice = ret.OverridePrice,
                    free = ret.Free,
                    created_at = ret.Created
                };
                return Json(item, JsonRequestBehavior.AllowGet);
            }
            //var a = LifeIsFeudalApi.GetItemQuality(id);
            
        }
        public ActionResult GetItemQualities(long id)
        {
            using(var db = new LifeIsFeudalDb())
            {
                var items = db.ItemQualities.ToList().Where(x => x.Id == id).Select(y => new {
                    buy_active = y.BuyActive,
                    id = y.Id,
                    Item_id = y.Item_Id,
                    ItemQualityType_id = y.ItemQualityType_Id,
                    sell_active = y.SellActive,
                    overridePrice = y.OverridePrice,
                    free = y.Free,
                    created_at = y.Created
                });
                return Json(items, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Save(ItemQuality item)
        {
            using(var db = new LifeIsFeudalDb())
            {
                if(item.Id == 0)
                {
                    db.ItemQualities.Add(item);
                }
                else
                {
                    var existing = db.ItemQualities.ToList().FirstOrDefault(x => x.Id == item.Id);
                    existing.BuyActive = item.BuyActive;
                    existing.SellActive = item.SellActive;
                    existing.Item_Id = item.Item_Id;
                    existing.ItemQualityType_Id = item.ItemQualityType_Id;
                    existing.Free = item.Free;
                    existing.OverridePrice = item.OverridePrice;
                    db.Entry(existing).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
                var ret= new
                {
                    buy_active = item.BuyActive,
                    id = item.Id,
                    Item_id = item.Item_Id,
                    ItemQualityType_id = item.ItemQualityType_Id,
                    sell_active = item.SellActive,
                    created_at = item.Created,
                    free = item.Free,
                    overridePrice = item.OverridePrice
                };
                return Json(ret, JsonRequestBehavior.AllowGet);
            }
        }
    }
}