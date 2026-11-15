using F1Predict.Application.Interfaces;
using F1Predict.Application.Mapping;
using F1Predict.Contracts.Dto;
using F1Predict.Contracts.ServiceInterfaces;
using F1Predict.Domain.Models;

namespace F1Predict.Application.Services;

/// <summary>
/// Сервис для работы с пилотами
/// </summary>
public class DriverService(
    IRepository<Driver> driverRepository, 
    IRepository<Constructor> constructorRepository) : IDriverService
{
    /// <summary>
    /// Возвращает всех пилотов
    /// </summary>
    public async Task<List<DriverDto>> GetAllAsync()
    {
        var drivers = await driverRepository.GetAllAsync();
        return drivers.Select(d => d.ToDto()).ToList();
    }

    /// <summary>
    /// Возвращает пилота по его Id
    /// </summary>
    public async Task<DriverDto?> GetByIdAsync(int id)
    {
        var driver = await driverRepository.GetByIdAsync(id);
        return driver?.ToDto();
    }

    /// <summary>
    /// Создает нового пилота
    /// </summary>
    public async Task<DriverDto?> CreateAsync(DriverEditDto dto)
    {
        var constructor = await constructorRepository.GetByIdAsync(dto.ConstructorId);
        if (constructor is null) return null;

        await EnsureNumberUnique(dto.Number, excludeId: null);

        var created = await driverRepository.AddAsync(dto.ToEntity(constructor));
        return created.ToDto();
    }
    
    /// <summary>
    /// Обновляет данные существующего пилота
    /// </summary>
    public async Task<bool> UpdateAsync(int id, DriverEditDto dto)
    {
        var driver = await driverRepository.GetByIdAsync(id);
        if (driver is null) return false;

        var constructor = await constructorRepository.GetByIdAsync(dto.ConstructorId);
        if (constructor is null) return false;

        await EnsureNumberUnique(dto.Number, excludeId: id);

        driver.FirstName = dto.FirstName;
        driver.LastName = dto.LastName;
        driver.Number = dto.Number;
        driver.Nationality = dto.Nationality;
        driver.Points = dto.Points;
        driver.ConstructorId = constructor.Id;   // переход в другую команду — допустим
        driver.Constructor = constructor;

        return await driverRepository.UpdateAsync(driver);
    }

    /// <summary>
    /// Удаляет пилота по его Id
    /// </summary>
    public Task<bool> DeleteAsync(int id)
    {
        return driverRepository.DeleteAsync(id);
    }
    
    private async Task EnsureNumberUnique(string number, int? excludeId)
    {
        var all = await driverRepository.GetAllAsync();
        if (all.Any(d => d.Number == number && d.Id != excludeId))
            throw new InvalidOperationException("Пилот с таким номером уже существует");
    }
}