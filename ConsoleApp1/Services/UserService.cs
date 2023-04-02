
using ConsoleApp1.Contexts;
using ConsoleApp1.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ConsoleApp1.Services;

internal class UserService
{
    private readonly DataContext _context = new DataContext();

    public async Task<UserEntity> CreateAsync(UserEntity userEntity)
    {
        var _userEntity = await GetAsync(x => x.Email == userEntity.Email);

        if (_userEntity == null)
        {
            _userEntity = userEntity;
            _context.Add(_userEntity);
            await _context.SaveChangesAsync();
            return userEntity;
        }

        return _userEntity;
    }

  

    public async Task<UserEntity> GetAsync(Expression<Func<UserEntity, bool>> predicate)
    {
        var _userEntity = await _context.Users.FirstOrDefaultAsync(predicate);
        return _userEntity!;
    }
    public async Task<IEnumerable<UserEntity>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }
}