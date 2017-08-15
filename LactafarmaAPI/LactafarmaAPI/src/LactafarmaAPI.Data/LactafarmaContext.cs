using LactafarmaAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LactafarmaAPI.Data
{
    public class LactafarmaContext : DbContext
    {
        private readonly ILogger<LactafarmaContext> _logger;
        #region Public Properties

        public DbSet<Log> Logs { get; set; }
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

        public LactafarmaContext(DbContextOptions<LactafarmaContext> options, ILogger<LactafarmaContext> logger) : base(options)
        {
            _logger = logger;
        }

        #endregion

        #region Overridden Members

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            LactafarmaDbMapping.Configure(modelBuilder, _logger);
        }

        #endregion
    }
}