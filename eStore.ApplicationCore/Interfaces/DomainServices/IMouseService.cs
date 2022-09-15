﻿using System.Collections.Generic;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.FilterModels;

namespace eStore.ApplicationCore.Interfaces.DomainServices
{
    public interface IMouseService
    {
        Task<IEnumerable<Mouse>> GetPresentAsync();
        Task<IEnumerable<Mouse>> GetPresentByFilterAsync(MouseFilterModel filter);
        Task<Mouse> GetByIdAsync(int mouseId);
        Task<IEnumerable<Manufacturer>> GetManufacturersAsync();
        Task<IEnumerable<ConnectionType>> GetConnectionTypesAsync();
        Task<IEnumerable<Backlight>> GetBacklightsAsync();
    }
}