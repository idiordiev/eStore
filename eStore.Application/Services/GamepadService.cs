using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStore.Application.Filtering.Models;
using eStore.Application.Interfaces.Data;
using eStore.Application.Interfaces.Services;
using eStore.Domain.Entities;

namespace eStore.Application.Services;

public class GamepadService : IGamepadService
{
    private readonly IUnitOfWork _unitOfWork;

    public GamepadService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Gamepad>> GetPresentAsync()
    {
        return await Task.FromResult(_unitOfWork.GamepadRepository.Query(g => !g.IsDeleted));
    }

    public async Task<IEnumerable<Gamepad>> GetPresentByFilterAsync(GamepadFilterModel filter)
    {
        var queryExpression = filter.GetPredicate();
        return await Task.FromResult(_unitOfWork.GamepadRepository.Query(queryExpression));
    }

    public async Task<Gamepad> GetByIdAsync(int gamepadId)
    {
        return await _unitOfWork.GamepadRepository.GetByIdAsync(gamepadId);
    }

    public async Task<IEnumerable<string>> GetManufacturersAsync()
    {
        var gamepads = await _unitOfWork.GamepadRepository.GetAllAsync();
        return gamepads.Select(g => g.Manufacturer).Distinct();
    }

    public async Task<IEnumerable<string>> GetFeedbacksAsync()
    {
        var gamepads = await _unitOfWork.GamepadRepository.GetAllAsync();
        return gamepads.Select(g => g.Feedback).Distinct();
    }

    public async Task<IEnumerable<string>> GetConnectionTypesAsync()
    {
        var gamepads = await _unitOfWork.GamepadRepository.GetAllAsync();
        return gamepads.Select(g => g.ConnectionType).Distinct();
    }

    public async Task<IEnumerable<string>> GetCompatibleDevicesAsync()
    {
        var gamepads = await _unitOfWork.GamepadRepository.GetAllAsync();
        return gamepads.SelectMany(g => g.CompatibleDevices).Distinct();
    }
}