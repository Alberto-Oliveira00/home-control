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

    // Injeta os três repositórios para validar Usuário e Categoria
    public TransactionService(ITransactionRepository transactionRepository, IUserRepository userRepository, ICategoryRepository categoryRepository)
    {
        _transactionRepository = transactionRepository;
        _userRepository = userRepository;
        _categoryRepository = categoryRepository;
    }

    // Retorna todas as transações.
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

    // Cria uma nova transação aplicando as regras de negócio de idade e categoria.
    public async Task<TransactionResponseDto> CreateTransactionAsync(CreateTransactionDto dto)
    {
        // Validações de usuário
        var user = await _userRepository.GetByIdAsync(dto.UserId);
        if (user == null) throw new ArgumentException("Usuário não encontrado.");

        // Validações de usuário
        var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);
        if (category == null) throw new ArgumentException("Categoria não encontrada.");

        // Regras de negócio 
        // Impede que pessoas com menos de 18 anos registrem receitas
        if (user.Age < 18 && dto.Type != TransactionType.Expense)
            throw new InvalidOperationException("Menores de 18 anos só podem registrar transações do tipo Despesa.");

        // Garante consistência entre o tipo da transação e a finalidade da categoria
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