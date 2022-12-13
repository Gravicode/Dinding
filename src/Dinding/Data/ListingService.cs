using Microsoft.EntityFrameworkCore;
using Dinding.Data;
using Dinding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace Dinding.Data
{
    public class ListingService : ICrud<Listing>
    {
        DindingDB db;

        public ListingService()
        {
            if (db == null) db = new DindingDB();

        }

        public bool RemoveBookmark(long userid, long postid)
        {
            try
            {
                var removePost = db.ListingBookmarks.Where(x => x.UserId == userid && x.ListingId == postid).FirstOrDefault();
                if (removePost != null)
                {
                    db.ListingBookmarks.Remove(removePost);
                }

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }


        public bool AddBookmark(long userid, string username, long postid)
        {
            try
            {
                var newBookmark = new ListingBookmark() { CreatedDate = DateHelper.GetLocalTimeNow(), UserId = userid, ListingId = postid };
                db.ListingBookmarks.Add(newBookmark);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }
        public bool UnLikePost(long userid, long postid)
        {
            try
            {
                var removePost = db.ListingFavorites.Where(x => x.UserId == userid && x.ListingId == postid).FirstOrDefault();
                if (removePost != null)
                {
                    db.ListingFavorites.Remove(removePost);
                }

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }


        public bool LikePost(long userid, string username, long postid)
        {
            try
            {
                var newLike = new ListingFavorite() { CreatedDate = DateHelper.GetLocalTimeNow(), UserId = userid, ListingId = postid };
                db.ListingFavorites.Add(newLike);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }
        public bool DeleteData(object Id)
        {
            var selData = (db.Listings.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.Listings.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public long GetListingCount()
        {
           return db.Listings.Count();
        }
        public List<Listing> FindByKeyword(string Keyword)
        {
            var data = from x in db.Listings
                       where x.Title.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<Listing> GetAllData()
        {
            return db.Listings.OrderBy(x => x.Id).ToList();
        }
        public List<Listing> GetAllData(string Keyword,PeriodFilterCls period,int Limit =100)
        {
            return db.Listings.Include(c => c.Category).Include(c => c.SubCategory).Where(x => x.Title.Contains(Keyword) || x.Desc.Contains(Keyword) || string.IsNullOrEmpty(Keyword)).Where(x => x.CreatedDate.Month == period.Month && x.CreatedDate.Year == period.Year).OrderBy(x => x.Id).Take(Limit).ToList();
        }
        public MyStat GetMyStat(long UserId)
        {
            MyStat info = new();
            var datas = db.Listings.Include(c => c.ListingViews).Include(c => c.ListingFavorites).Include(c => c.ListingBookmarks).Include(c => c.ListingComments).Where(x => x.UserId == UserId).ToList();
            info.TotalPost = datas.Count;
            info.TotalComment = datas.Sum(x => x.ListingComments.Count);
            info.TotalBookmark = datas.Sum(x => x.ListingBookmarks.Count);
            info.TotalFavorite = datas.Sum(x => x.ListingFavorites.Count);
            info.TotalView = datas.Sum(x => x.ListingViews.Count);
            return info;
        }
        public List<MapItemInfo> GetLatestMap(int Limit =100)
        {
            List<MapItemInfo> infos = new();
            var rnd = new Random(Environment.TickCount);
            var datas = db.Listings.Include(c => c.Category).Include(c => c.SubCategory).Where(x=>!string.IsNullOrEmpty(x.Latitude) && !string.IsNullOrEmpty(x.Longitude)).OrderByDescending(x=>x.CreatedDate).Take(Limit).ToList();
            datas.ForEach(x=> infos.Add( new MapItemInfo() { content = $"{x.Desc}<br/>Harga: Rp.{x.Harga}<br/>Alamat: Rp.{x.Alamat}<br/>Kontak: {x.Email}/{x.Phone}", title = x.Title, imgurl = x.ImageUrls.Split(';')[0], lat = double.Parse( x.Latitude), lng = double.Parse( x.Longitude), zindex = rnd.Next(1, 7) }));
            return infos;
        } 
        
        public List<Listing> GetLatestListing(int Limit =10)
        {
            return db.Listings.Include(c => c.Category).Include(c => c.SubCategory).Include(c=>c.ListingViews).OrderByDescending(x => x.CreatedDate).Take(Limit).ToList();
        }  
        
        public List<Listing> GetMyListing(long userid, int Limit =100)
        {
            return db.Listings.Include(c=>c.ListingViews).Include(c=>c.ListingFavorites).Include(c => c.Category).Include(c=>c.User).Include(c => c.SubCategory).Where(x=>x.UserId == userid).OrderByDescending(x => x.CreatedDate).Take(Limit).ToList();
        } 
        public List<Listing> GetFeaturedListing(int Limit =10)
        {
            return db.Listings.Include(c => c.Category).Include(c => c.SubCategory).Include(c=>c.ListingViews).OrderByDescending(x => x.ListingViews.Count()).ThenByDescending(x=>x.Rating).Take(Limit).ToList();
        } 
        
        public List<Listing> GetAllData(string Keyword, long CategoryId, long SubCategoryId=-1, int Limit = 100)
        {
            if (SubCategoryId > 0)
            {
                return db.Listings.Include(c=>c.Category).Include(c=>c.SubCategory).Where(x=>x.Title.Contains(Keyword) || x.Desc.Contains(Keyword) || string.IsNullOrEmpty(Keyword)).Where(x=>x.SubCategoryId == SubCategoryId).OrderBy(x => x.Id).Take(Limit).ToList();
            }
            else if (CategoryId > 0)
            {
                return db.Listings.Include(c => c.Category).Include(c => c.SubCategory).Where(x => x.Title.Contains(Keyword) || x.Desc.Contains(Keyword) || string.IsNullOrEmpty(Keyword)).Where(x => x.CategoryId == CategoryId).OrderBy(x => x.Id).Take(Limit).ToList();
            }
            else
            {
                return db.Listings.Include(c => c.Category).Include(c => c.SubCategory).Where(x => x.Title.Contains(Keyword) || x.Desc.Contains(Keyword) || string.IsNullOrEmpty(Keyword)).OrderBy(x => x.Id).Take(Limit).ToList();
            }
            
        }
        public List<Listing> GetDataByTag(string Tag, int Limit = 100)
        {
           
                return db.Listings.Include(c => c.Category).Include(c => c.SubCategory).Where(x => x.Tags.Contains(Tag) || string.IsNullOrEmpty(Tag)).OrderByDescending(x => x.Id).Take(Limit).ToList();
            

        } 
        public List<Listing> GetDataByLocation(string Location, int Limit = 100)
        {
           
                return db.Listings.Include(c => c.Category).Include(c => c.SubCategory).Where(x => x.Kota.Contains(Location) || string.IsNullOrEmpty(Location)).OrderByDescending(x => x.Id).Take(Limit).ToList();
            

        }
        public List<Listing> GetAllData(string Keyword, string Category, int Limit = 100)
        {
           if (!string.IsNullOrEmpty(Category) && Category!="all")
            {
                return db.Listings.Include(c => c.Category).Include(c => c.SubCategory).Where(x => x.Title.Contains(Keyword) || x.Desc.Contains(Keyword) || string.IsNullOrEmpty(Keyword)).Where(x => x.Category.Name == Category).OrderBy(x => x.Id).Take(Limit).ToList();
            }
            else
            {
                return db.Listings.Include(c => c.Category).Include(c => c.SubCategory).Where(x => x.Title.Contains(Keyword) || x.Desc.Contains(Keyword) || string.IsNullOrEmpty(Keyword)).OrderBy(x => x.Id).Take(Limit).ToList();
            }
            
        }
        
        public long GetCountSubCategory(long SubCategoryId)
        {
            return db.Listings.Where(x=>x.SubCategoryId == SubCategoryId).Count();
        }

        public Listing GetDataById(object Id)
        {
            return db.Listings.Include(c=>c.ListingBookmarks).Include(c => c.ListingRatings).Include(c => c.ListingViews).Include(c => c.ListingFavorites).Include(c => c.Category).Include(c => c.User).Include(c => c.SubCategory).Where(x => x.Id == (long)Id).FirstOrDefault();
        }

        public List<LocationItemCls> GetLocations()
        {
            /*
             from r in info
         orderby r.metric    
         group r by r.metric into grp
         select new { key = grp.Key, cnt = grp.Count()};
             */
            var locations = db.Listings.OrderByDescending(x => x.Kota).GroupBy(x => x.Kota).Select(x=> new LocationItemCls() { Location = x.Key, Count = x.Count() });
            return locations.ToList();
        } 
        
        public List<PeriodFilterCls> GetPeriods()
        {
            //var periods = db.Listings.OrderByDescending(x=>x.Id).Take(1000).Select(x => new PeriodFilterCls() { Month = x.CreatedDate.Month, Year = x.CreatedDate.Year, MonthYear = x.CreatedDate.ToString("MMMM yyyy") }).Distinct().ToList();
            var periods = db.Listings.OrderByDescending(x => x.Id).GroupBy(x => new { x.CreatedDate.Month, x.CreatedDate.Year }).Select(x => new PeriodFilterCls() { MonthYear = DateTime.Parse($"{x.Key.Month}/01/{x.Key.Year}").ToString("MMMM yyyy"), Month = x.Key.Month, Year = x.Key.Year, Count = x.Count() }).ToList();
           
            return periods;
        }

        public bool InsertData(Listing data)
        {
            try
            {
                db.Listings.Add(data);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;

        }



        public bool UpdateData(Listing data)
        {
            try
            {
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();

                /*
                if (sel != null)
                {
                    sel.Nama = data.Nama;
                    sel.Keterangan = data.Keterangan;
                    sel.Tanggal = data.Tanggal;
                    sel.DocumentUrl = data.DocumentUrl;
                    sel.StreamUrl = data.StreamUrl;
                    return true;

                }*/
                return true;
            }
            catch
            {

            }
            return false;
        }

        public long GetLastId()
        {
            return db.Listings.Max(x => x.Id);
        }
    }

}
/*











 */
