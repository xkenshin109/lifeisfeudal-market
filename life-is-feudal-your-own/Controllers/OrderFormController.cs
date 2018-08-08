using System;
using System.Collections.Generic;
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
        private static OrderForm _product { get; set; }
        // GET: OrderForm
        public ActionResult Index()
        {
            if(_product == null)
            {
                _product = new OrderForm
                {
                    PlayerName = ""
                };
            }
            return View();
        }
        public ActionResult Save(OrderForm product)
        {
            using(var db = new LifeIsFeudalDb())
            {
                db.OrderForms.Add(product);
                db.SaveChanges();
            }
            return Json(product, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SendOrder(OrderForm order, List<OrderFormProduct> products)
        {
            string htmlBody = htmlMessage.CreateHtmlHeader(order);
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