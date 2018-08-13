using CoreManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RazorEngine.Templating;
using System.IO;

namespace life_is_feudal_your_own.utils
{
    public static class htmlMessage
    {
        public static string CreateHtmlHeader(OrderForm order)
        {
            string header = $"<h2>Player Name: {order.PlayerName}</h2><br/>";
            header += $"<h4>Order Number: {order.Id.ToString().PadLeft(5, '0')}</br>";
            return header;
        }
        public static string CreateTable(List<OrderFormProduct> products)
        {
            string header = "<table>" +
                "<thead>"
                + "<tr>"
                + "<th class=\"col-md-3\"'>Item</th>"
                + "<th class=\"col-md-3\"'>Quality</th>"
                + "<th class=\"col-md-3\"'>Price</th>"
                + "<th class=\"col-md-3\"'>Quantity</th>"
                + "</tr>"
                + "</thead>";
            //+ "</table>";
            products.ForEach(x =>
            {
                header += "<tr>" +
                $"<td class=\"col-md-3\"'>{x.ItemQuality.Item.Name}</td>" +
                $"<td class=\"col-md-3\"'>{x.ItemQuality.ItemQualityType.Name}</td>" +
                $"<td class=\"col-md-3\"'>{x.Price}</td>" +
                $"<td class=\"col-md-3\"'>{x.Quantity}</td>" +
                $"</tr>";
            });
            header += "</table>";
            return header;
        }
        public static string CreateEmailHtml(OrderForm order)
        {
            var itemsSelling = order.Products.Where(x => x.Selling).ToList();
            var itemsBuying = order.Products.Where(x => !x.Selling).ToList();
            var deal = new
            {
                PlayerName = order.PlayerName,
                OrderNumber = order.OrderNumber,
                BuyProducts = itemsBuying.Select(x=>new
                {
                    Name = x.ItemQuality.Item.Name,
                    Quality = x.ItemQuality.ItemQualityType.Name,
                    Price = Convert.ToInt32(x.ItemQuality.ItemQualityType.SellMultiplier * x.ItemQuality.Item.Price * x.ItemQuality.ItemQualityType.BuyMultiplier),
                    Quantity = x.Quantity,
                    Total = Convert.ToInt32(x.Price)
                }),
                SellProducts = itemsSelling.Select(x => new
                {
                    Name = x.ItemQuality.Item.Name,
                    Quality = x.ItemQuality.ItemQualityType.Name,
                    Price = Convert.ToInt32(x.ItemQuality.ItemQualityType.SellMultiplier * x.ItemQuality.Item.Price),
                    Quantity = x.Quantity,
                    Total = Convert.ToInt32(x.Price)
                }),
                TotalBuying = Convert.ToInt32(itemsBuying.Sum(x=>x.Price)),
                TotalSelling = Convert.ToInt32(itemsSelling.Sum(x=>x.Price))
            };
            var html = "<html><head>" +
           "<meta charset=\"utf-8\" />" +
           "<link rel=\"stylesheet\" href=\"http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css\" />" +
           "<link rel=\"stylesheet\" href=\"https://ajax.googleapis.com/ajax/libs/jquery/1.12.2/jquery.min.js \" />" +
           "<link rel=\"stylesheet\" href=\"http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js \" />"+
           "<link rel=\"stylesheet\" href=\"http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js \" />";
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
            var css = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
            css = Path.Combine(css + "Templates", "style.css");
            filePath = Path.Combine(filePath + "Templates", "OrderEmail.cshtml");
#pragma warning disable CS0618 // Type or member is obsoletes
            var temp = new TemplateService();
#pragma warning restore CS0618 // Type or member is obsolete
            //" + temp.Parse(File.ReadAllText(css), null, null, null) + "
            html += "<style></style></head>" + "<body style=\"padding-bottom: 5px\">"; ;
            html += temp.Parse(File.ReadAllText(filePath), deal, null, null);
            html += "</body></html>";


            return html;
        }
    }
}