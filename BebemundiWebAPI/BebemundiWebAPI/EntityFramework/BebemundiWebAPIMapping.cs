using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BebemundiWebAPI.Entities;

namespace BebemundiWebAPI.EntityFramework
{
    public class BebemundiWebAPIMapping
    {
        public static void Configure(DbModelBuilder modelBuilder)
        {
            MapAlias(modelBuilder);
            MapProduct(modelBuilder);
            MapGroup(modelBuilder);
            MapTrademark(modelBuilder);
            MapApiUser(modelBuilder);
            MapApiToken(modelBuilder);
            MapBookmark(modelBuilder);
            MapProductTrademark(modelBuilder);
            MapProductAlternative(modelBuilder);
            MapSearch(modelBuilder);
        }

        static void MapApiToken(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthToken>().ToTable("Tokens");
        }

        static void MapApiUser(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApiUser>().ToTable("Usuarios");
        }

        static void MapAlias(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alias>().ToTable("Alias");
        }

        static void MapProduct(DbModelBuilder modelBuilder)
        {            
            modelBuilder.Entity<Product>().ToTable("Medicamentos");
        }

        static void MapGroup(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>().ToTable("Grupos");
        }

        static void MapTrademark(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trademark>().ToTable("Marcas");
        }

        static void MapBookmark(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bookmark>().ToTable("Favoritos");
        }

        static void MapProductTrademark(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductTrademark>().ToTable("MarcasMedicamentos");
        }

        static void MapProductAlternative(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductAlternative>().ToTable("AlternativasMedicamentos");
        }

        private static void MapSearch(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SearchItem>().ToTable("SearchIndex");
        }
    }
}