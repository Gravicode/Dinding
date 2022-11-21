using Microsoft.EntityFrameworkCore;
using Dinding.Data;
using Dinding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dinding.Data
{
    public class ListingViewService : ICrud<ListingView>
    {
        DindingDB db;

        public ListingViewService()
        {
            if (db == null) db = new DindingDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.ListingViews.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.ListingViews.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<ListingView> FindByKeyword(string Keyword)
        {
            var data = from x in db.ListingViews.Include(c => c.User)
                       where x.User.FullName.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<ListingView> GetAllData()
        {
            return db.ListingViews.OrderBy(x => x.Id).ToList();
        }

        public ListingView GetDataById(object Id)
        {
            return db.ListingViews.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(ListingView data)
        {
            try
            {
                db.ListingViews.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(ListingView data)
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
            return db.ListingViews.Max(x => x.Id);
        }
    }

}
/*











 */
