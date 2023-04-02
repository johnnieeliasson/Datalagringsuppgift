
using ConsoleApp1.Contexts;
using ConsoleApp1.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ConsoleApp1.Services;
internal class StatusService
{

    private readonly DataContext _context = new();

    public async Task InitializeAsync()
    {
        if (!await _context.Statuses.AnyAsync())
        {
            var list = new List<StatusEntity>()
            {
                new StatusEntity() { StatusName = "Ej påbörjad" },
                new StatusEntity() { StatusName = "Pågående" },
                new StatusEntity() { StatusName = "Avslutad" },
            };

            _context.AddRange(list);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<StatusEntity>> GetAllAsync()
    {
        return await _context.Statuses.ToListAsync();
    }

    public async Task<StatusEntity> GetAsync(Expression<Func<StatusEntity, bool>> predicate)
    {
        var _statusEntity = await _context.Statuses.FirstOrDefaultAsync(predicate);
        return _statusEntity!;
    }
}