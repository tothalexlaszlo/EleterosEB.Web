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
    public class CategoriesController: ControllerBase
    {
        private readonly CategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public CategoriesController(CategoryService categoryService, IMapper mapper,
            LinkGenerator linkGenerator)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _categoryService.GetAllCategoriesAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            try
            {
                var result = await _categoryService.GetCategoryByIdAsync(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Post(Category model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existing = await _categoryService.GetCategoryByNameAsync(model.CategoryName);
                    if (existing != null)
                    {
                        return BadRequest("The category is already exists!");
                    }

                    var location = _linkGenerator.GetPathByAction(HttpContext,
                        "Get", "Categories",
                        new { name = model.CategoryName });

                    if (string.IsNullOrWhiteSpace(location))
                    {
                        return BadRequest("Could not use current category name");
                    }

                    var category = _mapper.Map<Category>(model);

                    if (await _categoryService.CreateCategory(category))
                    {
                        return Created(location, category);
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
                var oldCategory = await _categoryService.GetCategoryByIdAsync(id);
                if (oldCategory == null) return NotFound();

                if (await _categoryService.DeleteCategory(oldCategory))
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e}");
            }

            return BadRequest("Failed to delete the category");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Category>> Put(int id, CategoryDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Model is not valid");
                }

                var oldCategory = await _categoryService.GetCategoryByIdAsync(id);
                if (oldCategory == null) return NotFound($"Could not find category with id of {id}");

                var updateCategory = _mapper.Map(model, oldCategory);

                if (await _categoryService.UpdateCategory(updateCategory))
                {
                    return Ok(updateCategory);
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
