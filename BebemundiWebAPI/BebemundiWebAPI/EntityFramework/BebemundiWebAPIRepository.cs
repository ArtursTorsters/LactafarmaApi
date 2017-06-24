using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BebemundiWebAPI.Entities;
using System.Web.Http.Routing;
using System.Net.Http;
using System.Data;

namespace BebemundiWebAPI.EntityFramework
{
    public class BebemundiWebAPIRepository: IBebemundiWebAPIRepository
    {        
        private BebemundiWebAPIContext _ctx;
        public BebemundiWebAPIRepository(BebemundiWebAPIContext ctx)
        {            
            _ctx = ctx;
        }

        public bool SaveAll()
        {            
            return _ctx.SaveChanges() > 0;
        }

        #region PRODUCTS

        public IQueryable<Product> GetAllProducts()
        {
            return _ctx.Products.Where(p => !String.IsNullOrEmpty(p.Nombre));
        }

        public IQueryable<Product> FindProductsByNameStartsWith(string query)
        {
            return _ctx.Products.Where(p => p.Nombre.StartsWith(query)).Where(p => !String.IsNullOrEmpty(p.Nombre)); ;
        }

        public IQueryable<Product> GetProductsByGroup(string group)
        {
            return _ctx.Products.Where(p => p.IdGrupo.Equals(group)).Where(p => !String.IsNullOrEmpty(p.Nombre)); ;
        }

        public IEnumerable<Product> GetProductsByProduct(string product)
        {
            var alternatives = (_ctx.ProductAlternatives.Where(n => n.IdMedicamento.Equals(product)).Select(n => n.IdAlternativaMedicamento));
            return _ctx.Products.Where(p => alternatives.Contains(p.Id)).Where(p => !String.IsNullOrEmpty(p.Nombre)); ;
        }

        public Product GetProduct(string id)
        {
            return _ctx.Products.FirstOrDefault(p => p.Id.Equals(id));
        }

        public string GetLastIdProduct()
        {
            var results = _ctx.Products.Select(a => a.Id);

            return results.Select(int.Parse).OrderBy(i => i).Last().ToString();            
        }

        public bool InsertProduct(Product entry)
        {
            try
            {
                _ctx.Products.Add(entry);
                return true;
            }
            catch (Exception)
            {
                //TODO Logging
            }

            return false;
        }

        public bool UpdateProduct(Product entry)
        {
            return UpdateEntity(_ctx.Products, entry);
        }

        public bool DeleteProduct(string id)
        {
            try
            {
                var entity = _ctx.Products.Where(p => p.Id.Equals(id)).FirstOrDefault();
                if (entity != null)
                {
                    _ctx.Products.Remove(entity);
                    return true;
                }
            }
            catch (Exception)
            {
                //TODO Logging
            }

            return false;
        }

        #endregion

        #region GROUPS

        public IQueryable<Group> GetAllGroups()
        {
            return _ctx.Groups.Where(g => !String.IsNullOrEmpty(g.Nombre));
        }

        public IQueryable<Group> FindGroupsByNameStartsWith(string query)
        {
            return _ctx.Groups.Where(p => p.Nombre.StartsWith(query)).Where(g => !String.IsNullOrEmpty(g.Nombre));
        }

        public Group GetGroup(string id)
        {
            return _ctx.Groups.FirstOrDefault(p => p.Id.Equals(id));
        }

        public string GetLastIdGroup()
        {
            var results = _ctx.Groups.Select(a => a.Id);

            return results.Select(int.Parse).OrderBy(i => i).Last().ToString();            
        }

        public bool InsertGroup(Group entry)
        {
            try
            {
                _ctx.Groups.Add(entry);
                return true;
            }
            catch (Exception)
            {
                //TODO Logging
            }

            return false;
        }

        public bool UpdateGroup(Group entry)
        {
            return UpdateEntity(_ctx.Groups, entry);
        }

        public bool DeleteGroup(string id)
        {
            try
            {
                var entity = _ctx.Groups.Where(p => p.Id.Equals(id)).FirstOrDefault();
                if (entity != null)
                {
                    _ctx.Groups.Remove(entity);
                    return true;
                }
            }
            catch (Exception)
            {
                //TODO Logging
            }

            return false;
        }

        #endregion

        #region ALIAS
        public IQueryable<Alias> GetAllAliases()
        {
            return _ctx.Aliases.Where(a => !String.IsNullOrEmpty(a.Nombre)); 
        }

        public IQueryable<Alias> FindAliasesByNameStartsWith(string query)
        {
            return _ctx.Aliases.Where(p => p.Nombre.StartsWith(query)).Where(a => !String.IsNullOrEmpty(a.Nombre)); 
        }

        public IQueryable<Alias> GetAliasesByProduct(string product)
        {
            return _ctx.Aliases.Where(a => a.IdMedicamento.Equals(product)).Where(a => !String.IsNullOrEmpty(a.Nombre)); 
        }

        public Alias GetAlias(string id)
        {
            return _ctx.Aliases.FirstOrDefault(p => p.Id.Equals(id));
        }

        public string GetLastIdAlias()
        {
            var results = _ctx.Aliases.Select(a => a.Id);

            return results.Select(int.Parse).OrderBy(i => i).Last().ToString();            
        }

        public bool InsertAlias(Alias entry)
        {
            try
            {
                _ctx.Aliases.Add(entry);
                return true;
            }
            catch (Exception)
            {
                //TODO Logging
            }

            return false;
        }

        public bool UpdateAlias(Alias entry)
        {
            return UpdateEntity(_ctx.Aliases, entry);
        }

        public bool DeleteAlias(string id)
        {
            try
            {
                var entity = _ctx.Aliases.Where(p => p.Id.Equals(id)).FirstOrDefault();
                if (entity != null)
                {
                    _ctx.Aliases.Remove(entity);
                    return true;
                }
            }
            catch (Exception)
            {
                //TODO Logging
            }

            return false;
        }
        #endregion

        #region TRADEMARKS

        public IQueryable<Trademark> GetAllTrademarks()
        {
            return _ctx.Trademarks.Where(t => !String.IsNullOrEmpty(t.Nombre)); 
        }

        public IQueryable<Trademark> FindTrademarksByNameStartsWith(string query)
        {
            return _ctx.Trademarks.Where(p => p.Nombre.StartsWith(query)).Where(t => !String.IsNullOrEmpty(t.Nombre)); 
        }

        public IQueryable<Trademark> GetTrademarksByProduct(string product)
        {
            var trademarks = (_ctx.ProductTrademarks.Where(n => n.IdMedicamento.Equals(product)).Select(n => n.IdMarca));
            return _ctx.Trademarks.Where(p => trademarks.Contains(p.Id)).Where(t => !String.IsNullOrEmpty(t.Nombre));             
        }

        public Trademark GetTrademark(string id)
        {
            return _ctx.Trademarks.FirstOrDefault(p => p.Id.Equals(id));
        }

        public string GetLastIdTrademark()
        {
            var results = _ctx.Trademarks.Select(a => a.Id);

            return results.Select(int.Parse).OrderBy(i => i).Last().ToString();            
        }

        public bool InsertTrademark(Trademark entry)
        {
            try
            {
                _ctx.Trademarks.Add(entry);
                return true;
            }
            catch (Exception)
            {
                //TODO Logging
            }

            return false;
        }

        public bool UpdateTrademark(Trademark entry)
        {
            return UpdateEntity(_ctx.Trademarks, entry);
        }

        public bool DeleteTrademark(string id)
        {
            try
            {
                var entity = _ctx.Trademarks.Where(p => p.Id.Equals(id)).FirstOrDefault();
                if (entity != null)
                {
                    _ctx.Trademarks.Remove(entity);
                    return true;
                }
            }
            catch (Exception)
            {
                //TODO Logging
            }

            return false;
        }
        #endregion

        #region BOOKMARKS
        public IQueryable<Bookmark> GetBookmarksByUser(string user)
        {
            return _ctx.Bookmarks.Where(b => b.IdUsuario.Equals(user));
        }

        public Bookmark GetBookmark(string id)
        {
            return _ctx.Bookmarks.FirstOrDefault(p => p.Id.Equals(id));
        }

        public string GetLastIdBookmark()
        {
            var results = _ctx.Bookmarks.Select(a => a.Id);

            return results.Select(int.Parse).OrderBy(i => i).Last().ToString();            
        }

        public bool InsertBookmark(Bookmark entry)
        {
            try
            {
                _ctx.Bookmarks.Add(entry);
                return true;
            }
            catch (Exception)
            {
                //TODO Logging
            }

            return false;
        }

        public bool UpdateBookmark(Bookmark entry)
        {
            return UpdateEntity(_ctx.Bookmarks, entry);
        }

        public bool DeleteBookmark(string id)
        {
            try
            {
                var entity = _ctx.Bookmarks.Where(p => p.Id.Equals(id)).FirstOrDefault();
                if (entity != null)
                {
                    _ctx.Bookmarks.Remove(entity);
                    return true;
                }
            }
            catch (Exception)
            {
                //TODO Logging
            }

            return false;
        }
        #endregion

        #region USERS
        public IQueryable<ApiUser> GetApiUsers()
        {
            return _ctx.ApiUsers;
        }

        public ApiUser GetUser(decimal id)
        {
            return _ctx.ApiUsers.FirstOrDefault(p => p.UserId.Equals(id));
        }

        public ApiUser GetUserByName(string username)
        {
            return _ctx.ApiUsers.FirstOrDefault(p => p.Email.Equals(username));
        }

        public string GetLastIdUser()
        {
            return _ctx.ApiUsers.Select(a => a.UserId).OrderBy(i => i).Last().ToString();            
        }

        public bool InsertUser(ApiUser entry)
        {
            try
            {
                _ctx.ApiUsers.Add(entry);
                return true;
            }
            catch (Exception)
            {
                //TODO Logging
            }

            return false;
        }

        public bool UpdateUser(ApiUser entry)
        {
            return UpdateEntity(_ctx.ApiUsers, entry);
        }

        public bool DeleteUser(string id)
        {
            try
            {
                var entity = _ctx.ApiUsers.Where(p => p.UserId.Equals(id)).FirstOrDefault();
                if (entity != null)
                {
                    _ctx.ApiUsers.Remove(entity);
                    return true;
                }
            }
            catch (Exception)
            {
                //TODO Logging
            }

            return false;
        }
        #endregion

        #region TOKENS
        public AuthToken GetAuthToken(string token)
        {
            return _ctx.AuthTokens.FirstOrDefault(t => t.Token.Equals(token));
        }

        public bool InsertToken(AuthToken entry)
        {
            try
            {
                _ctx.AuthTokens.Add(entry);
                return true;
            }
            catch (Exception)
            {
                //TODO Logging
            }

            return false;
        }

        public bool UpdateToken(AuthToken entry)
        {
            return UpdateEntity(_ctx.AuthTokens, entry);
        }

        public bool DeleteToken(string id)
        {
            try
            {
                var entity = _ctx.AuthTokens.Where(p => p.Token.Equals(id)).FirstOrDefault();
                if (entity != null)
                {
                    _ctx.AuthTokens.Remove(entity);
                    return true;
                }
            }
            catch (Exception)
            {
                //TODO Logging
            }

            return false;
        }

        #endregion

        #region SEARCH
        public IQueryable<SearchItem> Search()
        {
            return _ctx.SearchItems;
        }

        public IQueryable<SearchItem> SearchByAlias()
        {
            return _ctx.SearchItems.Where(p => p.Tipo.Equals(1));
        }

        public IQueryable<SearchItem> SearchByTrademark()
        {
            return _ctx.SearchItems.Where(p => p.Tipo.Equals(2));
        }

        public IQueryable<SearchItem> SearchByProduct()
        {
            return _ctx.SearchItems.Where(p => p.Tipo.Equals(3));
        }

        #endregion

        // Helper to update objects in context
        bool UpdateEntity<T>(DbSet<T> dbSet, T entity) where T : class
        {
            try
            {
                dbSet.AttachAsModified(entity, _ctx);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}