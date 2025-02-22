using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API;

public class UserRepository(DefaultDbContext _context)
{
    public async Task AddUserAsync(AppUser appUser)
    {
        _context.Users.Add(appUser);
        await _context.SaveChangesAsync();
    }

    public async Task<AppUser?> GetUserByIdAsync(int id)
    {
        return await _context.Users
            .Include(x => x.Photos)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<AppUser?> GetUserByUsernameAsync(string username)
    {
        return await _context.Users
            .Include(x => x.Photos)
            .SingleOrDefaultAsync(x => x.UserName == username);
    }

    public async Task<bool> CheckIfUsernameExistsAsync(string username)
    {
        return await _context.Users.AnyAsync(x => x.UserName == username);
    }

    public async Task<List<AppUser>> GetAllUsersAsync()
    {
        return await _context.Users.Include(x => x.Photos).ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
