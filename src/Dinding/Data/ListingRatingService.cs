using Microsoft.EntityFrameworkCore;
using Dinding.Data;
using Dinding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dinding.Data
{
    public class ListingRatingService : ICrud<ListingRating>
    {
        DindingDB db;

        public ListingRatingService()
        {
            if (db == null) db = new DindingDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.ListingRatings.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.ListingRatings.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<ListingRating> FindByKeyword(string Keyword)
        {
            var data = from x in db.ListingRatings.Include(c => c.Listing)
                       where x.Listing.Title.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<ListingRating> GetAllData()
        {
            return db.ListingRatings.OrderBy(x => x.Id).ToList();
        }

        public ListingRating GetDataById(object Id)
        {
            return db.ListingRatings.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(ListingRating data)
        {
            try
            {
                db.ListingRatings.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(ListingRating data)
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
            return db.ListingRatings.Max(x => x.Id);
        }
    }

}
/*











 */
