using DAL.Interfaces;
using DTO.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatsyCatStudio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<ProductDto>> GetAll()
        {
            try
            {
                return Ok(_productService.List());
            }
            catch (Exception ex)
            {
                return BadRequest($"Error occurred in fetching products. Error : {ex.Message}");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductDto>> AddAsync(ProductDto product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload.");
                }

                bool isExists = await _productService.IsExists(product);
                return isExists ? Conflict() : Ok(await _productService.AddAsync(product));
            }
            catch (Exception ex)
            {
                return BadRequest($"Error occurred in adding product info. Error : {ex.Message}");
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductDto>> UpdateAsync(ProductDto product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload.");
                }

                bool isExists = await _productService.IsExists(product);
                if (isExists)
                {
                    return Conflict();
                }

                ProductDto dto = await _productService.GetByIdAsync(product.Id);
                return dto is null ? NotFound() : Ok(await _productService.UpdateAsync(product));
            }
            catch (Exception ex)
            {
                return BadRequest($"Error occurred in updating product info. Error : {ex.Message}");
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
                var product = await _productService.GetByIdAsync(Id);

                if (product is null)
                {
                    return NotFound();
                }

                await _productService.DeleteAsync(product);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error occurred in deleting product for id: {Id}. Error : {ex.Message}");
            }
        }
    }
}