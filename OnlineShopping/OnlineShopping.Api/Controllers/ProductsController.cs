using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Api.Models;
using OnlineShopping.Api.Services;

namespace OnlineShopping.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [Route("sort")]
        public async Task<IActionResult> Sort(string sortOption)
        {
            List<Product> products = null;
            try
            {
                products = await _productService.GetProducts(sortOption);
            }
            catch (ArgumentException e)
            {
                // Log
                return BadRequest("Invalid Sortoption provided");
            }
            catch (Exception e)
            {
                // Log
            }
            return Ok(products);
        }
    }
}