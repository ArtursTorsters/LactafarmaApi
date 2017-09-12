using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LactafarmaAPI.Core;
using LactafarmaAPI.Core.Interfaces;
using LactafarmaAPI.Data.Entities;
using LactafarmaAPI.Data.Interfaces;
using LactafarmaAPI.Data.PagedData;
using LactafarmaAPI.Domain.Models.Base;
using LactafarmaAPI.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Alert = LactafarmaAPI.Domain.Models.Alert;
using Alias = LactafarmaAPI.Domain.Models.Alias;
using Brand = LactafarmaAPI.Domain.Models.Brand;
using Product = LactafarmaAPI.Domain.Models.Product;
using Risk = LactafarmaAPI.Domain.Models.Risk;
using Group = LactafarmaAPI.Domain.Models.Group;
using Log = LactafarmaAPI.Domain.Models.Log;
using User = LactafarmaAPI.Domain.Models.User;
using LactafarmaAPI.Domain.Models;

namespace LactafarmaAPI.Services.Services
{
    public class LactafarmaService : ILactafarmaService
    {
        #region Private Properties

        private readonly IAlertRepository _alertRepository;

        private readonly IAliasRepository _aliasRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IProductRepository _productRepository;
        private readonly IGroupRepository _groupRepository;

        private readonly ILogger<LactafarmaService> _logger;
        private readonly ILogRepository _logRepository;
        private readonly IFavoriteRepository _favoriteRepository;
        public LactafarmaAPI.Data.Entities.User _user;

        #endregion

        #region Constructors

        public LactafarmaService(ILogRepository logRepository, ILogger<LactafarmaService> logger,
            IAlertRepository alertRepository, IFavoriteRepository favoriteRepository,
            IAliasRepository aliasRepository, IProductRepository productRepository, IBrandRepository brandRepository,
            IGroupRepository groupRepository)
        {
            _logger = logger;
            _logRepository = logRepository;
            _favoriteRepository = favoriteRepository;
            _alertRepository = alertRepository;
            _aliasRepository = aliasRepository;
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _groupRepository = groupRepository;
        }

        #endregion

        #region Public Methods

        public async Task<IPagedList<Log>> GetLogsAsync(LogPagedDataRequest request)
        {
            _logger.LogInformation($"BEGIN GetLogs");
            try
            {
                return await Task.Run(() => MapLogs(_logRepository.GetLogs(request)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetLogs with message: {ex.Message}");
                return null;
            }
        }

        public void SetUser(Data.Entities.User currentUser)
        {
            _logger.LogInformation($"BEGIN SetUser");
            try
            {
                _user = currentUser;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on SetUser with message: {ex.Message}");
            }
        }

        public Task<IList<string>> GetLevelsAsync()
        {
            _logger.LogInformation($"BEGIN GetLevels");
            try
            {
                return _logRepository.GetLevels();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetLevels with message: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Get Favorites by Product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public IEnumerable<Domain.Models.Favorite> GetFavoritesByUser(Guid userId)
        {
            _logger.LogInformation($"BEGIN GetFavoritesByUser");
            try
            {
                return MapFavorites(_favoriteRepository.GetFavoritesByUser(userId));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetFavoritesByUser with message: {ex.Message}");
                return null;
            }

        }        

        public Domain.Models.Favorite GetFavorite(int favoriteId)
        {
            _logger.LogInformation($"BEGIN GetFavorite");
            try
            {
                var favorite = _favoriteRepository.GetFavorite(favoriteId);

                if (favorite == null) return null;

                var result = new Domain.Models.Favorite
                {
                    Id = favorite.Id,                    
                    Product = new Product
                    {
                        Id = favorite.ProductId,
                        Name = favorite.Name,
                        VirtualName = favorite.Name.RemoveDiacritics(),
                        Modified = favorite.Product.Modified,
                        Risk = new Risk
                        {
                            Description = favorite.Product.Risk.RisksMultilingual.FirstOrDefault().Description,
                            Id = favorite.Product.Risk.Id,
                            Modified = favorite.Product.Risk.Modified,
                            Name = favorite.Product.Risk.RisksMultilingual.FirstOrDefault().Name
                        },
                        Description = favorite.Product.ProductsMultilingual.FirstOrDefault().Description
                    }                    
                };
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetBrand with message: {ex.Message}");
                return null;
            }
        }


        /// <summary>
        ///     Get Alerts by productId
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public IEnumerable<Alert> GetAlertsByProduct(int productId)
        {
            _logger.LogInformation($"BEGIN GetAlertsByProduct");
            try
            {
                return MapAlerts(_alertRepository.GetAlertsByProduct(productId));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetAlertsByDrug with message: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        ///     Get last alerts
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Alert> GetLastAlerts()
        {
            _logger.LogInformation($"BEGIN GetLastAlerts");
            try
            {
                return MapAlerts(_alertRepository.GetLastAlerts());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetLastAlerts with message: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        ///     Get all Aliases
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BaseModel> GetAllAliases()
        {
            _logger.LogInformation($"BEGIN GetAllAliases");
            try
            {
                return MapAliases(_aliasRepository.GetAllAliases());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetAllAliases with message: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        ///     Get aliases by productId
        /// </summary>
        /// <param name="drugId"></param>
        /// <returns></returns>
        public IEnumerable<Alias> GetAliasesByProduct(int productId)
        {
            _logger.LogInformation($"BEGIN GetAliasesByDrug");
            try
            {
                return MapAliases(_aliasRepository.GetAliasesByProduct(productId));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetAliasesByDrug with message: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        ///     Get Brands by productId
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public IEnumerable<Brand> GetBrandsByProduct(int productId)
        {
            _logger.LogInformation($"BEGIN GetBrandsByProduct");
            try
            {
                return MapBrands(_brandRepository.GetBrandsByProduct(productId));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetBrandsByProduct with message: {ex.Message}");
                return null;
            }
        }

        public IEnumerable<Group> GetGroupsByProduct(int productId)
        {
            _logger.LogInformation($"BEGIN GetGroupsByProduct");
            try
            {
                return MapGroups(_groupRepository.GetGroupsByProduct(productId));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetGroupsByProduct with message: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        ///     Get all Brands
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BaseModel> GetAllBrands()
        {
            _logger.LogInformation($"BEGIN GetAllBrands");
            try
            {
                return MapBrands(_brandRepository.GetAllBrands());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetAllBrands with message: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        ///     Get all drugs
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BaseModel> GetAllProducts()
        {
            _logger.LogInformation($"BEGIN GetAllProducts");
            try
            {
                return MapProducts(_productRepository.GetAllProducts());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetAllProducts with message: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        ///     Get Products by groupId
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public IEnumerable<Product> GetProductsByGroup(int groupId)
        {
            _logger.LogInformation($"BEGIN GetProductsByGroup");
            try
            {
                return MapProductsForGroup(_productRepository.GetProductsByGroup(groupId));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetProductsByGroup with message: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        ///     Get Products by brandId
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        public IEnumerable<Product> GetProductsByBrand(int brandId)
        {
            _logger.LogInformation($"BEGIN GetProductsByBrand");
            try
            {
                return MapProductsForBrand(_productRepository.GetProductsByBrand(brandId));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetProductsByBrand with message: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        ///     Get all Groups
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BaseModel> GetAllGroups()
        {
            _logger.LogInformation($"BEGIN GetAllGroups");
            try
            {
                return MapGroups(_groupRepository.GetAllGroups());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetAllGroups with message: {ex.Message}");
                return null;
            }
        }


        /// <summary>
        ///     Get group by Id
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public Group GetGroup(int groupId)
        {
            _logger.LogInformation($"BEGIN GetGroup");
            try
            {
                var group = _groupRepository.GetGroup(groupId);

                if (group == null) return null;

                var result = new Group
                {
                    Id = group.GroupId,
                    Modified = group.Group.Modified,
                    Name = group.Name
                };
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetGroup with message: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        ///     Get User by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        //public User GetUser(string userId)
        //{
        //    _logger.LogInformation($"BEGIN GetUser");
        //    try
        //    {
        //        var user = _userRepository.GetUser(userId);
        //        if (user == null) return null;
        //        var result = new User
        //        {
        //            AppId = user.AppId,
        //            Email = user.Email,
        //            FacebookInfo = user.FacebookInfo,
        //            GoogleInfo = user.GoogleInfo,
        //            Id = user.Id,
        //            Name = user.Name,
        //            TwitterInfo = user.TwitterInfo
        //        };
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Exception on GetUser with message: {ex.Message}");
        //        return null;
        //    }
        //}

        /// <summary>
        ///     Get Alias information by id (including associated Drug)
        /// </summary>
        /// <param name="aliasId"></param>
        /// <returns></returns>
        public Alias GetAlias(int aliasId)
        {
            _logger.LogInformation($"BEGIN GetAlias");
            try
            {
                var alias = _aliasRepository.GetAlias(aliasId);

                if (alias == null) return null;

                var result = new Alias
                {
                    Id = alias.AliasId,
                    Name = alias.Name,
                    Product = GetProductByAlias(aliasId)
                };
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetAlias with message: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        ///     Get Drug information by AliasId
        /// </summary>
        /// <param name="aliasId"></param>
        /// <returns></returns>
        public Product GetProductByAlias(int aliasId)
        {
            _logger.LogInformation($"BEGIN GetProductByAlias");
            try
            {
                var product = _aliasRepository.GetProductByAlias(aliasId);

                if (product == null) return null;

                var result = new Product
                {
                    Id = product.ProductId,
                    Name = product.Name,
                    VirtualName = product.Name.RemoveDiacritics(),
                    Modified = product.Product.Modified,
                    Risk = new Risk
                    {
                        Description = product.Product.Risk.RisksMultilingual.FirstOrDefault().Description,
                        Id = product.Product.Risk.Id,
                        Modified = product.Product.Risk.Modified,
                        Name = product.Product.Risk.RisksMultilingual.FirstOrDefault().Name
                    },
                    Description = product.Description
                };
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetDrugByAlias with message: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        ///     Get Brand information
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        public Brand GetBrand(int brandId)
        {
            _logger.LogInformation($"BEGIN GetBrand");
            try
            {
                var brand = _brandRepository.GetBrand(brandId);

                if (brand == null) return null;

                var result = new Brand
                {
                    Id = brand.BrandId,
                    Name = brand.Name
                };
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetBrand with message: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        ///     Get Product information
        /// </summary>
        /// <param name="drugId"></param>
        /// <returns></returns>
        public Product GetProduct(int productId)
        {
            _logger.LogInformation($"BEGIN GetProduct");
            try
            {
                var product = _productRepository.GetProduct(productId);

                if (product == null) return null;

                var result = new Product
                {
                    Id = product.ProductId,
                    Name = product.Product.ProductsMultilingual.FirstOrDefault().Name,
                    VirtualName = product.Product.ProductsMultilingual.FirstOrDefault().Name.RemoveDiacritics(),
                    Modified = product.Product.Modified,
                    Risk = new Risk
                    {
                        Description = product.Product.Risk.RisksMultilingual.FirstOrDefault().Description,
                        Id = product.Product.Risk.Id,
                        Modified = product.Product.Risk.Modified,
                        Name = product.Product.Risk.RisksMultilingual.FirstOrDefault().Name
                    },
                    Description = product.Product.ProductsMultilingual.FirstOrDefault().Description
                };

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetProduct with message: {ex.Message}");
                return null;
            }
        }

        #endregion

        #region Private Methods

        private PagedList<Log> MapLogs(Task<IPagedList<Data.Entities.Log>> logs)
        {
            var collection = new PagedList<Log>();
            foreach (var log in logs.Result.ToList())
            {
                var result = new Log
                {
                    Id = log.Id,
                    Message = log.Message,
                    Action = log.Action,
                    Application = log.Application,
                    Callsite = log.Callsite,
                    Controller = log.Controller,
                    Exception = log.Exception,
                    Identity = log.Identity,
                    Level = log.Level,
                    Logged = log.Logged,
                    Logger = log.Logger,
                    Referrer = log.Referrer,
                    Url = log.Url,
                    UserAgent = log.UserAgent
                };
                collection.Add(result);
            }

            return collection.Count == 0 ? null : collection;
        }

        private static IEnumerable<Domain.Models.Favorite> MapFavorites(IEnumerable<Data.Entities.Favorite> favorites)
        {
            var collection = new List<Domain.Models.Favorite>();
            foreach (var favorite in favorites.ToList())
            {
                var result = new Domain.Models.Favorite
                {
                    Id = favorite.Id,
                    Name = favorite.Product.ProductsMultilingual.FirstOrDefault().Name,
                    Product = new Product
                    {
                        Id = favorite.ProductId,
                        Name = favorite.Product.ProductsMultilingual.FirstOrDefault().Name,
                        VirtualName = favorite.Product.ProductsMultilingual.FirstOrDefault().Name.RemoveDiacritics(),
                        Modified = favorite.Product.Modified,
                        Risk = new Risk
                        {
                            Description = favorite.Product.Risk.RisksMultilingual.FirstOrDefault().Description,
                            Id = favorite.Product.Risk.Id,
                            Modified = favorite.Product.Risk.Modified,
                            Name = favorite.Product.Risk.RisksMultilingual.FirstOrDefault().Name
                        },
                        Description = favorite.Product.ProductsMultilingual.FirstOrDefault().Description
                    }
                };
                collection.Add(result);
            }

            return collection.Count == 0 ? null : collection;
        }

        private static IEnumerable<Alert> MapAlerts(IEnumerable<Data.Entities.Alert> alerts)
        {
            var collection = new List<Alert>();
            foreach (var alert in alerts.ToList())
            {
                var result = new Alert
                {
                    Id = alert.Id,
                    Name = alert.Product.ProductsMultilingual.FirstOrDefault().Name,                    
                    Created = alert.Created,
                    OldRisk = alert.OldRisk.RisksMultilingual.FirstOrDefault().Name,
                    NewRisk = alert.NewRisk.RisksMultilingual.FirstOrDefault().Name
                };
                collection.Add(result);
            }

            return collection.Count == 0 ? null : collection;
        }

        private static IEnumerable<Alias> MapAliases(IEnumerable<AliasMultilingual> aliases)
        {
            var collection = new List<Alias>();
            foreach (var alias in aliases.ToList())
            {
                var result = new Alias
                {
                    Id = alias.AliasId,
                    Name = alias.Name,
                    VirtualName = alias.Name.RemoveDiacritics()
                };
                collection.Add(result);
            }

            return collection.Count == 0 ? null : collection;
        }

        private static IEnumerable<Group> MapGroups(IEnumerable<GroupMultilingual> groups)
        {
            var collection = new List<Group>();

            foreach (var group in groups)
            {
                var result = new Group
                {
                    Id = group.GroupId,
                    Name = group.Name,
                    Modified = group.Group.Modified,
                    VirtualName = group.Name.RemoveDiacritics()
                };

                collection.Add(result);
            }

            return collection.Count == 0 ? null : collection;
        }

        private static IEnumerable<Brand> MapBrands(IEnumerable<BrandMultilingual> brands)
        {
            var collection = new List<Brand>();

            foreach (var brand in brands)
            {
                var result = new Brand
                {
                    Id = brand.BrandId,
                    Name = brand.Name,
                    VirtualName = brand.Name.RemoveDiacritics()
                };

                collection.Add(result);
            }

            return collection.Count == 0 ? null : collection;
        }

        private IEnumerable<Product> MapProductsForGroup(IEnumerable<ProductGroup> products)
        {
            var collection = new List<Product>();

            foreach (var product in products)
            {
                var result = new Product
                {
                    Id = product.ProductId,
                    Name = product.Product.ProductsMultilingual.FirstOrDefault().Name,
                    VirtualName = product.Product.ProductsMultilingual.FirstOrDefault().Name.RemoveDiacritics(),
                    Modified = product.Product.Modified,
                    Risk = new Risk
                    {
                        Description = product.Product.Risk.RisksMultilingual.FirstOrDefault().Description,
                        Id = product.Product.Risk.Id,
                        Modified = product.Product.Risk.Modified,
                        Name = product.Product.Risk.RisksMultilingual.FirstOrDefault().Name
                    },
                    Description = product.Product.ProductsMultilingual.FirstOrDefault().Description
                };

                collection.Add(result);
            }

            return collection.Count == 0 ? null : collection;
        }

        private IEnumerable<Product> MapProductsForBrand(IEnumerable<ProductBrand> products)
        {
            var collection = new List<Product>();

            foreach (var product in products)
            {
                var result = new Product
                {
                    Id = product.ProductId,
                    Name = product.Product.ProductsMultilingual.FirstOrDefault().Name,
                    VirtualName = product.Product.ProductsMultilingual.FirstOrDefault().Name.RemoveDiacritics(),
                    Modified = product.Product.Modified,
                    Risk = new Risk
                    {
                        Description = product.Product.Risk.RisksMultilingual.FirstOrDefault().Description,
                        Id = product.Product.Risk.Id,
                        Modified = product.Product.Risk.Modified,
                        Name = product.Product.Risk.RisksMultilingual.FirstOrDefault().Name
                    },
                    Description = product.Product.ProductsMultilingual.FirstOrDefault().Description
                };

                collection.Add(result);
            }

            return collection.Count == 0 ? null : collection;
        }

        private IEnumerable<Product> MapProducts(IEnumerable<ProductMultilingual> products)
        {
            var collection = new List<Product>();

            foreach (var product in products)
            {
                var result = new Product
                {
                    Id = product.ProductId,
                    Name = product.Name,
                    VirtualName = product.Name.RemoveDiacritics(),
                    Modified = product.Product.Modified,
                    Risk = new Risk {
                        Description = product.Product.Risk.RisksMultilingual.FirstOrDefault().Description,
                        Id = product.Product.Risk.Id,
                        Modified = product.Product.Risk.Modified,
                        Name = product.Product.Risk.RisksMultilingual.FirstOrDefault().Name
                    },
                    Description = product.Description
                };

                collection.Add(result);
            }

            return collection.Count == 0 ? null : collection;
        }

 
        #endregion
    }
}