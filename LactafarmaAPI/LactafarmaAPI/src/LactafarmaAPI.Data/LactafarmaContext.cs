using LactafarmaAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace LactafarmaAPI.Data
{
    public class LactafarmaContext: DbContext
    {
        public LactafarmaContext(DbContextOptions<LactafarmaContext> options) : base(options)
        {
        }

        public DbSet<Alert> Alerts { get; set; }
        public DbSet<Alias> Aliases { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<DrugBrand> DrugBrands { get; set; }
        public DbSet<DrugAlternative> DrugAlternatives { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            LactafarmaDbMapping.Configure(modelBuilder);
        }
    }

    

}
