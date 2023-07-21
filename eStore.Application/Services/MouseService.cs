using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStore.Application.Filtering.Models;
using eStore.Application.Interfaces.Data;
using eStore.Application.Interfaces.Services;
using eStore.Domain.Entities;

namespace eStore.Application.Services;

public class MouseService : IMouseService
{
    private readonly IUnitOfWork _unitOfWork;

    public MouseService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Mouse>> GetPresentAsync()
    {
        return await Task.FromResult(_unitOfWork.MouseRepository.Query(m => !m.IsDeleted));
    }

    public async Task<IEnumerable<Mouse>> GetPresentByFilterAsync(MouseFilterModel filter)
    {
        var queryExpression = filter.GetPredicate();
        return await Task.FromResult(_unitOfWork.MouseRepository.Query(queryExpression));
    }

    public async Task<Mouse> GetByIdAsync(int mouseId)
    {
        return await _unitOfWork.MouseRepository.GetByIdAsync(mouseId);
    }

    public async Task<IEnumerable<string>> GetManufacturersAsync()
    {
        var mouses = await _unitOfWork.MouseRepository.GetAllAsync();
        return mouses.Select(m => m.Manufacturer).Distinct().OrderBy(m => m);
    }

    public async Task<IEnumerable<string>> GetConnectionTypesAsync()
    {
        var mouses = await _unitOfWork.MouseRepository.GetAllAsync();
        return mouses.Select(m => m.ConnectionType).Distinct().OrderBy(c => c);
    }

    public async Task<IEnumerable<string>> GetBacklightsAsync()
    {
        var mouses = await _unitOfWork.MouseRepository.GetAllAsync();
        return mouses.Select(m => m.Backlight).Distinct().OrderBy(b => b);
    }
}