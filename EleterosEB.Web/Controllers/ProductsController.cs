using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EleterosEB.Bll;
using EleterosEB.Domain;
using EleterosEB.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace EleterosEB.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController: ControllerBase
    {
        private readonly ProductService _productService;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public ProductsController(ProductService productService, IMapper mapper,
            LinkGenerator linkGenerator)
        {
            _productService = productService;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _productService.GetAllProductsAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            try
            {
                var result = await _productService.GetProductByIdAsync(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Post(ProductDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existing = await _productService.GetProductByNameAsync(model.ProductName);
                    if (existing != null)
                    {
                        return BadRequest("The product is already exists!");
                    }

                    var location = _linkGenerator.GetPathByAction(HttpContext,
                        "Get", "Products",
                        new { name = model.ProductName });

                    if (string.IsNullOrWhiteSpace(location))
                    {
                        return BadRequest("Could not use current product name");
                    }

                    var product = _mapper.Map<Product>(model);

                    if (await _productService.CreateProduct(product))
                    {
                        return Created(location, product);
                    }

                    return BadRequest("Something bad happened!");
                }
                else
                {
                    return BadRequest(ModelState);
                }

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var oldProduct = await _productService.GetProductByIdAsync(id);
                if (oldProduct == null) return NotFound();

                if (await _productService.DeleteProduct(oldProduct))
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e}");
            }

            return BadRequest("Failed to delete the product");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> Put(int id, ProductDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Model is not valid");
                }

                var oldProduct = await _productService.GetProductByIdAsync(id);
                if (oldProduct == null) return NotFound($"Could not find product with id of {id}");

                var updateProduct = _mapper.Map(model, oldProduct);

                if (await _productService.UpdateProduct(updateProduct))
                {
                    return Ok(updateProduct);
                }

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e}");
            }

            return BadRequest();
        }
    }
}


