using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.TextFormatting;

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

        public GanreMusic GetGanre(GanreMusic temp)
        {
            return ganreMusics.Find(temp.Id);
        }

        public List<GanreMusic> GetListGanre(GanreMusic temp)
        {
            List<GanreMusic> list = new List<GanreMusic>();
            list.Add(ganreMusics.Find(temp.Id));
            return list;
        }

        public ExecutorMusic GetExecutors(ExecutorMusic temp)
        {
            return executorMusics.Find(temp.Id);
        }

        public List<ExecutorMusic> GetListExecutors(ExecutorMusic temp)
        {
            List<ExecutorMusic> list = new List<ExecutorMusic>();
            list.Add(executorMusics.Find(temp.Id));
            return list;
        }

        public List<Songs> GetSongs()
        {
            List<Songs> list = new List<Songs>();
            foreach(var item in songs.ToList())
            {
                Entry(item).Collection("ganreMusics").Load();
                Entry(item).Collection("executorMusics").Load();
                list.Add(item);
            }
            return list;
        }

        public Songs GetSong(Songs song)
        {
            return songs.Find(song.Id);
        }

        public PubishHouse GetPubishHouse(PubishHouse temp)
        {
            return pubishHouses.Find(temp.Id);
        }

        public List<Songs> GetSongsForRecord(List<Songs> temp)
        {
            List<Songs> list = new List<Songs>();
            foreach (var item in temp)
            {
                foreach (var song in songs)
                {
                    if(song.Id == item.Id)
                        list.Add(song);
                }
            }
            return list;

        }

    }
}
