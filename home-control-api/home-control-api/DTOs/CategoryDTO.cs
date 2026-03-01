using home_control_api.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace home_control_api.DTOs;

public class CreateCategoryDto
{
    [Required]
    [StringLength(400)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public CategoryType Type { get; set; }
}

public class CategoryResponseDto
{
    public int CategoryId { get; set; }
    public string Description { get; set; } = string.Empty;
    public CategoryType Type { get; set; }
}
