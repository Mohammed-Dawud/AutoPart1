
using EcommerceAutoPart.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAutoPart.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Brand> tblBrands { get; set; }
        public DbSet<Car> tblCars { get; set; }
        public DbSet<AutoPart> tblAutoParts { get; set; }
        public DbSet<ApplicationUser> tblApplicationUsers { get; set; }
        public DbSet<Company> tblCompanies { get; set; }

        public DbSet<ShoppingCart> tblShoppingCarts { get; set; }
        public DbSet<OrderHeader> tblOrderHeaders { get; set; }
        public DbSet<OrderDetail> tblOrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Disable Cascade Delete for the Car foreign key
            modelBuilder.Entity<ShoppingCart>()
                .HasOne(s => s.Car)
                .WithMany()
                .HasForeignKey(s => s.CarId)
                .OnDelete(DeleteBehavior.Restrict); // or DeleteBehavior.Restrict

            // You may need to repeat this for other foreign keys if necessary
            // For example, if AutoPart also has cascade delete, disable it similarly:
            modelBuilder.Entity<ShoppingCart>()
                .HasOne(s => s.AutoPart)
                .WithMany()
                .HasForeignKey(s => s.AutoPartId)
                .OnDelete(DeleteBehavior.Restrict); // or DeleteBehavior.Restrict
            modelBuilder.Entity<ShoppingCart>()
        .HasOne(s => s.ApplicationUser)
        .WithMany()
        .HasForeignKey(s => s.ApplicationUserId)
        .OnDelete(DeleteBehavior.Restrict);  // or DeleteBehavior.NoAction

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Brand>().HasData(
                new Brand { Id = 1, BrandName = "Honda", BrandPhoto = "/images/Honda-Logo.jpg", DisplayOrder = 1 },
                new Brand { Id = 2, BrandName = "Mitsubishi", BrandPhoto = "/images/Mitsubishilogo.jpg", DisplayOrder = 2 },
                new Brand { Id = 3, BrandName = "Toyota", BrandPhoto = "/images/ToyotaLogo.jpg", DisplayOrder = 3 }
                );
            modelBuilder.Entity<Car>().HasData(
               new Car { BrandId = 1, Id = 1, CarName = "Honda Civic 2006", CarPhoto = "/images/HondaCivic2006.jpg" },
               new Car { BrandId = 1, Id = 2, CarName = "Honda Accord 2006", CarPhoto = "/images/hondaAccord2006.jpg" },
               new Car { BrandId = 2, Id = 3, CarName = "Mitsubishi Lancer 2008", CarPhoto = "/images/mitsubishiLancer2008.jpg" },
               new Car { BrandId = 2, Id = 4, CarName = "Mitsubishi Eclipse 2007", CarPhoto = "/images/mitsubishiEclipse2007.jpg" },
               new Car { BrandId = 3, Id = 5, CarName = "Toyota Camry 2010", CarPhoto = "/images/toyotaCamry2010.jpg" },
               new Car { BrandId = 3, Id = 6, CarName = "Toyota Corolla 2010", CarPhoto = "/images/toyotaCorolla2010.jpg" }
               );

            modelBuilder.Entity<Company>().HasData(
            new Company { Id = 1, Name = "tech Solution", StreetAddress = "123 Tech St", City = "Amman", PostalCode = "11212", State = "IL", PhoneNumber = "078945752" },
            new Company { Id = 2, Name = "Sleep", StreetAddress = "456 WOW St", City = "Irbid", PostalCode = "53215", State = "IR", PhoneNumber = "079868210" },
            new Company { Id = 3, Name = "Wakeup", StreetAddress = "789 LOL St", City = "Aqaba", PostalCode = "32053", State = "AQ", PhoneNumber = "0778457805" }
            );
            modelBuilder.Entity<AutoPart>().HasData(
                new AutoPart
                {
                    Id = 1,
                    CarId = 1,
                    AutoPartName = "Civic",
                    AutoPartDescription = "Description",
                    AutoPartPhoto = "/images/Lights.jpg",
                    ListPrice = 99,
                    Price = 90,
                    Price50 = 85,
                    Price100 = 80,
                    NumberOfPieces = 1




                },
                    new AutoPart
                    {
                        Id = 2,
                        CarId = 2,
                        AutoPartName = "SCITOO",
                        AutoPartDescription = "Description",
                        AutoPartPhoto = "/images/SCITOO.jpg",
                        ListPrice = 99,
                        Price = 90,
                        Price50 = 85,
                        Price100 = 80,
                        NumberOfPieces = 1




                    },
                    new AutoPart
                    {
                        Id = 3,
                        CarId = 1,
                        AutoPartName = "Rear Tail",
                        AutoPartDescription = "Description",
                        AutoPartPhoto = "/images/battery.png",
                        ListPrice = 99,
                        Price = 90,
                        Price50 = 85,
                        Price100 = 80,
                        NumberOfPieces = 1




                    },
                    new AutoPart
                    {
                        Id = 4,
                        CarId = 2,
                        AutoPartName = "Suspension Kit",
                        AutoPartDescription = "Description",
                        AutoPartPhoto = "/images/HondaCivic-doorHandle.jpeg",

                        ListPrice = 99,
                        Price = 90,
                        Price50 = 85,
                        Price100 = 80,
                        NumberOfPieces = 1




                    },
                    new AutoPart
                    {
                        Id = 5,
                        CarId = 2,
                        AutoPartName = "Sedan",
                        AutoPartDescription = "Description",
                        AutoPartPhoto = "/images/KIAAutopart.jpg",
                        ListPrice = 99,
                        Price = 90,
                        Price50 = 85,
                        Price100 = 80,
                        NumberOfPieces = 1



                    },
                    new AutoPart
                    {
                        Id = 6,
                        CarId = 4,
                        AutoPartName = "Lower Control Arm",
                        AutoPartDescription = "Description",
                        AutoPartPhoto = "/images/SCITOO.jpg",
                        ListPrice = 99,
                        Price = 90,
                        Price50 = 85,
                        Price100 = 80,
                        NumberOfPieces = 1





                    });
        }

    }
}
