using LactafarmaAPI.Core.Interfaces;
using LactafarmaAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LactafarmaAPI.Data.Interfaces
{
    public interface IProductRepository : IDataRepository<Product>
    {
        IEnumerable<ProductMultilingual> GetAllProducts();
        IEnumerable<ProductGroup> GetProductsByGroup(int groupId);
        IEnumerable<ProductBrand> GetProductsByBrand(int brandId);
        ProductMultilingual GetProduct(int productId);

    }
}
