using System.Data.Entity;
using BebemundiWebAPI.Entities;

namespace BebemundiWebAPI.EntityFramework
{
    public class BebemundiWebAPIContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<BebemundiWebAPI.Models.BebemundiWebAPIContext>());

        public BebemundiWebAPIContext()
            : base("StringConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }


        static BebemundiWebAPIContext()
        {
            Database.SetInitializer<BebemundiWebAPIContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            BebemundiWebAPIMapping.Configure(modelBuilder);
        }

        public DbSet<ApiUser> ApiUsers { get; set; }
        public DbSet<AuthToken> AuthTokens { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Alias> Aliases { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Trademark> Trademarks { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<ProductTrademark> ProductTrademarks { get; set; }
        public DbSet<ProductAlternative> ProductAlternatives { get; set; }
        public DbSet<SearchItem> SearchItems { get; set; }
    }
}
