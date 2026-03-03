using System.ComponentModel.DataAnnotations;

namespace home_control_api.Models;

public class User
{
    [Key]
    public int UserId { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
