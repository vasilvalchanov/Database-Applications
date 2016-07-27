namespace ProductsShop.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Json;
    using System.Xml.Linq;

    using Newtonsoft.Json;

    using ProductsShop.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ProductsShop.Data.ProductsShopContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationDataLossAllowed = false;
            this.AutomaticMigrationsEnabled = true;
            this.ContextKey = "ProductsShop.Data.ProductsShopContext";
        }

        protected override void Seed(ProductsShopContext context)
        {
            const string UserPath = "../../../users.xml";
            const string ProductsPath = "../../../products.json";
            const string CategoriesPath = "../../../categories.json";

            if (context.Users.Any())
            {
                return;
            }

            this.SeedUsers(context, UserPath);
            context.SaveChanges();
            this.SeedProducts(context, ProductsPath);
            context.SaveChanges();
            this.SeedCategories(context, CategoriesPath);
            context.SaveChanges();
            this.SeedProductCategories(context);
            context.SaveChanges();
        }

        private void SeedProductCategories(ProductsShopContext context)
        {
            var categories = context.Categories.ToList();
            var products = context.Products.ToList();
            var random = new Random();

            foreach (var product in products)
            {
                var category = categories[random.Next(0, categories.Count)];
                product.Categories.Add(category);
                category.Products.Add(product);
            }

            foreach (var category in categories)
            {
                var product = products[random.Next(0, products.Count)];
                category.Products.Add(product);
                product.Categories.Add(category);
            }

            foreach (var product in products)
            {
                context.Products.AddOrUpdate(product);
            }

            foreach (var category in categories)
            {
                context.Categories.AddOrUpdate(category);
            }
        }

        private void SeedCategories(ProductsShopContext context, string categoriesPath)
        {
            var deserializedCategories = JsonConvert.DeserializeObject<List<Category>>(File.ReadAllText(categoriesPath));

            foreach (var category in deserializedCategories)
            {
                context.Categories.Add(category);
            }
        }

        private void SeedProducts(ProductsShopContext context, string productsPath)
        {
            var deserializedProducts = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(productsPath));
            var usersIds = context.Users.Select(u => u.Id).ToList();
            var random = new Random();

            for (int i = 0; i < deserializedProducts.Count; i++)
            {
                deserializedProducts[i].SellerId = usersIds[random.Next(0, usersIds.Count)];
                var buyerId = usersIds[random.Next(0, usersIds.Count)];

                if (i % 2 == 0 && deserializedProducts[i].SellerId != buyerId)
                {
                    deserializedProducts[i].BuyerId = buyerId;
                }
            }

            foreach (var product in deserializedProducts)
            {
                context.Products.Add(product);
            }
        }

        private void SeedUsers(ProductsShopContext context, string userPath)
        {
            XDocument xDocument = XDocument.Load(userPath);
             var users = xDocument.Descendants("user")
                .Select(
                    u =>
                    new User
                        {
                            FirstName = u.Attribute("first-name") != null ? u.Attribute("first-name").Value : null,
                            LastName = u.Attribute("last-name").Value,
                            Age = u.Attribute("age") != null ? int.Parse(u.Attribute("age").Value) : (int?)null
                        });

            foreach (var user in users)
            {
                context.Users.Add(user);
            }
        }
    }
}
