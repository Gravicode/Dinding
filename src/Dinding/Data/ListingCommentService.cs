using Microsoft.EntityFrameworkCore;
using Dinding.Data;
using Dinding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dinding.Data
{
    public class ListingCommentService : ICrud<ListingComment>
    {
        DindingDB db;

        public ListingCommentService()
        {
            if (db == null) db = new DindingDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.ListingComments.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.ListingComments.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<ListingComment> FindByKeyword(string Keyword)
        {
            var data = from x in db.ListingComments
                       where x.Comment.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<ListingComment> GetAllData()
        {
            return db.ListingComments.OrderBy(x => x.Id).ToList();
        }
        public List<ListingComment> GetAllData(long ListingId)
        {
            return db.ListingComments.Include(c=>c.User).Include(c=>c.Listing).Where(x=>x.ListingId == ListingId).OrderBy(x => x.Id).ToList();
        }

        public ListingComment GetDataById(object Id)
        {
            return db.ListingComments.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(ListingComment data)
        {
            try
            {
                db.ListingComments.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(ListingComment data)
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
            return db.ListingComments.Max(x => x.Id);
        }
    }

}
/*











 */
