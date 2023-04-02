
using ConsoleApp1.Contexts;
using ConsoleApp1.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ConsoleApp1.Services;

internal class CaseService
{
    private readonly DataContext _context = new();
    private readonly UserService _userService = new();
    private readonly StatusService _statusService = new();


    public async Task CreateAsync(CaseEntity caseEntity)
    {
        if (await _userService.GetAsync(userEntity => userEntity.Id == caseEntity.UserId) != null && await _statusService.GetAsync(statusEntity => statusEntity.Id == caseEntity.StatusId) != null)
        {
            _context.Add(caseEntity);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<IEnumerable<CaseEntity>> GetAllActiveAsync()
    {
        return await _context.Cases
            .Include(x => x.User)
            .Include(x => x.Status)
            .Include(x => x.Comments)
            .Where(x => x.StatusId != 3)
            .OrderByDescending(x => x.Created)
            .ToListAsync();
    }


    public async Task<IEnumerable<CaseEntity>> GetAllAsync()
    {
        return await _context.Cases
            .Include(x => x.User)
            .Include(x => x.Status)
            .Include(x => x.Comments)
            .OrderByDescending(x => x.Created)
            .ToListAsync();
    }

    public async Task<CaseEntity> GetAsync(string caseId)
    {
        var caseEntity = await _context.Cases
            .Include(x => x.User)
            .Include(x => x.Status)
            .Include(x => x.Comments)
            .FirstOrDefaultAsync(x => x.CaseId == caseId);

            return caseEntity;
    }

    public async Task<CaseEntity> UpdateCaseInformationAsync(Expression<Func<CaseEntity, bool>> predicate, CaseEntity updatedCaseEntity)
    {
        var _caseEntity = await _context.Cases.FirstOrDefaultAsync(predicate);
        if (_caseEntity != null)
        {
            _caseEntity.Description = updatedCaseEntity.Description;
            _caseEntity.UserId = (await _userService.CreateAsync(updatedCaseEntity.User)).Id;
            _caseEntity.Updated = DateTime.Now;
        }

        return _caseEntity!;
    }

    public async Task<CaseEntity> UpdateCaseStatusAsync(Expression<Func<CaseEntity, bool>> predicate)
    {
        var _caseEntity = await _context.Cases.FirstOrDefaultAsync(predicate);
        if (_caseEntity != null)
        {
            switch (_caseEntity.StatusId)
            {
                case 1:
                    _caseEntity.StatusId = 2;
                    _caseEntity.Updated = DateTime.Now;
                    break;

                case 2:
                    _caseEntity.StatusId = 3;
                    _caseEntity.Updated = DateTime.Now;
                    break;

                case 3:
                    _caseEntity.StatusId = 2;
                    _caseEntity.Updated = DateTime.Now;
                    break;
            }

            _context.Update(_caseEntity);
            await _context.SaveChangesAsync();
        }

        return _caseEntity!;
    }

    internal Task GetSpecificCaseAsync(string? caseId)
    {
        throw new NotImplementedException();
    }
}