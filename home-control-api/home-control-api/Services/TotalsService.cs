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

    public async Task<TotalsReportDto<PersonTotalDto>> GetTotalsPerPersonAsync()
    {
        var users = await _userRepository.GetAllAsync();

        var personTotals = users.Select(u => new PersonTotalDto
        {
            UserId = u.UserId,
            Name = u.Name,
            TotalIncomes = u.Transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Value),
            TotalExpenses = u.Transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Value)
        }).ToList();

        return new TotalsReportDto<PersonTotalDto>
        {
            Items = personTotals,
            GeneralTotalIncomes = personTotals.Sum(p => p.TotalIncomes),
            GeneralTotalExpenses = personTotals.Sum(p => p.TotalExpenses)
        };
    }
}