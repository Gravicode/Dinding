using Microsoft.EntityFrameworkCore;
using Dinding.Data;
using Dinding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dinding.Data
{
    public class ListingBookmarkService : ICrud<ListingBookmark>
    {
        DindingDB db;

        public ListingBookmarkService()
        {
            if (db == null) db = new DindingDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.ListingBookmarks.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.ListingBookmarks.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<ListingBookmark> FindByKeyword(string Keyword)
        {
            var data = from x in db.ListingBookmarks.Include(c => c.User)
                       where x.User.FullName.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<ListingBookmark> GetAllData()
        {
            return db.ListingBookmarks.OrderBy(x => x.Id).ToList();
        }

        public ListingBookmark GetDataById(object Id)
        {
            return db.ListingBookmarks.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(ListingBookmark data)
        {
            try
            {
                db.ListingBookmarks.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(ListingBookmark data)
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
            return db.ListingBookmarks.Max(x => x.Id);
        }
    }

}
/*











 */
