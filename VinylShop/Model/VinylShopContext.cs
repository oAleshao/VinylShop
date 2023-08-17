using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Media.TextFormatting;
using VinylShop.Model;
using MessageBox = System.Windows.MessageBox;

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
        public DbSet<HelpOrders> helpOrders { get; set; }

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
                if (user.Email == temp)
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
            foreach (var admin in admins)
            {
                if (admin.login == temp)
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

        public List<GanreMusic> GetListGanre(List<GanreMusic> temp)
        {
            List<GanreMusic> list = new List<GanreMusic>();
            foreach (var item in temp)
            {
                foreach (var ganre in ganreMusics)
                {
                    if (item.Id == ganre.Id)
                        list.Add(ganre);
                }
            }
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

        public List<ExecutorMusic> GetListExecutors(List<ExecutorMusic> temp)
        {
            List<ExecutorMusic> list = new List<ExecutorMusic>();
            foreach (var item in temp)
            {
                foreach (var executor in executorMusics)
                {
                    if (item.Id == executor.Id)
                        list.Add(executor);
                }
            }

            return list;
        }


        public List<Songs> GetSongs()
        {
            List<Songs> list = new List<Songs>();
            foreach (var item in songs.ToList())
            {
                Entry(item).Collection("ganreMusics").Load();
                Entry(item).Collection("executorMusics").Load();
                list.Add(item);
            }
            return list;
        }

        public List<Songs> GetListSongs()
        {
            List<Songs> list = new List<Songs>();
            foreach (var item in songs.ToList())
            {
                Entry(item).Collection("ganreMusics").Load();
                Entry(item).Collection("executorMusics").Load();
                list.Add(item);
            }
            return list;
        }

        public List<Songs> GetListSongs(List<Songs> songList)
        {
            List<Songs> list = new List<Songs>();
            foreach (var item in songList)
            {
                Entry(item).Collection("ganreMusics").Load();
                Entry(item).Collection("executorMusics").Load();
                list.Add(item);
            }
            return list;
        }


        public Songs GetSong(Songs song)
        {
            song = songs.Find(song.Id);
            Entry(song).Collection("ganreMusics").Load();
            Entry(song).Collection("executorMusics").Load();
            return song;
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
                    if (song.Id == item.Id)
                        list.Add(song);
                }
            }
            return list;

        }


        public void DelListExecutorsFromSong(List<ExecutorMusic> list, int IdSong)
        {
            Songs song = songs.Find(IdSong);
            bool help = false;
            foreach (var item in song.executorMusics.ToList())
            {
                foreach (var temp in list)
                {
                    if (temp.Id == item.Id)
                    {
                        help = true;
                        break;
                    }
                    help = false;
                }
                if (!help)
                    song.executorMusics.Remove(item);
            }

        }

        public void DelListGanresFromSong(List<GanreMusic> list, int IdSong)
        {
            Songs song = songs.Find(IdSong);
            bool help = false;
            foreach (var item in song.ganreMusics.ToList())
            {
                foreach (var temp in list)
                {
                    if (temp.Id == item.Id)
                    {
                        help = true;
                        break;
                    }
                    help = false;
                }
                if (!help)
                    song.ganreMusics.Remove(item);
            }

        }

        public void DelListSongsFromRecord(List<Songs> list, int IdRecord)
        {
            Music_Records record = music_Records.Find(IdRecord);
            bool help = false;
            foreach (var item in record.songs.ToList())
            {
                foreach (var temp in list)
                {
                    if (temp.Id == item.Id)
                    {
                        help = true;
                        break;
                    }
                    help = false;
                }
                if (!help)
                    record.songs.Remove(item);
            }
        }

        public List<Music_Records> GetListMusic_Records()
        {
            List<Music_Records> list = new List<Music_Records>();
            foreach (var records in music_Records.ToList())
            {
                Entry(records).Collection("songs").Load();
                Entry(records).Reference("pubishHouse").Load();
                list.Add(records);
            }
            return list;
        }

        public List<Music_Records> SearchMusicRecordsByName(string seacrhStr)
        {
            List<Music_Records> list = new List<Music_Records>();
            foreach(var record in music_Records.ToList())
            {
                if (record.Name.ToLower().Contains(seacrhStr.ToLower()))
                {
                    Entry(record).Reference("pubishHouse").Load();
                    list.Add(record);
                }
            }
            return list;
        }

        public List<Music_Records> SearchMusicRecordsByPublish(string seacrhStr)
        {
            List<Music_Records> list = new List<Music_Records>();
            foreach (var record in music_Records.ToList())
            {
                Entry(record).Reference("pubishHouse").Load();
                if (record.pubishHouse.Name.ToLower().Contains(seacrhStr.ToLower()))
                {
                    list.Add(record);
                }
            }
            return list;
        }

        public List<Songs> SearchMusicRecordsSongByName(int IdMusicRecords, string searchStr)
        {
            List<Songs> list = new List<Songs>();
            Music_Records records = music_Records.Find(IdMusicRecords);
            records.songs = GetListSongs(records.songs.ToList());
            foreach(var song in records.songs)
            {
                if (song.Name.ToLower().Contains(searchStr.ToLower()))
                    list.Add(song); 
            }
            return list;
        }

        public List<Songs> SearchMusicRecordsSongByGanre(int IdMusicRecords, string searchStr)
        {
            List<Songs> list = new List<Songs>();
            Music_Records records = music_Records.Find(IdMusicRecords);
            records.songs = GetListSongs(records.songs.ToList());
            foreach (var song in records.songs)
            {
                foreach (var ganre in song.ganreMusics.ToList())
                {
                    if (ganre.Name.ToLower().Contains(searchStr.ToLower()))
                    {
                        list.Add(song);
                        break;
                    }
                }
            }
            return list;
        }

        public List<Songs> SearchMusicRecordsSongByExecutor(int IdMusicRecords, string searchStr)
        {
            List<Songs> list = new List<Songs>();
            Music_Records records = music_Records.Find(IdMusicRecords);
            records.songs = GetListSongs(records.songs.ToList());
            foreach (var song in records.songs)
            {
                foreach (var executor in song.executorMusics.ToList())
                {
                    if (executor.Name.ToLower().Contains(searchStr.ToLower()))
                    {
                        list.Add(song);
                        break;
                    }
                }
            }
            return list;
        }

        public List<Songs> SearchSongsByName(string seacrhStr)
        {
            List<Songs> listSongs = new List<Songs>();
            foreach (var song in songs.ToList())
            {
                if (song.Name.ToLower().Contains(seacrhStr.ToLower()))
                {
                    Entry(song).Collection("ganreMusics").Load();
                    Entry(song).Collection("executorMusics").Load();
                    listSongs.Add(song);
                }
            }
            return listSongs;
        }

        public List<Songs> SearchSongsByGanre(string seacrhStr)
        {
            List<Songs> listSongs = new List<Songs>();
            foreach (var song in songs.ToList())
            {
                foreach (var ganre in song.ganreMusics.ToList())
                {
                    if (ganre.Name.ToLower().Contains(seacrhStr.ToLower()))
                    {
                        Entry(song).Collection("ganreMusics").Load();
                        Entry(song).Collection("executorMusics").Load();
                        listSongs.Add(song);
                        break;
                    }
                }
            }
            return listSongs;
        }

        public List<Songs> SearchSongsByExecutors(string seacrhStr)
        {
            List<Songs> listSongs = new List<Songs>();
            foreach (var song in songs.ToList())
            {
                foreach (var executor in song.executorMusics)
                {
                    if (executor.Name.ToLower().Contains(seacrhStr.ToLower()))
                    {
                        Entry(song).Collection("ganreMusics").Load();
                        Entry(song).Collection("executorMusics").Load();
                        listSongs.Add(song);
                        break;
                    }
                }
            }
            return listSongs;
        }

        public List<GanreMusic> SearchByGanres(string seacrhStr)
        {
            List<GanreMusic> list = new List<GanreMusic>();
            foreach(var ganre in ganreMusics.ToList())
            {
                if (ganre.Name.ToLower().Contains(seacrhStr.ToLower()))
                {
                    list.Add(ganre);
                }
            }
            return list;
        }

        public List<ExecutorMusic> SearchByExecutors(string seacrhStr)
        {
            List<ExecutorMusic> list = new List<ExecutorMusic>();
            foreach (var executor in executorMusics.ToList())
            {
                if (executor.Name.ToLower().Contains(seacrhStr.ToLower()))
                {
                    list.Add(executor);
                }
            }
            return list;
        }

        public List<PubishHouse> SearchByPublishHouse(string seacrhStr)
        {
            List<PubishHouse> list = new List<PubishHouse>();
            foreach(var publish in pubishHouses.ToList())
            {
                if (publish.Name.ToLower().Contains(seacrhStr.ToLower()))
                {
                    list.Add(publish);
                }
            }
            return list;
        }

        public List<Music_Records> GetListMusicRecordsForShopBag(int IdUser)
        {
            List<Music_Records> list = new List<Music_Records>();
            foreach(var item in shopBags.ToList())
            {
                if(item.IdUser.Id == IdUser) 
                {
                    Entry(item.music_Records).Reference("pubishHouse").Load();
                    list.Add(item.music_Records);
                }
            }
            return list;
        }

        public bool AddMusicRecordsToShopBag(ShopBag bag, Users user)
        {
            foreach(var item in shopBags.ToList())
            {
                if(user.Id == item.IdUser.Id)
                {
                    if (CompareToMusicRecords(item.music_Records, bag.music_Records))
                        return false;
                }
            }
            shopBags.Add(bag);
            SaveChanges();
            return true;
        }

        public void DelMusicRecordFromShopBag(Music_Records records, Users user)
        {
            foreach (var item in shopBags.ToList())
            {
                if(item.IdUser.Id == user.Id)
                {
                    if(CompareToMusicRecords(records, item.music_Records))
                    {
                        shopBags.Remove(item);
                        SaveChanges();
                        return;
                    }
                }
            }
        }

        public bool CompareToMusicRecords(Music_Records first, Music_Records second)
        {
            return first.Name == second.Name && first.pubishHouse.Name == second.pubishHouse.Name && first.CountSongs == second.CountSongs
                && first.Quantity == second.Quantity && first.Year == second.Year && first.Price == second.Price;
        }

        public bool IsMusicRecordInList(List<Music_Records> list, Music_Records music)
        {
            foreach(var item in list)
            {
                if(CompareToMusicRecords(item, music))
                    return true;
            }
            return false;
        }

        public List<Orders> GetListOrders(Users user)
        {
            List<Orders> list = new List<Orders>();

            foreach(var item in orders.ToList())
            {
                if (item.Iduser.Id == user.Id)
                {
                    Entry(item).Collection("helpOrders_").Load();
                    list.Add(item);
                }
            }
            return list;
        }


        public List<HelpOrders> GetListHelpOrders(Orders order)
        {
            order = orders.Find(order.Id);
            List<HelpOrders> list = new List<HelpOrders>();
            foreach (var item in order.helpOrders_.ToList()) 
            {
                Entry(item).Reference("music_Records").Load();
                list.Add(item);
            }
            return list;
        }

        public List<Music_Records> Get5NewMusicRecords()
        {
            List<Music_Records> Rlist = music_Records.ToList();
            Rlist.Reverse(0, Rlist.Count);
            List<Music_Records> list = new List<Music_Records>();
            int helpIndx = 0;
            foreach (var item in Rlist) 
            {
                if (helpIndx == 5)
                    break;
                Entry(item).Reference("pubishHouse").Load();
                list.Add(item); 
                helpIndx++;
            }
            return list;
        }

        public List<Music_Records> GetTopSellMusicRecords()
        {
            List<Music_Records> list = new List<Music_Records>();
            int indxHelp = 0;
            Music_Records record = new Music_Records();
            var temp = helpOrders.GroupBy(x => x.music_Records.Id).Select(g => new { id = g.Key, Count = g.Count()}).OrderBy(x=> -x.Count);
            foreach (var item in temp.ToList())
            {
                if (indxHelp == 5)
                    break;
                record = music_Records.Find(item.id);
                Entry(record).Reference("pubishHouse").Load();
                list.Add(record);
            }


            return list;
        }

        public List<Music_Records> GetTopExecutors()
        {
            var temp = executorMusics.OrderBy(x => -x.songs.Count).Take(5).ToList();

            List<Music_Records> list = new List<Music_Records>();

            foreach (var item in temp)
            {
                foreach (var item2 in item.songs)
                {
                    foreach(var record in item2.music_Records)
                    {
                        Entry(record).Reference("pubishHouse").Load();
                        if (!IsMusicRecordInList(list, record))
                            list.Add(record);
                    }
                }
            }

            return list;
        }

        public List<Music_Records> GetTopGanres()
        {
            var temp = ganreMusics.OrderBy(x => -x.songs.Count).Take(5).ToList();
            List<Music_Records> list = new List<Music_Records>();

            foreach (var item in temp)
            {
                foreach (var item2 in item.songs)
                {
                    foreach (var record in item2.music_Records)
                    {
                        Entry(record).Reference("pubishHouse").Load();
                        if (!IsMusicRecordInList(list, record))
                            list.Add(record);
                    }
                }
            }

            return list;
        }

    }



}
