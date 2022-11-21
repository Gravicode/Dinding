using Microsoft.EntityFrameworkCore;
using Dinding.Data;
using Dinding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dinding.Data
{
    public class ListingFavoriteService : ICrud<ListingFavorite>
    {
        DindingDB db;

        public ListingFavoriteService()
        {
            if (db == null) db = new DindingDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.ListingFavorites.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.ListingFavorites.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<ListingFavorite> FindByKeyword(string Keyword)
        {
            var data = from x in db.ListingFavorites.Include(c=>c.User)
                       where x.User.FullName.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<ListingFavorite> GetAllData()
        {
            return db.ListingFavorites.OrderBy(x => x.Id).ToList();
        }

        public ListingFavorite GetDataById(object Id)
        {
            return db.ListingFavorites.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(ListingFavorite data)
        {
            try
            {
                db.ListingFavorites.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(ListingFavorite data)
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
            return db.ListingFavorites.Max(x => x.Id);
        }
    }

}
/*











 */
