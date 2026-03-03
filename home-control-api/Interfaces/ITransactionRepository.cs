using home_control_api.Models;

namespace home_control_api.Interfaces;

public interface ITransactionRepository
{
    Task<IEnumerable<Transaction>> GetAllAsync();
    Task<Transaction> CreateAsync(Transaction transaction);
}
