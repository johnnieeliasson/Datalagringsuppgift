
using ConsoleApp1.Contexts;
using ConsoleApp1.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp.Services;

internal class CommentService
{
    private readonly DataContext _context = new();

    public async Task CreateAsync(CommentEntity commentEntity)
    {
        {
            _context.Add(commentEntity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<CommentEntity>> GetAllAsync()
    {
        return await _context.Comments.ToListAsync();
    }
}