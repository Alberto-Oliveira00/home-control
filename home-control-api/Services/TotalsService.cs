using home_control_api.DTOs;
using home_control_api.Interfaces;
using home_control_api.Models.Enums;

namespace home_control_api.Services;

public class TotalsService : ITotalsService
{
    private readonly IUserRepository _userRepository;

    public TotalsService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    // Gera relatório com saldos individuais de cada pessoa e o total geral do sistema.
    public async Task<TotalsReportDto<PersonTotalDto>> GetTotalsPerPersonAsync()
    {
        // Retorna todos os usuários com suas transações
        var users = await _userRepository.GetAllAsync();

        // Processa a lista de usuários para calcular os totais individuais
        var personTotals = users.Select(u => new PersonTotalDto
        {
            UserId = u.UserId,
            Name = u.Name,
            // Filtra e soma apenas as transações que representam a receita
            TotalIncomes = u.Transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Value),
            // Filtra e soma apenas as transações que representam a despesa
            TotalExpenses = u.Transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Value)
        }).ToList();

        // Retorna o relatório final, calculando os totais gerais.
        return new TotalsReportDto<PersonTotalDto>
        {
            Items = personTotals,
            GeneralTotalIncomes = personTotals.Sum(p => p.TotalIncomes),
            GeneralTotalExpenses = personTotals.Sum(p => p.TotalExpenses)
        };
    }
}