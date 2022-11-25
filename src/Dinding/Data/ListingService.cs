using Microsoft.EntityFrameworkCore;
using Dinding.Data;
using Dinding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dinding.Data
{
    public class ListingService : ICrud<Listing>
    {
        DindingDB db;

        public ListingService()
        {
            if (db == null) db = new DindingDB();

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
        
        public long GetCountSubCategory(long SubCategoryId)
        {
            return db.Listings.Where(x=>x.SubCategoryId == SubCategoryId).Count();
        }

        public Listing GetDataById(object Id)
        {
            return db.Listings.Where(x => x.Id == (long)Id).FirstOrDefault();
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
