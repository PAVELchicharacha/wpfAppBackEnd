using Microsoft.EntityFrameworkCore;

namespace Lab3
{
    public class ModelDB : DbContext
    {
        public ModelDB(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Product>? products { get; set; }
        public DbSet<PriceList>? PriceLists { get; set; }
        public DbSet<User>? Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PriceList>().HasData(
                new PriceList { Id = 1, Name = "nucrealwearaponRU", Coast = 13 },
                new PriceList { Id = 2, Name = "nucrealwearaponUSA", Coast = 13 },
                new PriceList { Id = 3, Name = "nucrealwearaponCHI", Coast = 13 },
                new PriceList { Id = 4, Name = "nucrealwearaponFRA", Coast = 13 },
                new PriceList { Id = 5, Name = "nucrealwearaponKDR", Coast = 13 },
                new PriceList { Id = 6, Name = "nucrealwearaponUK", Coast = 13 },
                new PriceList { Id = 7, Name = "PolygonUA", Coast = 13 }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 8,

                    Quantity = 13,
                    ProductCoast = 123,
                    SaleDate = new DateTime(11 / 09 / 2020),
                    ProductSales = 5
                },
                new Product
                {
                    Id = 9,

                    Quantity = 11,
                    ProductCoast = 124,
                    SaleDate = new DateTime(11 / 09 / 2020),
                    ProductSales = 1
                },
                new Product
                {
                    Id = 10,

                    Quantity = 16,
                    ProductCoast = 125,
                    SaleDate = new DateTime(11 / 09 / 2020),
                    ProductSales = 1
                },
                new Product
                {
                    Id = 11,

                    Quantity = 13123,
                    ProductCoast = 126,
                    SaleDate = new DateTime(11 / 09 / 2020),
                    ProductSales = 1
                },
                new Product
                {
                    Id = 12,

                    Quantity = 465613,
                    ProductCoast = 127,
                    SaleDate = new DateTime(11 / 09 / 2020),
                    ProductSales = 1
                },
                new Product
                {
                    Id = 13,

                    Quantity = 1488,
                    ProductCoast = 128,
                    SaleDate = new DateTime(11 / 09 / 2020),
                    ProductSales = 1
                },
                new Product
                {
                    Id = 14,

                    Quantity = 55555,
                    ProductCoast = 129,
                    SaleDate = new DateTime(11 / 09 / 2020),
                    ProductSales = 1
                },
                new Product
                {
                    Id = 15,

                    Quantity = 16,
                    ProductCoast = 125,
                    SaleDate = new DateTime(11 / 09 / 2020),
                    ProductSales = 1
                },
                new Product
                {
                    Id = 16,

                    Quantity = 16,
                    ProductCoast = 125,
                    SaleDate = new DateTime(11 / 09 / 2020),
                    ProductSales = 1
                },
                new Product
                {
                    Id = 17,

                    Quantity = 16,
                    ProductCoast = 125,
                    SaleDate = new DateTime(11 / 09 / 2020),
                    ProductSales = 1
                },
                new Product
                {
                    Id = 18,

                    Quantity = 16,
                    ProductCoast = 125,
                    SaleDate = new DateTime(11 / 09 / 2020),
                    ProductSales = 1
                },
                new Product
                {
                    Id = 19,

                    Quantity = 16,
                    ProductCoast = 125,
                    SaleDate = new DateTime(11 / 09 / 2020),
                    ProductSales = 1
                },
                new Product
                {
                    Id = 20,

                    Quantity = 16,
                    ProductCoast = 125,
                    SaleDate = new DateTime(11 / 09 / 2020),
                    ProductSales = 1
                },
                new Product
                {
                    Id = 21,

                    Quantity = 16,
                    ProductCoast = 125,
                    SaleDate = new DateTime(11 / 09 / 2020),
                    ProductSales = 1
                },
                new Product
                {
                    Id = 22,

                    Quantity = 16,
                    ProductCoast = 125,
                    SaleDate = new DateTime(11 / 09 / 2020),
                    ProductSales = 1
                },
                new Product
                {
                    Id = 23,

                    Quantity = 16,
                    ProductCoast = 125,
                    SaleDate = new DateTime(11 / 09 / 1488),
                    ProductSales = 1
                }
                );
        }
    }
}