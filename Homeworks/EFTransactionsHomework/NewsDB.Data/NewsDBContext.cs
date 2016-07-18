namespace NewsDB.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using NewsDB.Data.Migrations;
    using NewsDB.Models;

    public class NewsDBContext : DbContext
    {
        
        public NewsDBContext()
            : base("name=NewsDBContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NewsDBContext, Configuration>());
        }

        public IDbSet<News> News { get; set; }

    }  
}