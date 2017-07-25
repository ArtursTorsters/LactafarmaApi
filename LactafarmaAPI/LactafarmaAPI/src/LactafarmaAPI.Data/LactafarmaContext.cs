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
        private IConfigurationRoot _config;

        public LactafarmaContext(IConfigurationRoot config, DbContextOptions options): base(options)
        {
            _config = config;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config["ConnectionStrings:LactafarmaContextConnection"]);
        }
    }

    

}
