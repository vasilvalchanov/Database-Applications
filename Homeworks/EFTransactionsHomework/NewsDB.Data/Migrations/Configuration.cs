namespace NewsDB.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using NewsDB.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<NewsDBContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationDataLossAllowed = false;
            this.AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(NewsDBContext context)
        {
            if (!context.News.Any())
            {
                context.News.Add(new News() { NewsContent = "Nqkva si tam novina" });
                context.News.Add(new News() { NewsContent = "Portugaliq evropeiski shampion" });
                context.News.Add(new News() { NewsContent = "Ronaldo smotan" });
                context.News.Add(new News() { NewsContent = "Mnogo skuchna novina" });
                context.News.Add(new News() { NewsContent = "Kichka Bodurova s nova pesen" });
                context.News.Add(new News() { NewsContent = "Azis se omaji" });
                context.SaveChanges();
            }
           
        }
    }
}
