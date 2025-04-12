using Bteste.Data;
using Bteste.Models;
using BTeste.Extensions;
using BTeste.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Bteste.Controllers{
[ApiController]
public class CategoryController:ControllerBase{
    [HttpGet("v1/categories")]
    public async Task<IActionResult> GetAsync(
        [FromServices] BlogDataContext context){
        
        var categories = await context.Categories.ToListAsync();
        return Ok(new ResultViewModel<List<Category>>(categories));
    }

    [HttpGet("v1/categories/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] int id,
        [FromServices] BlogDataContext context){
        
        try{
            var category = await context.Categories.FirstOrDefaultAsync(x=>x.Id == id);
            if(category == null){
                return NotFound(new ResultViewModel<Category>("Category not found"));
            }

            return Ok(category);
        }
        catch (Exception ex){
            return StatusCode(500, new ResultViewModel<Category>("Internal error"));
        }
    }

    [HttpPost("v1/categories")]
    public async Task<IActionResult> PostAsync(
        [FromBody]EditorCategoryViewModel model,
        [FromServices] BlogDataContext context){

            if(!ModelState.IsValid){
                return BadRequest(ModelState.GetErrors());
            }
        
        try{

            var category = new Category
            {
                Id = 0,
                Posts = [],
                Name = model.Name,
                Slug = model.Slug
            };

            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            return Created($"v1/categories/{category.Id}", model);
        }
        catch (DbUpdateException ex){
            return StatusCode(500,"Cannot was possible to include value");
        }
        catch (Exception ex){
            return StatusCode( 500);
        }
    }

    [HttpPut("v1/categories/{id:int}")]
    public async Task<IActionResult> PutAsync(
        [FromRoute] int id,
        [FromBody] EditorCategoryViewModel model,
        [FromServices] BlogDataContext context){
        
        var category = await context.Categories.FirstOrDefaultAsync(x=>x.Id == id);
        if(category == null){
            return NotFound();
        }

        category.Name = model.Name;
        category.Slug = model.Slug;

        context.Categories.Update(category);
        await context.SaveChangesAsync();
        return Ok(model);
    }

    [HttpDelete("v1/categories/{id:int}")]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] int id,
        [FromServices] BlogDataContext context){
        
        var category = await context.Categories.FirstOrDefaultAsync(x=>x.Id == id);
        if(category == null){
            return NotFound();
        }

        context.Categories.Remove(category);
        await context.SaveChangesAsync();
        return Ok(category);
    }

}
}