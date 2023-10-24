using DAL.Interfaces;
using DTO.OrderRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatsyCatStudio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService OrderService)
        {
            this._orderService = OrderService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<RequestOrderCustomerProductsDto>> GetAll()
        {
            try
            {
                return Ok(_orderService.List());
            }
            catch (Exception ex)
            {
                return BadRequest($"Error occurred in fetching Orders. Error : {ex.Message}");
            }
        }

        [HttpGet]
        [Route("{CustomerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<RequestOrderCustomerProductsDto>>> GetAllOrdersAgainstCustomerAsync(int CustomerId)
        {
            try
            {
                var results = await _orderService.GetAllOrdersAgainstCustomerAsyc(CustomerId);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error occurred in fetching all Orders against customer. Error : {ex.Message}");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddAsync(RequestOrderCustomerProductsDto requestDto)
        {
            try
            {
                if (requestDto.Customer is null || requestDto.Customer.Id == 0)
                {
                    return BadRequest("No customer added.");
                }

                if (requestDto.Customer.Products is null || !requestDto.Customer.Products.Any())
                {
                    return BadRequest("No products added.");
                }

                await _orderService.AddWithCustomerProductsAsync(requestDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error occurred in adding Order info. Error : {ex.Message}");
            }
        }
    }
}