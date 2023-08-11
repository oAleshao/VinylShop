using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinylShop.Model
{
    public class VinylShopContext : DbContext
    {
        public DbSet<ExecutorMusic> executorMusics { get; set; }
        public DbSet<GanreMusic> ganreMusics { get; set; }
        public DbSet<Music_Records> music_Records { get; set; }
        public DbSet<Orders> orders { get; set; }
        public DbSet<PubishHouse> pubishHouses { get; set; }
        public DbSet<ShopBag> shopBags { get; set; }
        public DbSet<Songs> songs { get; set; }
        public DbSet<Users> users { get; set; }
        public DbSet<Admins> admins { get; set; }
        public DbSet<Product> products { get; set; }

        public VinylShopContext() : base("DbVinylShop") { }

        public void AddUser(Users newUser)
        {
            if (newUser != null)
            {
                users.Add(newUser);
                SaveChanges();
            }
        }

        public bool CheckUserEmail(string temp)
        {
            foreach (var user in users)
            {
                if(user.Email == temp)
                    return true;
            }
            return false;
        }
        public bool CheckUserLogin(string temp)
        {
            foreach (var user in users)
            {
                if (user.Login == temp)
                    return true;
            }
            return false;
        }
        public bool CheckUserPhone(string temp)
        {
            foreach (var user in users)
            {
                if (user.PhoneNumber == temp)
                    return true;
            }
            return false;
        }
        public Users CheckUserLogAndEmail(string temp)
        {

            foreach (var user in users)
            {
                if (user.Login == temp || user.Email == temp)
                    return user;
            }
            return null;
        }
        public Admins CheckAdminLogin(string temp)
        {
            foreach(var admin in admins)
            {
                if(admin.login == temp)
                    return admin;
            }
            return null;
        }
    }
}
