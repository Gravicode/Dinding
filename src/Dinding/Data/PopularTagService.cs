using Dinding.Models;
using Microsoft.EntityFrameworkCore;

namespace Dinding.Data
{
    public class PopularTagService : ICrud<PopularTag>
    {
        DindingDB db;

        public PopularTagService()
        {
            if (db == null) db = new DindingDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.PopularTags.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.PopularTags.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<PopularTag> FindByKeyword(string Keyword)
        {
            var data = from x in db.PopularTags
                       where x.Tag.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<PopularTag> GetAllData()
        {
            return db.PopularTags.OrderBy(x => x.Id).ToList();
        } 
        
        public List<PopularTag> GetPopularTags(int TakeCount = 10)
        {
            return db.PopularTags.OrderByDescending(x => x.Count).Take(TakeCount).ToList();
        }

        public PopularTag GetDataById(object Id)
        {
            return db.PopularTags.Where(x => x.Id == (long)Id).FirstOrDefault();
        }

        public bool InsertFromListing(UserProfile user, Listing data)
        {
            try
            {
                if (!string.IsNullOrEmpty(data.Tags))
                {
                    foreach (var hashtag in data.Tags.Split(';'))
                    {
                        var selItem = db.PopularTags.Where(x => x.Tag == hashtag).FirstOrDefault();
                        if (selItem == null)
                        {
                            selItem = new PopularTag() { LastUpdate = data.CreatedDate, Tag = hashtag, Count=1 };
                            db.PopularTags.Add(selItem);
                        }
                        else
                        {
                            selItem.Count = selItem.Count + 1;
                            db.Entry(selItem).State = EntityState.Modified;
                        }
                       
                    }
                }

                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }


        public bool InsertData(PopularTag data)
        {
            try
            {
                db.PopularTags.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(PopularTag data)
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
            return db.PopularTags.Max(x => x.Id);
        }
    }

}
/*











 */
