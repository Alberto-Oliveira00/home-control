using Microsoft.EntityFrameworkCore;
using home_control_api.Data;
using home_control_api.Interfaces;
using home_control_api.Models;

namespace home_control_api.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Transaction> CreateAsync(Transaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }

    public async Task<IEnumerable<Transaction>> GetAllAsync()
    {
        return await _context.Transactions.AsNoTracking().ToListAsync();
    }
}
