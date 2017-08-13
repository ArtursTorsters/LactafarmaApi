using System;
using System.Collections.Generic;
using System.Linq;
using LactafarmaAPI.Core;
using LactafarmaAPI.Data.Entities;
using LactafarmaAPI.Data.Interfaces;
using LactafarmaAPI.Domain.Models.Base;
using LactafarmaAPI.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Alert = LactafarmaAPI.Domain.Models.Alert;
using Alias = LactafarmaAPI.Domain.Models.Alias;
using Brand = LactafarmaAPI.Domain.Models.Brand;
using Drug = LactafarmaAPI.Domain.Models.Drug;
using Group = LactafarmaAPI.Domain.Models.Group;
using User = LactafarmaAPI.Domain.Models.User;

namespace LactafarmaAPI.Services.Services
{
    public class LactafarmaService : ILactafarmaService
    {
        #region Private Properties

        private readonly IAlertRepository _alertRepository;

        private readonly IAliasRepository _aliasRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IDrugRepository _drugRepository;
        private readonly IGroupRepository _groupRepository;

        private readonly ILogger<LactafarmaService> _logger;
        private readonly IUserRepository _userRepository;

        #endregion

        #region Constructors

        public LactafarmaService(ILogger<LactafarmaService> logger, IAlertRepository alertRepository,
            IAliasRepository aliasRepository, IDrugRepository drugRepository, IBrandRepository brandRepository,
            IGroupRepository groupRepository, IUserRepository userRepository)
        {
            _logger = logger;
            _alertRepository = alertRepository;
            _aliasRepository = aliasRepository;
            _drugRepository = drugRepository;
            _brandRepository = brandRepository;
            _groupRepository = groupRepository;
            _userRepository = userRepository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Get Alerts by drugIds
        /// </summary>
        /// <param name="drugId"></param>
        /// <returns></returns>
        public IEnumerable<Alert> GetAlertsByDrug(int drugId)
        {
            _logger.LogInformation($"BEGIN GetAlertsByDrug");
            try
            {
                return MapAlerts(_alertRepository.GetAlertsByDrug(drugId));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetAlertsByDrug with message: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        ///     Get all alerts
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BaseModel> GetAllAlerts()
        {
            _logger.LogInformation($"BEGIN GetAllAlerts");
            try
            {
                return MapAlerts(_alertRepository.GetAllAlerts());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetAllAlerts with message: {ex.Message}");
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
        ///     Get aliases by drugId
        /// </summary>
        /// <param name="drugId"></param>
        /// <returns></returns>
        public IEnumerable<Alias> GetAliasesByDrug(int drugId)
        {
            _logger.LogInformation($"BEGIN GetAliasesByDrug");
            try
            {
                return MapAliases(_aliasRepository.GetAliasesByDrug(drugId));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetAliasesByDrug with message: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        ///     Get Brands by drugId
        /// </summary>
        /// <param name="drugId"></param>
        /// <returns></returns>
        public IEnumerable<Brand> GetBrandsByDrug(int drugId)
        {
            _logger.LogInformation($"BEGIN GetBrandsByDrug");
            try
            {
                return MapBrands(_brandRepository.GetBrandsByDrug(drugId));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetBrandsByDrug with message: {ex.Message}");
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
        public IEnumerable<BaseModel> GetAllDrugs()
        {
            _logger.LogInformation($"BEGIN GetAllDrugs");
            try
            {
                return MapDrugs(_drugRepository.GetAllDrugs());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetAllDrugs with message: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        ///     Get Drugs by groupId
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public IEnumerable<Drug> GetDrugsByGroup(int groupId)
        {
            _logger.LogInformation($"BEGIN GetDrugsByGroup");
            try
            {
                return MapDrugsForGroup(_drugRepository.GetDrugsByGroup(groupId));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetDrugsByGroup with message: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        ///     Get Drugs by brandId
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        public IEnumerable<Drug> GetDrugsByBrand(int brandId)
        {
            _logger.LogInformation($"BEGIN GetDrugsByBrand");
            try
            {
                return MapDrugsForBrand(_drugRepository.GetDrugsByBrand(brandId));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetDrugsByBrand with message: {ex.Message}");
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
        public User GetUser(Guid userId)
        {
            _logger.LogInformation($"BEGIN GetUser");
            try
            {
                var user = _userRepository.GetUser(userId);
                if (user == null) return null;
                var result = new User
                {
                    AppId = user.AppId,
                    Email = user.Email,
                    FacebookInfo = user.FacebookInfo,
                    GoogleInfo = user.GoogleInfo,
                    Id = user.Id,
                    Name = user.Name,
                    TwitterInfo = user.TwitterInfo
                };
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetUser with message: {ex.Message}");
                return null;
            }
        }

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
                    Drug = GetDrugByAlias(aliasId)
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
        public Drug GetDrugByAlias(int aliasId)
        {
            _logger.LogInformation($"BEGIN GetDrugByAlias");
            try
            {
                var drug = _aliasRepository.GetDrugByAlias(aliasId);

                if (drug == null) return null;

                var result = new Drug
                {
                    Id = drug.DrugId,
                    Name = drug.Name,
                    Comment = drug.Comment,
                    Description = drug.Description,
                    Risk = drug.Risk,
                    Modified = drug.Drug.Modified
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
        ///     Get Drug information
        /// </summary>
        /// <param name="drugId"></param>
        /// <returns></returns>
        public Drug GetDrug(int drugId)
        {
            _logger.LogInformation($"BEGIN GetDrug");
            try
            {
                var drug = _drugRepository.GetDrug(drugId);

                if (drug == null) return null;

                var result = new Drug
                {
                    Comment = drug.Comment,
                    Description = drug.Description,
                    Modified = drug.Drug.Modified,
                    Name = drug.Name,
                    Risk = drug.Risk,
                    Id = drug.DrugId
                };
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetDrug with message: {ex.Message}");
                return null;
            }
        }

        #endregion

        #region Private Methods

        private static IEnumerable<Alert> MapAlerts(IEnumerable<AlertMultilingual> alerts)
        {
            var collection = new List<Alert>();
            foreach (var alert in alerts.ToList())
            {
                var result = new Alert
                {
                    Id = alert.AlertId,
                    Name = alert.Name,
                    Comment = alert.Comment,
                    Created = alert.Alert.Created,
                    Risk = alert.Risk
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

        private IEnumerable<Drug> MapDrugsForGroup(IEnumerable<DrugMultilingual> drugs)
        {
            var collection = new List<Drug>();

            foreach (var drug in drugs)
            {
                var result = new Drug
                {
                    Id = drug.DrugId,
                    Name = drug.Name
                };

                collection.Add(result);
            }

            return collection.Count == 0 ? null : collection;
        }

        private IEnumerable<Drug> MapDrugsForBrand(IEnumerable<DrugBrand> drugs)
        {
            var collection = new List<Drug>();

            foreach (var drug in drugs)
            {
                var result = new Drug
                {
                    Id = drug.DrugId,
                    Name = drug.Drug.DrugsMultilingual.FirstOrDefault().Name,
                    Modified = drug.Drug.Modified
                };

                collection.Add(result);
            }

            return collection.Count == 0 ? null : collection;
        }

        private IEnumerable<Drug> MapDrugs(IEnumerable<DrugMultilingual> drugs)
        {
            var collection = new List<Drug>();

            foreach (var drug in drugs)
            {
                var result = new Drug
                {
                    Id = drug.DrugId,
                    Name = drug.Name,
                    VirtualName = drug.Name.RemoveDiacritics(),
                    Modified = drug.Drug.Modified,
                    Comment = drug.Comment,
                    Risk = drug.Risk,
                    Description = drug.Description
                };

                collection.Add(result);
            }

            return collection.Count == 0 ? null : collection;
        }

        #endregion
    }
}