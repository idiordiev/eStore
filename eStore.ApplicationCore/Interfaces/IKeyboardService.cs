﻿using System.Collections.Generic;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.FilterModels;

namespace eStore.ApplicationCore.Interfaces
{
    public interface IKeyboardService
    {
        Task<IEnumerable<Keyboard>> GetAllAsync();
        Task<IEnumerable<Keyboard>> GetAllByFilterAsync(KeyboardFilterModel filter);
        Task<Keyboard> GetByIdAsync(int keyboardId);
        Task<IEnumerable<Manufacturer>> GetManufacturersAsync();
        Task<IEnumerable<KeyboardSwitch>> GetSwitchesAsync();
        Task<IEnumerable<KeyboardSize>> GetSizesAsync();
        Task<IEnumerable<KeyboardType>> GetTypesAsync();
    }
}