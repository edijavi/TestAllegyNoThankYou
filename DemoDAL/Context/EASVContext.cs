using Microsoft.EntityFrameworkCore;
using DemoDAL.Entities;
using System.Collections.Generic;


namespace DemoDAL.Context
{
    public class EASVContext : DbContext
    {
        public EASVContext(DbContextOptions<EASVContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        /// String to connect with the DB
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=tcp:testallegynothankyou.database.windows.net,1433;Initial Catalog=TestAllegyNoThankYouDB;Persist Security Info=False;User ID=edijavi;Password=Javier8716;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }
        */


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductIngredient>()
                        .HasKey(pi => new { pi.IngredientId, pi.ProductId });

            modelBuilder.Entity<ProductIngredient>()
                        .HasOne(pi => pi.Ingredient)
                        .WithMany(p => p.Products)
                        .HasForeignKey(pi => pi.IngredientId);

            modelBuilder.Entity<ProductIngredient>()
                        .HasOne(pi => pi.Product)
                        .WithMany(i => i.Ingredients)
                        .HasForeignKey(pi => pi.ProductId);


            base.OnModelCreating(modelBuilder);
        }


    }
}