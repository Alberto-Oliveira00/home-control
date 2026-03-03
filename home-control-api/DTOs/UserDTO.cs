using System.ComponentModel.DataAnnotations;

namespace home_control_api.DTOs;

public class CreateUserDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Range(0, 150, ErrorMessage = "Idade inválida.")]
    public int Age { get; set; }
}

public class UpdateUserDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int Age { get; set; }
}

public class UserResponseDto
{
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
}
