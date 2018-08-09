using ClosedXML.Excel;
using CoreManagement;
using CoreManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkbookImport
{
    public static class MasterWorkbook
    {
        public static void ReadFile(string filePath)
        {
            using (var wb = new XLWorkbook(filePath))
            using (var db = new LifeIsFeudalDb())
            {
                var qualities = db.ItemQualityTypes.ToList();
                var ws = wb.Worksheet(1);
                List<Category> _categoriesReadyToSave = new List<Category>();
                List<SubCategory> _subCategoriesReadyToSave = new List<SubCategory>();
                List<Item> _itemsReadyToSave = new List<Item>();
                Category cat = new Category();

                SubCategory _sub = new SubCategory();
                foreach (IXLRow row in ws.RowsUsed())
                {

                    if (row.RowNumber() >= 5)
                    {
                        try
                        {
                            var name = row.Cell("A").Value.ToString();
                            var buy = row.Cell("C").Value.ToString();
                            var sell = row.Cell("D").Value.ToString();
                            var hidden = row.IsHidden;
                            if (name == "Builder's Corner")
                            {
                                Console.WriteLine("----=======");
                            }
                            if (row.IsHidden) continue;
                            if (string.IsNullOrEmpty(buy) && string.IsNullOrEmpty(sell) && name != "Hand and a Half Swords")
                            {
                                if (!string.IsNullOrEmpty(name))
                                {
                                    _sub = db.SubCategories.ToList().FirstOrDefault(x => x.Name == "General Goods");
                                    //Leave and continue
                                    cat = new Category
                                    {
                                        Name = name,
                                        Created = DateTime.Now
                                    };
                                    _categoriesReadyToSave.Add(cat);
                                    Console.WriteLine($"Category: {name}");
                                }
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(sell))
                                {
                                    _sub = new SubCategory
                                    {
                                        Name = name
                                    };
                                    _subCategoriesReadyToSave.Add(_sub);
                                    Console.WriteLine($"SubCategory: {name}");
                                }
                                else
                                {
                                    var setFree = false;
                                    if (sell.Equals("Free"))
                                    {
                                        setFree = true;
                                        sell = "0";
                                    }
                                    Item _item = new Item
                                    {
                                        Name = name,
                                        Category = cat,
                                        SubCategory = _sub,
                                        Price = Convert.ToInt32(StripCharacters(sell)),
                                        Created = DateTime.Now
                                    };
                                    foreach (var quality in qualities)
                                    {
                                        _item.Qualities.Add(new ItemQuality
                                        {
                                            Item = _item,
                                            Free = setFree,
                                            BuyActive = true,
                                            SellActive = true,
                                            ItemQualityType = quality,
                                            ItemQualityType_Id = quality.Id,
                                            Created = DateTime.Now
                                        });
                                    }
                                    _itemsReadyToSave.Add(_item);
                                    Console.WriteLine($"Item: {name}");
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            if (e.Message.Contains("base materials"))
                            {
                                continue;
                            }
                            Console.WriteLine(e);
                        }
                    }
                }
                db.Categories.AddRange(_categoriesReadyToSave);
                db.SubCategories.AddRange(_subCategoriesReadyToSave);
                db.Items.AddRange(_itemsReadyToSave);
                db.SaveChanges();
            }
        }
        private static string StripCharacters(string input)
        {
            var chars = new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            var output = "";
            foreach (var c in input)
            {

                if (chars.Any(x => x == c))
                {
                    output += c;
                }
            }
            return output;
        }
    }
}
