using LactafarmaAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LactafarmaAPI.Data
{
    public class LactafarmaContext : DbContext
    {
        #region Public Properties

        public DbSet<Alert> Alerts { get; set; }
        public DbSet<AlertMultilingual> AlertsMultilingual { get; set; }
        public DbSet<Alias> Aliases { get; set; }
        public DbSet<AliasMultilingual> AliasMultilingual { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<BrandMultilingual> BrandsMultilingual { get; set; }
        public DbSet<DrugAlternative> DrugAlternatives { get; set; }
        public DbSet<DrugBrand> DrugBrands { get; set; }
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<DrugMultilingual> DrugsMultilingual { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMultilingual> GroupsMultilingual { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<User> Users { get; set; }

        #endregion

        #region Constructors

        public LactafarmaContext(DbContextOptions<LactafarmaContext> options) : base(options)
        {
        }

        #endregion

        #region Overridden Members

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            LactafarmaDbMapping.Configure(modelBuilder);
        }

        #endregion
    }
}