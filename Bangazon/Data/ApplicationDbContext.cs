using System;
using System.Collections.Generic;
using System.Text;
using Bangazon.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bangazon.Data {
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base (options) { }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<PaymentType> PaymentType { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            modelBuilder.Entity<Order>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("GETDATE()");

            // Restrict deletion of related order when OrderProducts entry is removed
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderProducts)
                .WithOne(l => l.Order)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("GETDATE()");

            // Restrict deletion of related product when OrderProducts entry is removed
            modelBuilder.Entity<Product>()
                .HasMany(o => o.OrderProducts)
                .WithOne(l => l.Product)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PaymentType>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("GETDATE()");

            ApplicationUser user = new ApplicationUser {
                FirstName = "Buyer",
                LastName = "User",
                StreetAddress = "123 Buyer Way",
                UserName = "buyer@admin.com",
                NormalizedUserName = "BUYER@ADMIN.COM",
                Email = "buyer@admin.com",
                NormalizedEmail = "buyer@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };


            ApplicationUser user2 = new ApplicationUser
            {
                FirstName = "Seller",
                LastName = "User",
                StreetAddress = "123 Seller Way",
                UserName = "seller@admin.com",
                NormalizedUserName = "SELLER@ADMIN.COM",
                Email = "seller@admin.com",
                NormalizedEmail = "seller@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };


            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user);

            var passwordHash2 = new PasswordHasher<ApplicationUser>();
            user2.PasswordHash = passwordHash2.HashPassword(user2, "Admin10*");
            modelBuilder.Entity<ApplicationUser>().HasData(user2);

            modelBuilder.Entity<PaymentType>().HasData(
                new PaymentType() {
                    PaymentTypeId = 1,
                    UserId = user.Id,
                    Description = "American Express",
                    AccountNumber = "86753095551212"
                },
                new PaymentType()
                {
                    PaymentTypeId = 4,
                    UserId = user.Id,
                    Description = "Chase",
                    AccountNumber = "9999111122220000"
                },
                        new PaymentType()
                        {
                            PaymentTypeId = 3,
                            UserId = user.Id,
                            Description = "VISA",
                            AccountNumber = "1111222233334444"
                        },
                        new PaymentType()
                        {
                            PaymentTypeId = 2,
                            UserId = user.Id,
                            Description = "Discover",
                            AccountNumber = "4102948572991"
                        }
                    );

            modelBuilder.Entity<ProductType>().HasData(
                new ProductType()
                {
                    ProductTypeId = 1,
                    Label = "Sporting Goods"
                },
                
                new ProductType()
                {
                    ProductTypeId = 3,
                    Label = "Toys"
                },
                
                new ProductType()
                {
                    ProductTypeId = 4,
                    Label = "Furniture"
                },
                new ProductType()
                {
                    ProductTypeId = 2,
                    Label = "Appliances"
                }

                 );

            modelBuilder.Entity<Product> ().HasData (
                new Product()
                {
                    ProductId = 1,
                    ProductTypeId = 1,
                    UserId = user.Id,
                    Description = "It flies high",
                    Title = "Kite",
                    Quantity = 100,
                    Price = 2.99
                },
                new Product()
                {
                    ProductId = 2,
                    ProductTypeId = 1,
                    UserId = user.Id,
                    Description = "Aluminum",
                    Title = "Baseball bat",
                    Quantity = 25,
                    Price = 14.99
                },
                new Product()
                {
                    ProductId = 3,
                    ProductTypeId = 1,
                    UserId = user.Id,
                    Description = "It floats",
                    Title = "Kayak",
                    Quantity = 6,
                    Price = 120.00
                },
                new Product()
                {
                    ProductId = 4,
                    ProductTypeId = 1,
                    UserId = user.Id,
                    Description = "Kobeeeee",
                    Title = "Basketball",
                    Quantity = 24,
                    Price = 201.99
                },
                new Product()
                {
                    ProductId = 5,
                    ProductTypeId = 1,
                    UserId = user.Id,
                    Description = "It does tennis things",
                    Title = "Tennis Racket",
                    Quantity = 19,
                    Price = 15.00
                },
                 new Product()
                 {
                     ProductId = 6,
                     ProductTypeId = 2,
                     UserId = user.Id,
                     Description = "It washes",
                     Title = "Dishwasher",
                     Quantity = 4,
                     Price = 200.00
                 },
                 new Product()
                 {
                     ProductId = 7,
                     ProductTypeId = 2,
                     UserId = user.Id,
                     Description = "waste of money",
                     Title = "Quesadilla Maker",
                     Quantity = 190,
                     Price = 74.00
                 },
                 new Product()
                 {
                     ProductId = 8,
                     ProductTypeId = 2,
                     UserId = user.Id,
                     Description = "It's little",
                     Title = "Toaster",
                     Quantity = 32,
                     Price = 6.99
                 },
                 new Product()
                 {
                     ProductId = 9,
                     ProductTypeId = 2,
                     UserId = user.Id,
                     Description = "Ears sold seperately",
                     Title = "George Foreman Grill",
                     Quantity = 35,
                     Price = 19.00
                 },
                 new Product()
                 {
                     ProductId = 10,
                     ProductTypeId = 2,
                     UserId = user.Id,
                     Description = "you need it",
                     Title = "Fridge",
                     Quantity = 60,
                     Price = 300.00
                 },
                 new Product()
                 {
                     ProductId = 11,
                     ProductTypeId = 3,
                     UserId = user.Id,
                     Description = "you shoot it",
                     Title = "Nerf Gun",
                     Quantity = 600,
                     Price = 10.00
                 },
                 new Product()
                 {
                     ProductId = 12,
                     ProductTypeId = 3,
                     UserId = user.Id,
                     Description = "fast fun",
                     Title = "RC Car",
                     Quantity = 10,
                     Price = 70.00
                 },
                 new Product()
                 {
                     ProductId = 13,
                     ProductTypeId = 3,
                     UserId = user.Id,
                     Description = "don't step on me, build me",
                     Title = "Legos",
                     Quantity = 12,
                     Price = 80.00
                 },
                 new Product()
                 {
                     ProductId = 14,
                     ProductTypeId = 3,
                     UserId = user.Id,
                     Description = "Disclamer: we won't cover your divorce costs",
                     Title = "Monopoly",
                     Quantity = 1,
                     Price = 30.00
                 },
                 new Product()
                 {
                     ProductId = 15,
                     ProductTypeId = 3,
                     UserId = user.Id,
                     Description = "you blow bubbles with it",
                     Title = "Bubble Wand",
                     Quantity = 60,
                     Price = 2.00
                 },
                 new Product()
                 {
                     ProductId = 16,
                     ProductTypeId = 4,
                     UserId = user.Id,
                     Description = "Scary but won't eat you",
                     Title = "Bear Rug",
                     Quantity = 60,
                     Price = 450.00
                 },
                 new Product()
                 {
                     ProductId = 17,
                     ProductTypeId = 4,
                     UserId = user.Id,
                     Description = "Because sitting is fun",
                     Title = "Wood Bench",
                     Quantity = 25,
                     Price = 200.00
                 },
                 new Product()
                 {
                     ProductId = 18,
                     ProductTypeId = 4,
                     UserId = user.Id,
                     Description = "Babies go here",
                     Title = "Crib",
                     Quantity = 66,
                     Price = 666.00
                 },
                 new Product()
                 {
                     ProductId = 19,
                     ProductTypeId = 4,
                     UserId = user.Id,
                     Description = "Orange, suede, and amazingly ugly",
                     Title = "Couch",
                     Quantity = 1,
                     Price = 700.00
                 },
                 new Product()
                 {
                     ProductId = 20,
                     ProductTypeId = 4,
                     UserId = user.Id,
                     Description = "Cheap but impossible to put together",
                     Title = "IKEA Shelf",
                     Quantity = 99,
                     Price = 20.00
                 }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order()
                {
                    OrderId = 1,
                    UserId = user.Id,
                    PaymentTypeId = null
                }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order()
                {
                    OrderId = 2,
                    UserId = user.Id,
                    PaymentTypeId = 1
                }
            );

            modelBuilder.Entity<OrderProduct>().HasData(
                new OrderProduct()
                {
                    OrderProductId = 1,
                    OrderId = 1,
                    ProductId = 1
                }
            );

            modelBuilder.Entity<OrderProduct>().HasData(
                new OrderProduct()
                {
                    OrderProductId = 2,
                    OrderId = 2,
                    ProductId = 2
                }
            );

        }
    }
}