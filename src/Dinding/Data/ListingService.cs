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
            return db.Listings.Include(c=>c.ListingBookmarks).Include(c => c.ListingViews).Include(c => c.ListingFavorites).Include(c => c.Category).Include(c => c.User).Include(c => c.SubCategory).Where(x => x.Id == (long)Id).FirstOrDefault();
        }

        public List<string> GetLocations()
        {
            var locations = db.Listings.OrderByDescending(x=>x.Id).Take(1000).Select(x => x.Kota).Distinct().ToList();
            return locations;
        } 
        
        public List<PeriodFilterCls> GetPeriods()
        {
            var periods = db.Listings.OrderByDescending(x=>x.Id).Take(1000).Select(x => new PeriodFilterCls() { Month = x.CreatedDate.Month, Year = x.CreatedDate.Year, MonthYear = x.CreatedDate.ToString("MMMM yyyy") }).Distinct().ToList();
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
