using GemBox.Document;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Reflection;

namespace Dinding.Models
{
    #region helpers model
    public class StorageObject
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public string FileUrl { get; set; }
        public string ContentType { get; set; }
        public DateTime? LastUpdate { get; set; }
        public DateTime? LastAccess { get; set; }
    }
    public class StorageSetting
    {
        public string EndpointUrl { get; set; } = "https://is3.cloudhost.id";
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string Region { get; set; } = "USWest1";
        public string Bucket { get; set; }
        public string BaseUrl { get; set; }
        public bool Ssl { get; set; } = true;
        public StorageSetting()
        {

        }
        public StorageSetting(string Endpoint, string Accesskey, string Secretkey, string Region, string Bucket)
        {
            this.EndpointUrl = Endpoint;
            this.AccessKey = Accesskey;
            this.SecretKey = Secretkey;
            this.Region = Region;
            this.Bucket = Bucket;
            GenerateBaseUrl();
        }
        public void GenerateBaseUrl()
        {
            this.BaseUrl = EndpointUrl + "/{bucket}/{key}";
        }
    }
    public class PeriodFilterCls
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public string MonthYear { get; set; }
    }
    public class CategoryCountCls//(long Id, string Name, long Total);
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public long Total { set; get; }
        public CategoryCountCls(long Id, string Name, long Total)
        {
            this.Id = Id;
            this.Name = Name;
            this.Total = Total;
        }
    }
    public class OutputCls
    {
        public OutputCls()
        {
            this.IsSucceed = false;
            this.Message = "";
        }
        public object Data { get; set; }
        public string Message { get; set; }
        public bool IsSucceed { get; set; }
    }
    #endregion

    [Table("popular_tag")]
    public class PopularTag
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public string Tag { get; set; }
        public long Count { get; set; }
        public DateTime LastUpdate { get; set; }
    }

        [Table("notification")]
    public class Notification
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }

        public DateTime CreatedDate { set; get; }
        [ForeignKey(nameof(User)), Column(Order = 0)]
        public long UserId { set; get; }
        public UserProfile User { set; get; }
        public string Action { set; get; }
        public string LinkUrl { set; get; }
        public string LinkDesc { set; get; }
        public string Message { set; get; }
        public bool IsRead { set; get; } = false;
    }
    public enum LogCategory
    {
        Info, Error, Warning
    }
    [Table("logs")]
    public class Log
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LogDate { get; set; }
        public string Message { get; set; }
        public LogCategory Category { get; set; }
    }

    [Table("userprofile")]
    public class UserProfile
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Alamat { get; set; }
        public string? KodePos { get; set; }
        public string? Negara { get; set; }
        public string? Kota { get; set; }
        public AddressTypes TipeAlamat { get; set; } = AddressTypes.Rumah;
        public string? KTP { get; set; }
        public string? PicUrl { get; set; }
        public bool Aktif { get; set; } = true;
        public string? Daerah { get; set; }
        public string? Desa { get; set; }
        public string? Kelompok { get; set; }
        public Char Gender { get; set; } = 'N';
        public Roles Role { set; get; } = Roles.User;
        public DateTime CreatedDate { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }

        public string? FirstName { set; get; }
        public string? LastName { set; get; }

        public string? AboutMe { set; get; }

        public string? Website { set; get; }
        public string? FBUrl { set; get; }
        public string? TwitterUrl { set; get; }
        public string? GithubUrl { set; get; }
        public string? InstagramUrl { set; get; }
        public string? LinkedIdUrl { set; get; }
        public string? Pekerjaan { set; get; }

        [InverseProperty(nameof(ListingFavorite.User))]
        public ICollection<ListingFavorite> ListingFavorites { get; set; }

        [InverseProperty(nameof(ListingBookmark.User))]
        public ICollection<ListingBookmark> ListingBookmarks { get; set; }

        [InverseProperty(nameof(ListingRating.User))]
        public ICollection<ListingRating> ListingRatings { get; set; }

        [InverseProperty(nameof(ListingView.User))]
        public ICollection<ListingView> ListingViews { get; set; }
        
        [InverseProperty(nameof(Listing.User))]
        public ICollection<Listing> Listings { get; set; }

    }

    public enum AddressTypes { Rumah, Kantor, Toko, Lainnya }

    [Table("contact")]
    public class Contact
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }

        public string Firstname { set; get; }
        public string Lastname { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Message { set; get; }
        public DateTime CreatedDate { set; get; } = DateTime.Now;
        public string? ReplyMessage { set; get; }
        public string? ReplyBy { set; get; }
        public DateTime? ReplyDate { set; get; }
    }


    public enum Roles { Admin, User, Pengurus }

    [Table("category")]
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public string Name { set; get; }
        public string? Desc { set; get; }

        [InverseProperty(nameof(SubCategory.Category))]
        public ICollection<SubCategory> SubCategories { get; set; }
    }
    [Table("subcategory")]
    public class SubCategory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public string Name { set; get; }
        public string Desc { set; get; }

        [ForeignKey(nameof(Category)), Column(Order = 1)]
        public long CategoryId { set; get; }
        public Category Category { set; get; }
    }

    [Table("listing")]
    public class Listing
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public string Title { set; get; }
        public string Desc { set; get; }
        [ForeignKey(nameof(Category)), Column(Order = 0)]
        public long CategoryId { set; get; }
        public Category Category { set; get; }
        [ForeignKey(nameof(SubCategory)), Column(Order = 1)]
        public long SubCategoryId { set; get; }
        public SubCategory SubCategory { set; get; }
        //public string Location { set; get; }
        public string Alamat { set; get; }
        public string Phone { set; get; }
        public ListingTypes ListingType { set; get; }
        public double Harga { set; get; }
        public string? Kota { set; get; }
        public string Email { set; get; }
        public string? Website { set; get; }
        public string? WorkHourSenin { set; get; }
        public string? WorkHourSelasa { set; get; }
        public string? WorkHourRabu { set; get; }
        public string? WorkHourKamis { set; get; }
        public string? WorkHourJumat { set; get; }
        public string? WorkHourSabtu { set; get; }
        public string? WorkHourMinggu { set; get; }
        public string? Latitude { set; get; }
        public string? Longitude { set; get; }
        public string ImageUrls { set; get; }
        public string? Facilities { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        [ForeignKey(nameof(User)), Column(Order = 2)]
        public long UserId { set; get; }
        public UserProfile User { set; get; }
        public int? Rating { set; get; }
        public string? Tags { set; get; }

        [InverseProperty(nameof(ListingFavorite.Listing))]
        public ICollection<ListingFavorite> ListingFavorites { get; set; }

        [InverseProperty(nameof(ListingBookmark.Listing))]
        public ICollection<ListingBookmark> ListingBookmarks { get; set; }

        [InverseProperty(nameof(ListingRating.Listing))]
        public ICollection<ListingRating> ListingRatings { get; set; }

        [InverseProperty(nameof(ListingView.Listing))]
        public ICollection<ListingView> ListingViews { get; set; }
    }

    public enum ListingTypes { Jual, Sewa, Subscription, Kontrak, Service }

    [Table("listing_rating")]
    public class ListingRating
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }

        public int Rating { get; set; }

        [ForeignKey(nameof(User)), Column(Order = 0)]
        public long UserId { set; get; }
        public UserProfile User { set; get; }
        public DateTime CreatedDate { set; get; }
        

        [ForeignKey(nameof(Listing)), Column(Order = 1)]
        public long ListingId { set; get; }
        public Listing Listing { set; get; }
    }

    [Table("listing_favorite")]
    public class ListingFavorite
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }

        [ForeignKey(nameof(User)), Column(Order = 0)]
        public long UserId { set; get; }
        public UserProfile User { set; get; }
        public DateTime CreatedDate { set; get; }


        [ForeignKey(nameof(Listing)), Column(Order = 1)]
        public long ListingId { set; get; }
        public Listing Listing { set; get; }
    } 
    
    [Table("listing_bookmark")]
    public class ListingBookmark
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }

        [ForeignKey(nameof(User)), Column(Order = 0)]
        public long UserId { set; get; }
        public UserProfile User { set; get; }
        public DateTime CreatedDate { set; get; }


        [ForeignKey(nameof(Listing)), Column(Order = 1)]
        public long ListingId { set; get; }
        public Listing Listing { set; get; }
    }

    [Table("listing_view")]
    public class ListingView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }

        [ForeignKey(nameof(User)), Column(Order = 0)]
        public long UserId { set; get; }
        public UserProfile User { set; get; }
        public DateTime CreatedDate { set; get; }


        [ForeignKey(nameof(Listing)), Column(Order = 1)]
        public long ListingId { set; get; }
        public Listing Listing { set; get; }
    }
}
