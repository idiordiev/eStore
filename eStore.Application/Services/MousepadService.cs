using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStore.Application.Filtering.Models;
using eStore.Application.Interfaces.Data;
using eStore.Application.Interfaces.Services;
using eStore.Domain.Entities;

namespace eStore.Application.Services;

public class MousepadService : IMousepadService
{
    private readonly IUnitOfWork _unitOfWork;

    public MousepadService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Mousepad>> GetPresentAsync()
    {
        return await Task.FromResult(_unitOfWork.MousepadRepository.Query(m => !m.IsDeleted));
    }

    public async Task<IEnumerable<Mousepad>> GetPresentByFilterAsync(MousepadFilterModel filter)
    {
        var queryExpression = filter.GetPredicate();
        return await Task.FromResult(_unitOfWork.MousepadRepository.Query(queryExpression));
    }

    public async Task<Mousepad> GetByIdAsync(int mousepadId)
    {
        return await _unitOfWork.MousepadRepository.GetByIdAsync(mousepadId);
    }

    public async Task<IEnumerable<string>> GetManufacturersAsync()
    {
        var mousepads = await _unitOfWork.MousepadRepository.GetAllAsync();
        return mousepads.Select(m => m.Manufacturer).Distinct().OrderBy(m => m);
    }

    public async Task<IEnumerable<string>> GetTopMaterialsAsync()
    {
        var mousepads = await _unitOfWork.MousepadRepository.GetAllAsync();
        return mousepads.Select(m => m.TopMaterial).Distinct().OrderBy(tm => tm);
    }

    public async Task<IEnumerable<string>> GetBottomMaterialsAsync()
    {
        var mousepads = await _unitOfWork.MousepadRepository.GetAllAsync();
        return mousepads.Select(m => m.BottomMaterial).Distinct().OrderBy(tm => tm);
    }

    public async Task<IEnumerable<string>> GetBacklightsAsync()
    {
        var mousepads = await _unitOfWork.MousepadRepository.GetAllAsync();
        return mousepads.Select(m => m.Backlight).Distinct().OrderBy(b => b);
    }
}