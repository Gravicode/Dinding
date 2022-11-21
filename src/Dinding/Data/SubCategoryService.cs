using Microsoft.EntityFrameworkCore;
using Dinding.Data;
using Dinding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dinding.Data
{
    public class SubCategoryService : ICrud<SubCategory>
    {
        DindingDB db;

        public SubCategoryService()
        {
            if (db == null) db = new DindingDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.SubCategorys.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.SubCategorys.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<SubCategory> FindByKeyword(string Keyword)
        {
            var data = from x in db.SubCategorys
                       where x.Name.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<SubCategory> GetAllData()
        {
            return db.SubCategorys.OrderBy(x => x.Id).ToList();
        }

        public SubCategory GetDataById(object Id)
        {
            return db.SubCategorys.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(SubCategory data)
        {
            try
            {
                db.SubCategorys.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(SubCategory data)
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
            return db.SubCategorys.Max(x => x.Id);
        }
    }

}
/*











 */
