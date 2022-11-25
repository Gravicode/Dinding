using Microsoft.EntityFrameworkCore;
using Dinding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace Dinding.Data
{
    public class DindingDB : DbContext
    {

        public DindingDB()
        {
        }

        public DindingDB(DbContextOptions<DindingDB> options)
            : base(options)
        {
        }

        public DbSet<PopularTag> PopularTags { get; set; }     
        public DbSet<UserProfile> UserProfiles { get; set; }     
       
        public DbSet<Notification> Notifications { get; set; }      
      
        public DbSet<Contact> Contacts { get; set; }      
        public DbSet<Log> Logs { get; set; }      
        public DbSet<Listing> Listings { get; set; }      
        public DbSet<ListingBookmark> ListingBookmarks { get; set; }      
        public DbSet<ListingFavorite> ListingFavorites { get; set; }      
        public DbSet<ListingRating> ListingRatings { get; set; }      
        public DbSet<ListingView> ListingViews { get; set; }      
        public DbSet<Category> Categorys { get; set; }      
        public DbSet<SubCategory> SubCategorys { get; set; }      
      
       
       
       
      

        protected override void OnModelCreating(ModelBuilder builder)
        {
            /*
            builder.Entity<DataEventRecord>().HasKey(m => m.DataEventRecordId);
            builder.Entity<SourceInfo>().HasKey(m => m.SourceInfoId);

            // shadow properties
            builder.Entity<DataEventRecord>().Property<DateTime>("UpdatedTimestamp");
            builder.Entity<SourceInfo>().Property<DateTime>("UpdatedTimestamp");
            */
          
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            /*
            ChangeTracker.DetectChanges();

            updateUpdatedProperty<SourceInfo>();
            updateUpdatedProperty<DataEventRecord>();
            */
            return base.SaveChanges();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(AppConstants.SQLConn,ServerVersion.AutoDetect(AppConstants.SQLConn));
            }
        }
        private void updateUpdatedProperty<T>() where T : class
        {
            /*
            var modifiedSourceInfo =
                ChangeTracker.Entries<T>()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in modifiedSourceInfo)
            {
                entry.Property("UpdatedTimestamp").CurrentValue = DateTime.UtcNow;
            }
            */
        }

    }
}
