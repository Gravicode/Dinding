using Microsoft.EntityFrameworkCore;
using Dinding.Data;
using Dinding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dinding.Data
{
    public class CategoryService : ICrud<Category>
    {
        DindingDB db;

        public CategoryService()
        {
            if (db == null) db = new DindingDB();

        }
        public long GetCategoryCount()
        {
            return db.Categorys.Count();
        }
        public bool DeleteData(object Id)
        {
            var selData = (db.Categorys.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.Categorys.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<Category> FindByKeyword(string Keyword)
        {
            var data = from x in db.Categorys
                       where x.Name.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<Category> GetAllData()
        {
            return db.Categorys.OrderBy(x => x.Name).ToList();
        } 
        
        public IEnumerable<CategoryCountCls> GetCategoriesWithCount()
        {
            var categories =  db.Categorys.OrderBy(x => x.Name).ToList();
            var data = from x in categories
                       select new CategoryCountCls(x.Id, x.Name, db.Listings.Where(x => x.CategoryId == x.Id).Count());
                       
            return data;
        }

        public Category GetDataById(object Id)
        {
            return db.Categorys.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(Category data)
        {
            try
            {
                db.Categorys.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(Category data)
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
            return db.Categorys.Max(x => x.Id);
        }
    }

}
/*











 */
