using home_control_api.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace home_control_api.Models;

public class Category
{
    [Key]
    public int CategoryId { get; set; }

    [Required]
    [StringLength(400)]
    public string Description { get; set; } = string.Empty;
    [Required]
    public CategoryType Type { get; set; }
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
