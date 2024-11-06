using Core.Domain.Models;
using Core.Domain.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryAndServices.Service.CustomService.CategoryService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet("GetAllCategories")]

        public async Task<IActionResult> GetAllCategories()
        {
            var category = await categoryService.GetAllAsync();
            if (category != null)
            {
                return Ok(category);
            }
            else
            {
                return BadRequest("Unable to retrieve category");
            }
        }

        [HttpGet("GetCategoryById")]

        public async Task<IActionResult> GetCategoryById(int id)
        {
            var categoryid = await categoryService.GetByIDAsync(id);
            if (categoryid != null)
            {
                return Ok(categoryid);
            }
            else
            {
                return BadRequest("Unable to retrieve the requested category");
            }
        }

        [HttpPost("AddCategory")]

        public async Task<IActionResult> AddCategory([FromBody] CategoryInsertModel categoryInsertModel)
        {
            if (ModelState.IsValid)
            {
                var success = await categoryService.InsertAsync(categoryInsertModel);

                if (success)
                {
                    return Ok("Category created successfully.");
                }
                else
                {
                    return BadRequest("Failed to create category.");
                }
            }
            return BadRequest("Invalid model state.");
        }

        [HttpPut("UpdateCategory")]

        public async Task<IActionResult> UpdateCategory([FromBody] CategoryUpdateModel categoryUpdateModel)
        {
            if (ModelState.IsValid)
            {
                var existingCategory = await categoryService.GetByIDAsync(categoryUpdateModel.Id);

                if (existingCategory != null)
                {
                    var success = await categoryService.UpdateAsync(categoryUpdateModel);

                    if (success)
                    {
                        return Ok("Category updated successfully.");
                    }
                    else
                    {
                        return BadRequest("Failed to update category.");
                    }
                }
                return BadRequest("Category not found.");
            }
            return BadRequest("Invalid model state.");
        }

        [HttpDelete("DeleteCategory")]

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var existingCategory = await categoryService.GetByIDAsync(id);

            if (existingCategory != null)
            {
                var success = await categoryService.DeleteAsync(id);

                if (success)
                {
                    return Ok("Category deleted successfully.");
                }
                else
                {
                    return BadRequest("Failed to delete category.");
                }
            }
            return BadRequest("Category not found.");
        }

    }
}
