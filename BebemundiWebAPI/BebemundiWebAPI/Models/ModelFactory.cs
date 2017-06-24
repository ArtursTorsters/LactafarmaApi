using BebemundiWebAPI.Entities;
using BebemundiWebAPI.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace BebemundiWebAPI.Models
{
    public class ModelFactory
    {
        private UrlHelper _urlHelper;
        private IBebemundiWebAPIRepository _repo;
        public ModelFactory(HttpRequestMessage request, IBebemundiWebAPIRepository repo)
        {
            _urlHelper = new UrlHelper(request);
            _repo = repo;
        }

        public ProductModel Create(Product product)
        {
            return new ProductModel()
            {
                Url = _urlHelper.Link("Identifiers", new { id = product.Id }),
                Product = product
            };
        }

        public Product Parse(ProductModel product)
        {
            try
            {
                var entity = new Product();
                entity.Id = product.Product.Id;
                entity.IdGrupo = product.Product.IdGrupo;
                entity.Nombre = product.Product.Nombre;
                entity.Riesgo = product.Product.Riesgo;
                entity.Fecha = product.Product.Fecha;
                entity.DescripcionRiesgo = product.Product.DescripcionRiesgo;
                entity.Comentario = product.Product.Comentario;

                return entity;
            }
            catch
            {
                return null;
            }
        }

        public GroupModel Create(Group group)
        {
            return new GroupModel()
            {
                Url = _urlHelper.Link("Identifiers", new { id = group.Id }),
                Group = group
            };
        }

        public Group Parse(GroupModel group)
        {
            try
            {
                var entity = new Group();
                entity.Id = group.Group.Id;
                entity.Nombre = group.Group.Nombre;
                entity.Fecha = group.Group.Fecha;

                return entity;
            }
            catch
            {
                return null;
            }
        }

        public AliasModel Create(Alias alias)
        {
            return new AliasModel()
            {
                Url = _urlHelper.Link("Identifiers", new { id = alias.Id }),
                Alias = alias                
            };
        }

        public Alias Parse(AliasModel alias)
        {
            try
            {
                var entity = new Alias();
                entity.Id = alias.Alias.Id;
                entity.IdMedicamento = alias.Alias.IdMedicamento;
                entity.Nombre = alias.Alias.Nombre;

                return entity;
            }
            catch
            {
                return null;
            }
        }

        public TrademarkModel Create(Trademark trademark)
        {
            return new TrademarkModel()
            {
                Url = _urlHelper.Link("Identifiers", new { id = trademark.Id }),
                Trademark = trademark
            };
        }        

        public Trademark Parse(TrademarkModel trademark)
        {
            try
            {
                var entity = new Trademark();
                entity.Id = trademark.Trademark.Id;
                entity.Nombre = trademark.Trademark.Nombre;

                return entity;
            }
            catch
            {
                return null;
            }
        }

        public SearchItemModel Create(SearchItem searchItem)
        {
            return new SearchItemModel()
            {
                Url = _urlHelper.Link("Identifiers", new { id = searchItem.Id }),
                SearchItem = searchItem
            };
        }

        public BookmarkModel Create(Bookmark bookmark)
        {
            return new BookmarkModel()
            {
                Url = _urlHelper.Link("Identifiers", new { id = bookmark.Id }),
                Bookmark = bookmark
            };
        }

        public Bookmark Parse(BookmarkModel bookmark)
        {
            try
            {
                var entity = new Bookmark();
                entity.Id = bookmark.Bookmark.Id;
                entity.IdMedicamento = bookmark.Bookmark.IdMedicamento;
                entity.IdUsuario = bookmark.Bookmark.IdUsuario;

                return entity;
            }
            catch
            {
                return null;
            }
        }

        public UserModel Create(ApiUser user)
        {
            return new UserModel()
            {
                Url = _urlHelper.Link("Identifiers", new { id = user.UserId }),
                User = user
            };
        }

        public ApiUser Parse(UserModel user)
        {
            try
            {
                var entity = new ApiUser();                
                entity.UserId = user.User.UserId;
                entity.Secret = user.User.Secret;
                entity.AppId = user.User.AppId;
                entity.Email = user.User.Email;

                return entity;
            }
            catch
            {
                return null;
            }
        }

        public AuthTokenModel Create(AuthToken authToken)
        {
            return new AuthTokenModel()
            {
                Token = authToken.Token,
                Expiration = authToken.Expiration
            };
        }
        
    }
}