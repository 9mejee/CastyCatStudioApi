using DAL.Interfaces;
using DomainEntities;
using DTO.Product;
using System.Linq.Expressions;

namespace DAL.Repositeries
{
    public class ProductServices : IProductService
    {
        private readonly IGenericService<Product> _repository;

        public ProductServices(IGenericService<Product> productRepo)
        {
            this._repository = productRepo;
        }

        #region public methods

        public async Task<ProductDto> AddAsync(ProductDto dto)
        {
            try
            {
                var product = await _repository.AddWithSaveAsync(SetEntity(dto));
                return SetDto(product);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductDto> UpdateAsync(ProductDto dto)
        {
            try
            {
                var product = await _repository.UpdateWithSaveAsync(SetEntity(dto));
                return SetDto(product);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> IsExists(ProductDto dto)
        {
            try
            {
                return await _repository.IsAny(x => !x.Id.Equals(dto.Id) && dto.Name.ToLower().Equals(x.Name.ToLower()));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(ProductDto dto)
        {
            try
            {
                await _repository.DeleteWithSaveAsync(SetEntity(dto));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductDto> GetAsync(Expression<Func<Product, bool>> predicate)
        {
            try
            {
                var results = await _repository.GetAsync(predicate);
                return SetDto(results);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            try
            {
                var product = await _repository.GetAsync(id);
                return SetDto(product);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<ProductDto> List()
        {
            try
            {
                IQueryable<Product> products = _repository.GetAll();

                // return dto list
                return from p in products select SetDto(p);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region private methods

        private static ProductDto SetDto(Product dbProduct)
        {
            return new()
            {
                Id = dbProduct.Id,
                Name = dbProduct.Name,
                Description = dbProduct.Description
            };
        }

        private static Product SetEntity(ProductDto productDto)
        {
            return new()
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
            };
        }

        #endregion
    }
}