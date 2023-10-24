using DAL.Interfaces;
using DTO.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatsyCatStudio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            this._customerService = customerService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<CustomerDto>> GetAll()
        {
            try
            {
                return Ok(_customerService.List());
            }
            catch (Exception ex)
            {
                return BadRequest($"Error occurred in fetching customers. Error : {ex.Message}");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomerDto>> AddAsync(CustomerDto customer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload.");
                }

                bool isExists = await _customerService.IsExists(customer);
                return isExists ? (ActionResult<CustomerDto>)Conflict() : (ActionResult<CustomerDto>)Ok(await _customerService.AddAsync(customer));
            }
            catch (Exception ex)
            {
                return BadRequest($"Error occurred in adding customer info for name: {customer.Name}. Error : {ex.Message}");
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomerDto>> UpdateAsync(CustomerDto customer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload.");
                }

                bool isExists = await _customerService.IsExists(customer);
                if (isExists)
                {
                    return Conflict();
                }

                CustomerDto dto = await _customerService.GetByIdAsync(customer.Id);
                return dto is null ? (ActionResult<CustomerDto>)NotFound() : (ActionResult<CustomerDto>)Ok(await _customerService.UpdateAsync(customer));
            }
            catch (Exception ex)
            {
                return BadRequest($"Error occurred in updating customer info for name: {customer.Name}. Error : {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAsync(int Id)
        {
            try
            {
                var customer = await _customerService.GetByIdAsync(Id);

                if (customer is null)
                {
                    return NotFound();
                }

                await _customerService.DeleteAsync(customer);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error occurred in deleting customer for id: {Id}. Error : {ex.Message}");
            }
        }
    }
}