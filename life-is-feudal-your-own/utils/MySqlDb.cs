using life_is_feudal_your_own.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace life_is_feudal_your_own.utils
{
    public class MySqlDb
    {

        public static MySqlConnection
                 GetDBConnection(string host, int port, string database, string username, string password)
        {
            // Connection String.
            String connString = "Server=" + host + ";Database=" + database
                + ";port=" + port + ";User Id=" + username + ";password=" + password;

            MySqlConnection conn = new MySqlConnection(connString);

            return conn;
        }
    }
    public class MySqlUtils
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "104.197.211.98";
            int port = 3306;
            string database = "lifyodb";
            string username = "root";
            string password = "bacon123";

            return MySqlDb.GetDBConnection(host, port, database, username, password);
        }
    }
    public static class MySqlDbApi
    {
        private static MySqlDataReader runQuery(string query, MySqlConnection con)
        {
            MySqlDataReader _reader = null;
            try
            {

                MySqlCommand _cmd = new MySqlCommand(query, con);
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                _reader = _cmd.ExecuteReader();
            }
            catch (Exception ie)
            {
                con.Close();
            }
            return _reader;
        }
        private static int runUpdateQuery(string query, MySqlConnection con)
        {
            MySqlDataReader _reader = null;
            try
            {

                MySqlCommand _cmd = new MySqlCommand(query, con);
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                return _cmd.ExecuteNonQuery();
            }
            catch (Exception ie)
            {
                con.Close();
                return 0;
            }
        }
        #region Items
        public static List<Item> GetAllItems()
        {
            List<Item> Items = new List<Item>();
            var con = MySqlUtils.GetDBConnection();
            string query = "select id, name, price, subcategory_id, category_id, created_at, updated_at from lifyodb.Items";
            var _reader = runQuery(query, con);
            while (_reader.Read())
            {
                Items.Add(new Item
                {
                    Id = Convert.ToInt32(_reader["id"]),
                    Name = _reader["name"].ToString(),
                    Category_id = Convert.ToInt32(_reader["category_id"]),
                    Created = Convert.ToDateTime(_reader["created_at"]),
                    Updated = Convert.ToDateTime(_reader["updated_at"]),
                    Price = !string.IsNullOrEmpty(_reader["price"].ToString()) ? Convert.ToInt32(_reader["price"]) : 0,
                    SubCategory_id = Convert.ToInt32(_reader["subcategory_id"])
                });
            }
            return Items;
        }
        #endregion

        #region Lookups
        public static List<SubCategory> GetSubCategories()
        {
            List<SubCategory> _sc = new List<SubCategory>();
            var con = MySqlUtils.GetDBConnection();
            string query = "select id, name, created_at, updated_at from lifyodb.SubCategory";
            MySqlDataReader _reader = runQuery(query, con);
            while (_reader.Read())
            {
                _sc.Add(new SubCategory
                {
                    id = Convert.ToInt32(_reader["id"]),
                    name = _reader["name"].ToString(),
                    created_at = Convert.ToDateTime(_reader["created_at"]),
                    updated_at = Convert.ToDateTime(_reader["updated_at"]),
                });
            }
            return _sc;
        }
        public static List<Category> GetCategories()
        {
            List<Category> _c = new List<Category>();
            var con = MySqlUtils.GetDBConnection();
            string query = "select id, name, created_at, updated_at from lifyodb.Category";
            MySqlDataReader _reader = runQuery(query, con);
            while (_reader.Read())
            {
                _c.Add(new Category
                {
                    id = Convert.ToInt32(_reader["id"]),
                    name = _reader["name"].ToString(),
                    created_at = Convert.ToDateTime(_reader["created_at"]),
                    updated_at = Convert.ToDateTime(_reader["updated_at"]),
                });
            }
            return _c;
        }
        public static List<ItemQualityType> GetItemQualityTypes()
        {
            List<ItemQualityType> _iq = new List<ItemQualityType>();
            var con = MySqlUtils.GetDBConnection();
            string query = "select id,name,created_at,updated_at,buy_multiplier,sell_multiplier from lifyodb.ItemQualityType";
            MySqlDataReader _reader = runQuery(query, con);
            while (_reader.Read())
            {
                _iq.Add(new ItemQualityType
                {
                    Id = Convert.ToInt32(_reader["id"]),
                    Name = _reader["name"].ToString(),
                    Created = Convert.ToDateTime(_reader["created_at"]),
                    Updated = Convert.ToDateTime(_reader["updated_at"]),
                    BuyMultiplier = Convert.ToDecimal(_reader["buy_multiplier"]),
                    SellMultiplier = Convert.ToDecimal(_reader["sell_multiplier"])
                });
            }
            return _iq;
        }
        public static ItemQualityType GetItemQualityTypeById(long id)
        {
            ItemQualityType _iq = null;
            var con = MySqlUtils.GetDBConnection();
            string query = $"select id,name,created_at,updated_at,buy_multiplier,sell_multiplier from lifyodb.ItemQualityType where id ={id}";
            MySqlDataReader _reader = runQuery(query, con);
            while (_reader.Read())
            {
                _iq = new ItemQualityType
                {
                    Id = Convert.ToInt32(_reader["id"]),
                    Name = _reader["name"].ToString(),
                    Created = Convert.ToDateTime(_reader["created_at"]),
                    Updated = Convert.ToDateTime(_reader["updated_at"]),
                    BuyMultiplier = Convert.ToDecimal(_reader["buy_multiplier"]),
                    SellMultiplier = Convert.ToDecimal(_reader["sell_multiplier"])
                };
            }
            return _iq;
        }
        public static List<ItemQuality> GetItemQualities()
        {
            List<ItemQuality> _iq = new List<ItemQuality>();
            var con = MySqlUtils.GetDBConnection();
            string query = "select id,item_id,ItemQualityType_id,sell_active,buy_active,created_at,updated_at from lifyodb.ItemQuality";
            MySqlDataReader _reader = runQuery(query, con);
            while (_reader.Read())
            {
                _iq.Add(new ItemQuality
                {
                    Id = Convert.ToInt32(_reader["id"]),
                    CreatedAt = Convert.ToDateTime(_reader["created_at"]),
                    UpdatedAt = Convert.ToDateTime(_reader["updated_at"]),
                    BuyActive = Convert.ToInt32(_reader["buy_active"]) == 1,
                    SellActive = Convert.ToInt32(_reader["sell_active"]) == 1,
                    Item_Id = Convert.ToInt32(_reader["item_id"]),
                    ItemQualityType_id = Convert.ToInt32(_reader["ItemQualityType_id"])
                });
            }
            return _iq;

        }
        #endregion

        #region LookupByIds
        public static ItemQuality GetItemQualityById(long id)
        {
            ItemQuality _item = new ItemQuality();
            var con = MySqlUtils.GetDBConnection();
            string query = $"select * from lifyodb.ItemQuality where id = {id}";
            MySqlDataReader _reader = runQuery(query, con);
            while (_reader.Read())
            {
                _item = new ItemQuality
                {
                    Id = Convert.ToInt32(_reader["id"].ToString()),
                    Item_Id = Convert.ToInt32(_reader["item_id"]),
                    BuyActive = Convert.ToInt32(_reader["buy_active"]) == 1 ? true : false,
                    SellActive = Convert.ToInt32(_reader["sell_active"]) == 1 ? true : false,
                    ItemQualityType_id = Convert.ToInt32(_reader["ItemQualityType_id"]),
                    CreatedAt = Convert.ToDateTime(_reader["created_at"]),
                    UpdatedAt = Convert.ToDateTime(_reader["updated_at"])
                };
            }
            return _item;
        }
        public static List<ItemQuality> GetItemQualitysByItemId(long id)
        {
            List<ItemQuality> _item = new List<ItemQuality>();
            var con = MySqlUtils.GetDBConnection();
            string query = $"select * from lifyodb.ItemQuality where item_id = {id}";
            MySqlDataReader _reader = runQuery(query, con);
            while (_reader.Read())
            {
                var i = new ItemQuality
                {
                    Id = Convert.ToInt32(_reader["id"].ToString()),
                    Item_Id = Convert.ToInt32(_reader["item_id"]),
                    BuyActive = Convert.ToInt32(_reader["buy_active"]) == 1 ? true : false,
                    SellActive = Convert.ToInt32(_reader["sell_active"]) == 1 ? true : false,
                    ItemQualityType_id = Convert.ToInt32(_reader["ItemQualityType_id"]),
                    CreatedAt = Convert.ToDateTime(_reader["created_at"]),
                    UpdatedAt = Convert.ToDateTime(_reader["updated_at"])
                };
                _item.Add(i);
            }
            return _item;
        }
        #endregion

        #region Save Methods
        public static int SaveItemQuality(ref ItemQuality iq)
        {
            var con = MySqlUtils.GetDBConnection();
            string query;
            int result = 0;
            bool newRow = false;
            if (iq.Id != 0)
            {
                query = $"update lifyodb.ItemQuality set itemQualityType_id ='{iq.ItemQualityType_id}', item_id=${iq.Item_Id}, sell_active=${iq.SellActive}," +
                    $"buy_active=${iq.BuyActive},updated=current_date() where id = {iq.Id}";
            }
            else
            {
                query = "INSERT INTO lifyodb.ItemQuality(itemQualityType_id,item_id,sell_active,buy_active)" +
                    $"VALUES({iq.ItemQualityType_id},{iq.Item_Id},{iq.SellActive},{iq.BuyActive})";
                newRow = true;
            }
            try
            {
                if (newRow)
                {
                    MySqlDataReader _read = runQuery(query, con);
                    while (_read.Read())
                    {
                        iq.Id = Convert.ToInt32(_read["id"]);
                        result = 1;
                    }
                }
                else
                {
                    result = runUpdateQuery(query, con);
                }
            }
            catch (Exception ie)
            {
                con.Close();
                result = 0;
            }
            return result;
        }
        public static int SaveItem(ref Item item)
        {
            var con = MySqlUtils.GetDBConnection();
            string query;
            int result = 0;
            bool newRow = false;
            if (item.Id != 0)
            {
                query = $"update lifyodb.Items set name ='{item.Name}', category_id=${item.Category_id}, subcategory_id=${item.SubCategory_id}," +
                    $"price=${item.Price},updated=current_date()";
            }
            else
            {
                query = "INSERT INTO lifyodb.Items(name,category_id,subcategory_id,price)" +
                    $"VALUES('{item.Name}',{item.Category_id},{item.SubCategory_id},{item.Price})";
                newRow = true;
            }
            try
            {
                if (newRow)
                {
                    MySqlDataReader _read = runQuery(query, con);
                    while (_read.Read())
                    {
                        item.Id = Convert.ToInt32(_read["id"]);
                        result = 1;
                    }
                }
                else
                {
                    result = runUpdateQuery(query, con);
                }
            }
            catch (Exception ie)
            {
                con.Close();
                result = 0;
            }
            return result;
        }
        public static int SaveOrderForm(ref OrderForm order)
        {
            var con = MySqlUtils.GetDBConnection();
            string query;
            int result = 0;
            bool newRow = false;
            if (order.Id != 0)
            {
                query = $"update lifyodb.Items set playerName ='{order.PlayerName}', orderNumber='{order.OrderNumber}' where id = {order.Id}";
            }
            else
            {
                query = "INSERT INTO lifyodb.Items(playerName)" +
                    $"VALUES('{order.PlayerName}');" +
                    $"select LAST_INSERT_ID() as id;";
                newRow = true;
            }
            try
            {
                if (newRow)
                {
                    MySqlDataReader _read = runQuery(query, con);
                    while (_read.Read())
                    {
                        order.Id = Convert.ToInt32(_read["id"]);
                        order.OrderNumber = order.Id.ToString().PadLeft(5);
                        result = 1;
                    }
                    query = $"update lifyodb.Items set playerName ='{order.PlayerName}', orderNumber='{order.OrderNumber}' where id = {order.Id}";
                    runQuery(query, con);
                }
                else
                {
                    result = runUpdateQuery(query, con);
                }
            }
            catch (Exception ie)
            {
                con.Close();
                result = 0;
            }
            return result;
        }
        public static int SaveOrderFormProduct(ref OrderFormProducts product)
        {
            var con = MySqlUtils.GetDBConnection();
            string query;
            int result = 0;
            bool newRow = false;
            if (product.Id != 0)
            {
                query = $"update lifyodb.Items set OrderForm_id={product.OrderForm_id}, ItemQuality_id={product.ItemQuality_id},price={product.Price}, quantity={product.Quantity}, isSelling={product.Selling} where id = {product.Id}";
            }
            else
            {
                query = "INSERT INTO lifyodb.Items(OrderForm_id,ItemQuality_id,price,quantity,isSelling)" +
                    $"VALUES({product.OrderForm_id},{product.ItemQuality_id},{product.Price},{product.Quantity},{product.Selling});" +
                    $"select LAST_INSERT_ID() as id;";
                newRow = true;
            }
            try
            {
                if (newRow)
                {
                    MySqlDataReader _read = runQuery(query, con);
                    while (_read.Read())
                    {
                        product.Id = Convert.ToInt32(_read["id"]);
                        result = 1;
                    }
                    runQuery(query, con);
                }
                else
                {
                    result = runUpdateQuery(query, con);
                }
            }
            catch (Exception ie)
            {
                con.Close();
                result = 0;
            }
            return result;
        }
        #endregion
    }
}