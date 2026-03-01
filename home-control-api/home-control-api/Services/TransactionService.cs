using home_control_api.DTOs;
using home_control_api.Interfaces;
using home_control_api.Models;
using home_control_api.Models.Enums;
using static home_control_api.DTOs.TransactionDTO;

namespace home_control_api.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICategoryRepository _categoryRepository;

    public TransactionService(ITransactionRepository transactionRepository, IUserRepository userRepository, ICategoryRepository categoryRepository)
    {
        _transactionRepository = transactionRepository;
        _userRepository = userRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<TransactionResponseDto>> GetAllAsync()
    {
        var transactions = await _transactionRepository.GetAllAsync();
        return transactions.Select(t => new TransactionResponseDto
        {
            TransactionId = t.TransactionId,
            Description = t.Description,
            Value = t.Value,
            Type = t.Type,
            CategoryId = t.CategoryId,
            UserId = t.UserId
        });
    }

    public async Task<TransactionResponseDto> CreateTransactionAsync(CreateTransactionDto dto)
    {
        var user = await _userRepository.GetByIdAsync(dto.UserId);
        if (user == null) throw new ArgumentException("Usuário não encontrado.");

        var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);
        if (category == null) throw new ArgumentException("Categoria não encontrada.");

        // Regras de negócio
        if (user.Age < 18 && dto.Type != TransactionType.Expense)
            throw new InvalidOperationException("Menores de 18 anos só podem registrar transações do tipo Despesa.");

        if (dto.Type == TransactionType.Expense && category.Type == CategoryType.Income)
            throw new InvalidOperationException("Uma despesa não pode ser vinculada a uma categoria exclusiva de receita.");

        if (dto.Type == TransactionType.Income && category.Type == CategoryType.Expense)
            throw new InvalidOperationException("Uma receita não pode ser vinculada a uma categoria exclusiva de despesa.");

        var transaction = new Transaction
        {
            Description = dto.Description,
            Value = dto.Value,
            Type = dto.Type,
            CategoryId = dto.CategoryId,
            UserId = dto.UserId
        };

        var created = await _transactionRepository.CreateAsync(transaction);

        return new TransactionResponseDto
        {
            TransactionId = created.TransactionId,
            Description = created.Description,
            Value = created.Value,
            Type = created.Type,
            CategoryId = created.CategoryId,
            UserId = created.UserId
        };
    }
}