using CoreManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}