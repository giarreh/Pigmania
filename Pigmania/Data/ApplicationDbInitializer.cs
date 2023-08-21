using Microsoft.AspNetCore.Identity;
using Pigmania.Models;

namespace Pigmania.Data
{
    public class ApplicationDbInitializer
    {
        public static void Initialize(ApplicationDbContext db, UserManager<ApplicationUser> um, 
            RoleManager<IdentityRole> rm)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            
            

            var adminRole = new IdentityRole("Admin");
            rm.CreateAsync(adminRole).Wait();
            
            var admin = new ApplicationUser {UserName = "Pigmania@uia.no", Email = "Pigmania@uia.no", 
                EmailConfirmed = true,Playername = "PigMaster", TruffleCoins = 100};
            um.CreateAsync(admin, "Pigmania1.").Wait();
            um.AddToRoleAsync(admin, "Admin");

            var user = new ApplicationUser {UserName = "oskar@uia.no", Email = "oskar@uia.no", 
                EmailConfirmed = true, Playername = "Oskar", TruffleCoins = 150, TruffleClickPower = 1};
            um.CreateAsync(user, "Oskar1.").Wait();
            var user1 = new ApplicationUser {UserName = "lars@uia.no", Email = "lars@uia.no", 
                EmailConfirmed = true,Playername = "Lars", TruffleCoins = 250, TruffleClickPower = 1};
            um.CreateAsync(user1, "Lars1.").Wait();
            var user2 = new ApplicationUser {UserName = "markus@uia.no", Email = "markus@uia.no", 
                EmailConfirmed = true,Playername = "Markus", TruffleCoins = 200, TruffleClickPower = 1};
            um.CreateAsync(user2, "Markus1.").Wait();
            var user3 = new ApplicationUser {UserName = "giar@uia.no", Email = "giar@uia.no", 
                EmailConfirmed = true,Playername = "Giar", TruffleCoins = 300, TruffleClickPower = 1};
            um.CreateAsync(user3, "Giar1.").Wait();
            var user4 = new ApplicationUser {UserName = "shaheen@uia.no", Email = "shaheen@uia.no", 
                EmailConfirmed = true,Playername = "Shaheen", TruffleCoins = 400, TruffleClickPower = 1};
            um.CreateAsync(user4, "Shaheen1.").Wait();
            var user5 = new ApplicationUser {UserName = "anders@uia.no", Email = "anders@uia.no", 
                EmailConfirmed = true,Playername = "Anders", TruffleCoins = 350, TruffleClickPower = 1};
            um.CreateAsync(user5, "Anders1.").Wait();

            db.SaveChanges();

            var pigs = new[]
            {
                new Pig("Oskar",20),
                new Pig("Lars",14),
                new Pig("Markus",13),
                new Pig("Giar", 14),
                new Pig("Shaheen", 15),
                new Pig("Anders", 10)
            };
            
            db.Pigs.AddRange(pigs);
            db.SaveChanges();
        }
    }
}