using Microsoft.EntityFrameworkCore;
using MyShop.Models;
using System.Data;

namespace MyShop.Data
{
    public class MyShopContext : DbContext
    {
        public MyShopContext(DbContextOptions<MyShopContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<CategoryToProduct> CategoryToProducts { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<CategoryToProduct>().HasKey(t => new { t.ProductId, t.CategoryId });
            //modelBuilder.Entity<CategoryToProduct>().HasKey(t => new { t.CategoryId });


            #region Cheng Type In Data Base

            modelBuilder.Entity<Item>(i =>
            {
                //i.HasKey(w => w.Id);
                i.Property(w => w.Price).HasColumnType("money");
            });


            //modelBuilder.Entity<Item>(i =>
            //{
            //    i.HasKey(w => w.Id);
            //    i.Property(w => w.Price).HasColumnType("DECIMAL(10, 2)");
            //});

            
            #endregion



            #region Seed Data
            modelBuilder.Entity<Category>().HasData(new Category()
            {
                Id = 1,
                Name = "soheil0910",
                Description = "Creator",
            }, new Category()
            {
                Id = 2,
                Name = "mobile",
                Description = "G750",
            }, new Category()
            {
                Id = 3,
                Name = "mobile",
                Description = "Honor 8",
            }, new Category()
            {
                Id = 4,
                Name = "لب تاب",
                Description = "فقط کدنویسی",
            }, new Category()
            {
                Id = 5,
                Name = "شرکت ها",
                Description = "تعداد زیاد ",
            }, new Category()
            {
                Id = 6,
                Name = "سنعت هایی که کار میکند",
                Description = "بیشتر سنعت های بروز جهان",
            }

            );

            //---------------------------------------------------------

            modelBuilder.Entity<Item>().HasData(
                new Item()
                {
                    Id = 1,
                    Price = 214,
                    QuantityInStock = 8,
                }, new Item()
                {
                    Id = 2,
                    Price = 250,
                    QuantityInStock = 7,
                }, new Item()
                {
                    Id = 3,
                    Price = 2412,
                    QuantityInStock = 6,
                }, new Item()
                {
                    Id = 4,
                    Price = 2500.02m,
                    QuantityInStock = 5,
                }, new Item()
                {
                    Id = 5,
                    Price = 3500,
                    QuantityInStock = 4,
                }, new Item()
                {
                    Id = 6,
                    Price = 9800,
                    QuantityInStock = 3,
                }


                );

            //---------------------------------------------------------

            modelBuilder.Entity<Product>().HasData(

                new Product()
                {
                    Id = 1,
                    ItemId = 1,
                    Name = "28",
                    Description = "نصب و راه اندازی BootStrap4 رو پروژه فروشگاه",
                }, new Product()
                {
                    Id = 2,
                    ItemId = 2,
                    Name = "32",
                    Description = "ساخت جداول فروشگاه با Code First - بخش دوم",
                }, new Product()
                {
                    Id = 3,
                    ItemId = 3,
                    Name = "43",
                    Description = "معرفی Razor Pages و ساخت ادمین فروشگاه با Razor Pages",
                }, new Product()
                {
                    Id = 4,
                    ItemId = 4,
                    Name = "گوشی",
                    Description = "قوی ترین پردازنده جهان بسیار خنک با باتری اتمی و حافظه رم در حد کامپیوتر",
                }, new Product()
                {
                    Id = 5,
                    ItemId = 5,
                    Name = "گوشی",
                    Description = "گوشی با چند سیستم امل و قابلیت نصب هر گونه رام و دوربین فوقولاده ",
                }, new Product()
                {
                    Id = 6,
                    ItemId = 6,
                    Name = "ماشین",
                    Description = "نگران تمام شدن سوخت نباشید دارای برق شهری و ابشن های جلو تر از تکنولوژی",
                }



                );
            //---------------------------------------------------------

            modelBuilder.Entity<CategoryToProduct>().HasData(

             new CategoryToProduct() { CategoryId = 1, ProductId = 1 },
             new CategoryToProduct() { CategoryId = 1, ProductId = 2 },
             new CategoryToProduct() { CategoryId = 1, ProductId = 3 },
             new CategoryToProduct() { CategoryId = 1, ProductId = 4 },
             new CategoryToProduct() { CategoryId = 1, ProductId = 5 },
             new CategoryToProduct() { CategoryId = 1, ProductId = 6 },

             new CategoryToProduct() { CategoryId = 2, ProductId = 1 },
             new CategoryToProduct() { CategoryId = 2, ProductId = 2 },
             new CategoryToProduct() { CategoryId = 2, ProductId = 3 },
             new CategoryToProduct() { CategoryId = 2, ProductId = 4 },
             new CategoryToProduct() { CategoryId = 2, ProductId = 5 },
             new CategoryToProduct() { CategoryId = 2, ProductId = 6 },

             new CategoryToProduct() { CategoryId = 3, ProductId = 1 },
             new CategoryToProduct() { CategoryId = 3, ProductId = 2 },
             new CategoryToProduct() { CategoryId = 3, ProductId = 3 },
             new CategoryToProduct() { CategoryId = 3, ProductId = 4 },
             new CategoryToProduct() { CategoryId = 3, ProductId = 5 },
             new CategoryToProduct() { CategoryId = 3, ProductId = 6 },

             new CategoryToProduct() { CategoryId = 4, ProductId = 1 },
             new CategoryToProduct() { CategoryId = 4, ProductId = 2 },
             new CategoryToProduct() { CategoryId = 4, ProductId = 3 },
             new CategoryToProduct() { CategoryId = 4, ProductId = 4 },
             new CategoryToProduct() { CategoryId = 4, ProductId = 5 },
             new CategoryToProduct() { CategoryId = 4, ProductId = 6 },

             new CategoryToProduct() { CategoryId = 5, ProductId = 1 },
             new CategoryToProduct() { CategoryId = 5, ProductId = 2 },
             new CategoryToProduct() { CategoryId = 5, ProductId = 3 },
             new CategoryToProduct() { CategoryId = 5, ProductId = 4 },
             new CategoryToProduct() { CategoryId = 5, ProductId = 5 },
             new CategoryToProduct() { CategoryId = 5, ProductId = 6 }






             );




            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
