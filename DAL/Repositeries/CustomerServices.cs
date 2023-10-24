using DAL.Interfaces;
using DomainEntities;
using DTO.Customer;
using System.Linq.Expressions;

namespace DAL.Repositeries
{
    public class CustomerServices : ICustomerService
    {
        private readonly IGenericService<Customer> _repository;

        public CustomerServices(IGenericService<Customer> customerRepo)
        {
            this._repository = customerRepo;
        }

        #region public methods

        public async Task<CustomerDto> AddAsync(CustomerDto dto)
        {
            try
            {
                var customer = await _repository.AddWithSaveAsync(SetEntity(dto));
                return SetDto(customer);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CustomerDto> UpdateAsync(CustomerDto dto)
        {
            try
            {
                var customer = await _repository.UpdateWithSaveAsync(SetEntity(dto));
                return SetDto(customer);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> IsExists(CustomerDto dto)
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

        public async Task DeleteAsync(CustomerDto dto)
        {
            try
            {
                var entity = await _repository.GetAsync(dto.Id);
                await _repository.DeleteWithSaveAsync(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CustomerDto> GetAsync(Expression<Func<Customer, bool>> predicate)
        {
            try
            {
                var entity = await _repository.GetAsync(predicate);
                return SetDto(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CustomerDto> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _repository.GetAsync(id);
                return SetDto(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<CustomerDto> List()
        {
            try
            {
                IQueryable<Customer> customers = _repository.GetAll();

                // return dto list
                return from c in customers select SetDto(c);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region Private Methods
        private static CustomerDto SetDto(Customer dbCustomer)
        {
            return new()
            {
                Id = dbCustomer.Id,
                Name = dbCustomer.Name,
                Address = dbCustomer.Address,
            };
        }

        private static Customer SetEntity(CustomerDto customerDto)
        {
            return new()
            {
                Id = customerDto.Id,
                Name = customerDto.Name,
                Address = customerDto.Address,
            };
        }

        #endregion
    }
}