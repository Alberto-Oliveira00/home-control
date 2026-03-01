using static home_control_api.DTOs.TransactionDTO;

namespace home_control_api.Interfaces;

public interface ITransactionService
{
    Task<IEnumerable<TransactionResponseDto>> GetAllAsync();
    Task<TransactionResponseDto> CreateTransactionAsync(CreateTransactionDto dto);
}
