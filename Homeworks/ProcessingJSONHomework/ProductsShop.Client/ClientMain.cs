using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsShop.Client
{
    using System.IO;
    using System.Xml;
    using System.Xml.Linq;

    using Newtonsoft.Json;

    using ProductsShop.Data;

    using Formatting = Newtonsoft.Json.Formatting;

    class ClientMain
    {
        static void Main(string[] args)
        {
            var context = new ProductsShopContext();
            var count = context.Users.Count();

            ExtractProductsInRage(context, 500, 1000);
            ExtractUsersWithSoldProducts(context);
            ExtractCategoriesBuProductsCount(context);
            ExtractUsersAndProducts(context);
        }

        private static void ExtractProductsInRage(ProductsShopContext context, decimal firstPrice, decimal secondPrice)
        {
            var products =
                context.Products.Where(p => p.Price >= firstPrice && p.Price <= secondPrice && p.Buyer == null)
                    .Select(
                        p => new { name = p.Name, price = p.Price, seller = p.Seller.FirstName + " " + p.Seller.LastName })
                        .OrderBy(p => p.price);

            var serializable = JsonConvert.SerializeObject(products, Formatting.Indented);

            Console.WriteLine(serializable);

            File.WriteAllText("../../../query1.json", serializable);

        }

        private static void ExtractUsersWithSoldProducts(ProductsShopContext context)
        {
            var products =
                context.Users.Where(u => u.SoldProducts.Any(p => p.Buyer != null))
                    .Select(
                        u =>
                        new
                            {
                                firstName = u.FirstName ?? string.Empty,
                                lastName = u.LastName,
                                soldProducts =
                            u.SoldProducts.Where(p => p.Buyer != null)
                            .Select(
                                p =>
                                new
                                    {
                                        name = p.Name,
                                        price = p.Price,
                                        buyerFirstName = p.Buyer.FirstName ?? string.Empty,
                                        buyerLastName = p.Buyer.LastName
                                    })
                            
                            })
                    .OrderBy(u => u.lastName)
                    .ThenBy(u => u.firstName);

            var serializable = JsonConvert.SerializeObject(products, Formatting.Indented);

            Console.WriteLine(serializable);
            File.AppendAllText("../../../query2.json", serializable);
        }

        private static void ExtractCategoriesBuProductsCount(ProductsShopContext context)
        {
            var categories =
                context.Categories.Select(
                    c =>
                    new
                        {
                            name = c.Name,
                            productsCount = c.Products.Count,
                            averagePrice = c.Products.Average(p => p.Price),
                            totalRevenue = c.Products.Sum(p => p.Price)
                        }).OrderBy(c => c.productsCount);

            var serializable = JsonConvert.SerializeObject(categories, Formatting.Indented);

            Console.WriteLine(serializable);
            File.WriteAllText("../../../query3.json", serializable);
        }

        private static void ExtractUsersAndProducts(ProductsShopContext context)
        {
            var users =
                context.Users.Where(u => u.SoldProducts.Any(p => p.Buyer != null))
                    .Select(
                        u =>
                        new
                            {
                                firstName = u.FirstName,
                                lastName = u.LastName,
                                age = u.Age,
                                soldProducts =
                            u.SoldProducts.Where(p => p.Buyer != null)
                            .Select(p => new { productName = p.Name, price = p.Price })
                            })
                    .OrderByDescending(u => u.soldProducts.Count())
                    .ThenBy(u => u.lastName);

            var doc = new XDocument();
            var rootNode = new XElement("users");
            rootNode.SetAttributeValue("count", users.Count());
            

            foreach (var u in users)
            {
                var user = new XElement("user");

                if (u.firstName != null)
                {
                    user.SetAttributeValue("first-name", u.firstName);
                }

                user.SetAttributeValue("last-name", u.lastName);

                if (u.age != null)
                {
                    user.SetAttributeValue("age", u.age.Value);
                }

                var soldProducts = new XElement("sold-products");
                soldProducts.SetAttributeValue("count", u.soldProducts.Count());
                
                foreach (var product in u.soldProducts)
                {
                    var productName = new XElement("product");
                    productName.SetAttributeValue("name", product.productName);
                    productName.SetAttributeValue("price", product.price);
                    soldProducts.Add(productName);
                }

                user.Add(soldProducts);
                rootNode.Add(user);
            }

            doc.Add(rootNode);
            doc.Save("../../../ query4.xml");

        }
    }
}
