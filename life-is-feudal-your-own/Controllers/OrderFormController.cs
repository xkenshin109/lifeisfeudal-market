using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using life_is_feudal_your_own.utils;
using Newtonsoft.Json;
using CoreManagement;
using CoreManagement.Models;

namespace life_is_feudal_your_own.Controllers
{
    public class OrderFormController : Controller
    {
        // GET: OrderForm
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Save(OrderForm product)
        {
            using(var db = new LifeIsFeudalDb())
            {
                try
                {
                    db.OrderForms.Add(product);
                    db.SaveChanges();
                    product.OrderNumber = product.Id.ToString().PadLeft(5, '0');
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    var ret = new
                    {
                        Id = product.Id,
                        OrderNumber = product.OrderNumber,
                        PlayerName = product.PlayerName
                    };
                    return Json(ret);
                }
                catch (Exception ie)
                {
                    var e = ie;
                    return Json(ie);
                }
            }
        }
        public ActionResult SendOrder(OrderForm order, List<OrderFormProduct> products)
        {
            string htmlBody = htmlMessage.CreateHtmlHeader(order);
            using (var db = new LifeIsFeudalDb())
            {
                products.ForEach(x =>
                {
                    x.ItemQuality = db.ItemQualities.FirstOrDefault(y => x.ItemQuality_Id == y.Id);
                });
            }
            var itemsSelling = products.Where(x => x.Selling).ToList();
            var itemsBuying = products.Where(x => !x.Selling).ToList();
            if(itemsSelling.Count > 0)
            {
                htmlBody += "</br><p>Selling</p>";
                htmlBody += htmlMessage.CreateTable(itemsSelling);
            }
            if(itemsBuying.Count > 0)
            {
                htmlBody += "</br></br><p>Buying</p>";
                htmlBody += htmlMessage.CreateTable(itemsBuying);
            }
            
            
            SendEmail.SendEmailMessage($"Player Order: {order.PlayerName} Order Number: {order.Id.ToString().PadLeft(5,'0')}",htmlBody);
            return Json(new { });
        }
    }
}