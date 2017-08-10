using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using LactafarmaAPI.Data.Interfaces;
using LactafarmaAPI.Domain.Models;

namespace LactafarmaAPI.Services
{
    public class LactafarmaService : ILactafarmaService
    {
        IAlertRepository _alertRepository;
        IAliasRepository _aliasRepository;
        IDrugRepository _drugRepository;
        IBrandRepository _brandRepository;
        IGroupRepository _groupRepository;
        IUserRepository _userRepository;

        public LactafarmaService(IAlertRepository alertRepository,
            IAliasRepository aliasRepository, IDrugRepository drugRepository, IBrandRepository brandRepository,
            IGroupRepository groupRepository, IUserRepository userRepository)
        {
            _alertRepository = alertRepository;
            _aliasRepository = aliasRepository;
            _drugRepository = drugRepository;
            _brandRepository = brandRepository;
            _groupRepository = groupRepository;
            _userRepository = userRepository;
        }

        public IEnumerable<Alert> GetAlertsByDrug(int drugId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Alias> GetAliasesByDrug(int drugId)
        {
            var aliases = _aliasRepository.GetAliasesByDrug(drugId);
            return MapAliases(aliases);
        }

        private static List<Alias> MapAliases(IEnumerable<Data.Entities.Alias> aliases)
        {
            var collection = new List<Alias>();

            foreach (var alias in aliases)
            {
                Alias result = new Alias
                {
                    Id = alias.Id,
                    Name = alias.AliasMultilingual.FirstOrDefault().Name
                };
                collection.Add(result);
            }

            return collection;
        }

        public IEnumerable<Brand> GetBrandsByDrug(int drugId)
        {
            var brands = _brandRepository.GetBrandsByDrug(drugId);
            return MapBrands(brands);
        }

        private static List<Brand> MapBrands(IEnumerable<Data.Entities.Brand> brands)
        {
            var collection = new List<Brand>();

            foreach (var brand in brands)
            {
                Brand result = new Brand
                {
                    Id = brand.Id,
                    Name = brand.BrandsMultilingual.FirstOrDefault().Name
                };

                collection.Add(result);
            }

            return collection;
        }

        public IEnumerable<Drug> GetDrugsByGroup(int groupId)
        {
            var drugs = _drugRepository.GetDrugsByGroup(groupId);
            return MapDrugsForGroup(drugs);
        }

        private IEnumerable<Drug> MapDrugsForGroup(IEnumerable<Data.Entities.Drug> drugs)
        {
            var collection = new List<Drug>();

            foreach (var drug in drugs)
            {
                Drug result = new Drug
                {
                    Id = drug.Id,
                    Name = drug.DrugsMultilingual.FirstOrDefault().Name
                };
                
                collection.Add(result);
            }

            return collection;
        }

        public IEnumerable<Drug> GetDrugsByBrand(int brandId)
        {
            var drugs = _drugRepository.GetDrugsByBrand(brandId);
            return MapDrugsForBrand(drugs);
        }

        private IEnumerable<Drug> MapDrugsForBrand(IEnumerable<Data.Entities.Drug> drugs)
        {
            var collection = new List<Drug>();

            foreach (var drug in drugs)
            {
                Drug result = new Drug
                {
                    Id = drug.Id,
                    Name = drug.DrugsMultilingual.FirstOrDefault().Name
                };

                collection.Add(result);
            }

            return collection;
        }

        public Group GetGroup(int groupId)
        {
            var group = _groupRepository.GetGroup(groupId);

            var result = new Group
            {
                Id = @group.Id,
                Modified = @group.Modified,
                Name = @group.GroupsMultilingual.FirstOrDefault().Name
            };

            return result;
        }

        public User GetUser(Guid userId)
        {
            var user = _userRepository.GetUser(userId);
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

        public Alias GetAlias(int aliasId)
        {
            var alias = _aliasRepository.GetAlias(aliasId);

            var result = new Alias
            {
                Id = alias.Id,
                Name = alias.AliasMultilingual.FirstOrDefault().Name,
                Drug = new Drug
                {
                    Comment = alias.Drug.DrugsMultilingual.FirstOrDefault().Comment,
                    Description = alias.Drug.DrugsMultilingual.FirstOrDefault().Description,
                    Modified = alias.Drug.Modified,
                    Name = alias.Drug.DrugsMultilingual.FirstOrDefault().Name,
                    Risk = alias.Drug.DrugsMultilingual.FirstOrDefault().Risk,
                    Id = alias.Drug.DrugsMultilingual.FirstOrDefault().DrugId
                }
            };
            return result;
        }

        public Brand GetBrand(int brandId)
        {
            var brand = _brandRepository.GetBrand(brandId);

            var result = new Brand
            {
                Id = brand.Id,
                Name = brand.BrandsMultilingual.FirstOrDefault().Name
            };
            return result;
        }

        public Drug GetDrug(int drugId)
        {
            var drug = _drugRepository.GetDrug(drugId);
            var result = new Drug
            {
                Comment = drug.DrugsMultilingual.FirstOrDefault().Comment,
                Description = drug.DrugsMultilingual.FirstOrDefault().Description,
                Modified = drug.Modified,
                Name = drug.DrugsMultilingual.FirstOrDefault().Name,
                Risk = drug.DrugsMultilingual.FirstOrDefault().Risk,
                Id = drug.DrugsMultilingual.FirstOrDefault().DrugId
            };

            return result;
        }

        public Drug GetDrugByAlias(int aliasId)
        {
            var drug = _aliasRepository.GetDrugByAlias(aliasId);

            var result = new Drug
            {
                Comment = drug.DrugsMultilingual.FirstOrDefault().Comment,
                Description = drug.DrugsMultilingual.FirstOrDefault().Description,
                Modified = drug.Modified,
                Name = drug.DrugsMultilingual.FirstOrDefault().Name,
                Risk = drug.DrugsMultilingual.FirstOrDefault().Risk,
                Id = drug.DrugsMultilingual.FirstOrDefault().DrugId
            };

            return result;

        }
    }
}
