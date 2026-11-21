using F1Predict.Application.Exceptions;
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
    IRepository<Constructor> constructorRepository,
    IRepository<DriverResult> driverResultRepository,
    IRepository<Prediction> predictionRepository) : IDriverService
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
    public async Task<DriverDto> CreateAsync(DriverEditDto dto)
    {
        var constructor = await GetConstructorOrThrow(dto.ConstructorId);

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

        var constructor = await GetConstructorOrThrow(dto.ConstructorId);

        await EnsureNumberUnique(dto.Number, excludeId: id);

        driver.FirstName = dto.FirstName;
        driver.LastName = dto.LastName;
        driver.Number = dto.Number;
        driver.Nationality = dto.Nationality;
        driver.Points = dto.Points;
        driver.ConstructorId = constructor.Id;
        driver.Constructor = constructor;

        return await driverRepository.UpdateAsync(driver);
    }

    /// <summary>
    /// Удаляет пилота по его Id
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        var driver = await driverRepository.GetByIdAsync(id);
        if (driver is null) return false;

        var results = await driverResultRepository.GetAllAsync();
        var predictions = await predictionRepository.GetAllAsync();

        var hasResults = results.Any(r => r.DriverId == id);
        var hasPredictions = predictions.Any(p =>
            p.DriverTopOneId == id || p.DriverTopTwoId == id || p.DriverTopThreeId == id);

        if (hasResults || hasPredictions)
            throw new ConflictException("Нельзя удалить пилота, у которого есть результаты или прогнозы");

        return await driverRepository.DeleteAsync(id);
    }
    
    private async Task<Constructor> GetConstructorOrThrow(int constructorId)
    {
        return await constructorRepository.GetByIdAsync(constructorId)
               ?? throw new NotFoundException($"Команда с Id={constructorId} не найдена");
    }
    
    private async Task EnsureNumberUnique(string number, int? excludeId)
    {
        var all = await driverRepository.GetAllAsync();
        if (all.Any(d => d.Number == number && d.Id != excludeId))
            throw new ConflictException("Пилот с таким номером уже существует");
    }
}