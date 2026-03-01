using home_control_api.Data;
using home_control_api.Interfaces;
using home_control_api.Models;
using Microsoft.EntityFrameworkCore;

namespace home_control_api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.AsNoTracking().ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        //Include trazendo as transações para calcular receitas e despesas depois
        return await _context.Users
            .Include(u => u.Transactions)
            .FirstOrDefaultAsync(u => u.UserId == id);
    }

    public async Task<User> CreateAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) 
            return false;
    
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return true;
    }
}
