using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using life_is_feudal_your_own.utils;
using CoreManagement.Models;
using CoreManagement;
using Newtonsoft.Json;

namespace life_is_feudal_your_own.Controllers
{
    public class ItemController : Controller
    {
        // GET: Item
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult All()
        {
            using(var db = new LifeIsFeudalDb())
            {
                var a = db.Items.ToList().Select(x => new {
                    created_at = x.Created,
                    id = x.Id,
                    name = x.Name,
                    price = x.Price,
                    sub_category = x.SubCategory_Id,
                    category_id = x.Category_Id
                }).OrderBy(x => x.category_id).OrderBy(x => x.sub_category).OrderBy(x => x.name); ;
                return Json(a, JsonRequestBehavior.AllowGet); ;
            }            
        }

        public ActionResult SaveItem(Item item)
        {
            using(var db = new LifeIsFeudalDb())
            {
                if(item.Id == 0)
                {
                    db.Items.Add(item);
                    return Json(item);
                }
                else
                {
                    var findExisting = db.Items.ToList().FirstOrDefault(x => x.Id == item.Id);
                    findExisting.Name = item.Name;
                    findExisting.Price = item.Price;
                    findExisting.SubCategory_Id = item.SubCategory_Id;
                    findExisting.Category_Id = item.Category_Id;
                    db.Entry(findExisting).State = System.Data.Entity.EntityState.Modified;
                    return Json(item);
                }                
            }
        }       
        public ActionResult GetItemQualitiesById(long id)
        {
            using(var db = new LifeIsFeudalDb())
            {
                var ret = db.ItemQualities.Where(x => x.Item_Id == id).ToList().Select(x => new {
                    buy_active = x.BuyActive,
                    id = x.Id,
                    Item_id = x.Item_Id,
                    ItemQualityType_id = x.ItemQualityType_Id,
                    sell_active = x.SellActive,
                    created_at = x.Created
                }); ;
                return Json(ret, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetItemsById(long catId,long subId)
        {
            using (var db = new LifeIsFeudalDb())
            {
                var ret = db.Items.Where(x => x.SubCategory_Id == subId && x.Category_Id == catId).ToList()
                    .Select(x=>new {
                        id = x.Id,
                        name = x.Name,
                        price = x.Price
                    });
                return Json(ret, JsonRequestBehavior.AllowGet);
            };

        }
    }
}