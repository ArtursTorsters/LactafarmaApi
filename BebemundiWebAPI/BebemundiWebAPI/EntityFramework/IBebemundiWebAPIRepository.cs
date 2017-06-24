using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BebemundiWebAPI.Entities;

namespace BebemundiWebAPI.EntityFramework
{
    public interface IBebemundiWebAPIRepository
    {
        // General
        bool SaveAll();

        // Product
        IQueryable<Product> GetAllProducts();
        IQueryable<Product> FindProductsByNameStartsWith(string query);
        IQueryable<Product> GetProductsByGroup(string group);
        IEnumerable<Product> GetProductsByProduct(string product);
        Product GetProduct(string id);
        string GetLastIdProduct();
        bool InsertProduct(Product entry);
        bool UpdateProduct(Product entry);
        bool DeleteProduct(string id);

        // Group
        IQueryable<Group> GetAllGroups();
        IQueryable<Group> FindGroupsByNameStartsWith(string query);
        Group GetGroup(string id);
        string GetLastIdGroup();
        bool InsertGroup(Group entry);
        bool UpdateGroup(Group entry);
        bool DeleteGroup(string id);

        // Alias
        IQueryable<Alias> GetAllAliases();
        IQueryable<Alias> FindAliasesByNameStartsWith(string query);
        IQueryable<Alias> GetAliasesByProduct(string product);
        Alias GetAlias(string id);
        string GetLastIdAlias();
        bool InsertAlias(Alias entry);
        bool UpdateAlias(Alias entry);
        bool DeleteAlias(string id);

        // Trademark
        IQueryable<Trademark> GetAllTrademarks();
        IQueryable<Trademark> FindTrademarksByNameStartsWith(string query);
        IQueryable<Trademark> GetTrademarksByProduct(string product);
        Trademark GetTrademark(string id);
        string GetLastIdTrademark();
        bool InsertTrademark(Trademark entry);
        bool UpdateTrademark(Trademark entry);
        bool DeleteTrademark(string id);

        // Bookmarks
        IQueryable<Bookmark> GetBookmarksByUser(string user);
        Bookmark GetBookmark(string p);
        string GetLastIdBookmark();
        bool InsertBookmark(Bookmark entry);
        bool UpdateBookmark(Bookmark entry);
        bool DeleteBookmark(string id);

        // Users
        IQueryable<ApiUser> GetApiUsers();
        ApiUser GetUser(decimal p);
        ApiUser GetUserByName(string username);
        string GetLastIdUser();
        bool InsertUser(ApiUser entry);
        bool UpdateUser(ApiUser entry);
        bool DeleteUser(string id);

        // Tokens
        AuthToken GetAuthToken(string token);
        bool InsertToken(AuthToken entry);
        bool UpdateToken(AuthToken entry);
        bool DeleteToken(string id);

        // Search
        IQueryable<SearchItem> Search();
        IQueryable<SearchItem> SearchByAlias();
        IQueryable<SearchItem> SearchByTrademark();
        IQueryable<SearchItem> SearchByProduct();
    }
}
