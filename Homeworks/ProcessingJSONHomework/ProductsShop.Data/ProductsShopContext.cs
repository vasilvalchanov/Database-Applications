namespace ProductsShop.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    using ProductsShop.Data.Migrations;
    using ProductsShop.Models;

    public class ProductsShopContext : DbContext
    {
      
        public ProductsShopContext()
            : base("name=ProductsShopContext")
        {          
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ProductsShopContext, Configuration>());
        }

        public virtual IDbSet<User> Users { get; set; }

        public virtual IDbSet<Product> Products { get; set; }

        public virtual IDbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<User>().HasMany(u => u.Friends).WithMany().Map(
                m =>
                    {
                        m.MapLeftKey("UserId");
                        m.MapRightKey("FriendId");
                        m.ToTable("UserFriends");
                    });

            modelBuilder.Entity<User>()
                .HasMany(u => u.SoldProducts)
                .WithRequired(p => p.Seller);

            modelBuilder.Entity<User>()
                .HasMany(p => p.BoughtProducts)
                .WithOptional(p => p.Buyer);

            base.OnModelCreating(modelBuilder);
        }
    }
}