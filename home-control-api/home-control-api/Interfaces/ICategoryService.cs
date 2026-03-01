using home_control_api.DTOs;

namespace home_control_api.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponseDto>> GetAllAsync();
    Task<CategoryResponseDto> CreateAsync(CreateCategoryDto dto);
}
