using home_control_api.DTOs;
using home_control_api.Interfaces;
using home_control_api.Models;

namespace home_control_api.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<CategoryResponseDto>> GetAllAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return categories.Select(c => new CategoryResponseDto
        {
            CategoryId = c.CategoryId,
            Description = c.Description,
            Type = c.Type
        });
    }

    public async Task<CategoryResponseDto> CreateAsync(CreateCategoryDto dto)
    {
        var category = new Category { Description = dto.Description, Type = dto.Type };
        var createdCategory = await _categoryRepository.CreateAsync(category);

        return new CategoryResponseDto { CategoryId = createdCategory.CategoryId, Description = createdCategory.Description, Type = createdCategory.Type };
    }
}