using home_control_api.DTOs;
using home_control_api.Interfaces;
using home_control_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace home_control_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>
    /// Retorna todas as categorias.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();
        return Ok(categories);
    }

    /// <summary>
    /// Cria uma nova categoria.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
    {
        var createdCategory = await _categoryService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetAll), new { id = createdCategory.CategoryId }, createdCategory);
    }
}