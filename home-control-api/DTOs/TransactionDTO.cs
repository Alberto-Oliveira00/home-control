using home_control_api.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace home_control_api.DTOs;

public class TransactionDTO
{
    public class CreateTransactionDto
    {
        [Required]
        [StringLength(400)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser positivo.")]
        public decimal Value { get; set; }

        [Required]
        public TransactionType Type { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int UserId { get; set; }
    }

    public class TransactionResponseDto
    {
        public int TransactionId { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public TransactionType Type { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
    }
}
