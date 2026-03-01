using home_control_api.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace home_control_api.Models;

public class Transaction
{
    [Key]
    public int TransactionId { get; set; }

    [Required]
    [StringLength(400)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser positivo.")]
    public int Value { get; set; }

    [Required]
    public TransactionType Type { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;

}
