using F1Predict.Application.Exceptions;
using F1Predict.Application.Interfaces;
using F1Predict.Application.Mapping;
using F1Predict.Contracts.Dto;
using F1Predict.Contracts.ServiceInterfaces;
using F1Predict.Domain.Models;

namespace F1Predict.Application.Services;

/// <summary>
/// Сервис для работы с конструкторами
/// </summary>
public class ConstructorService(
    IRepository<Constructor> constructorRepository,
    IRepository<Driver> driverRepository
    ) : IConstructorService
{
    /// <summary>
    /// Возвращает всех конструкторов
    /// </summary>
    public async Task<List<ConstructorDto>> GetAllAsync()
    {
        var constructors = await constructorRepository.GetAllAsync();
        return constructors.Select(c => c.ToDto()).ToList();
    }
    
    /// <summary>
    /// Возвращает конструктора по его Id
    /// </summary>
    public async Task<ConstructorDto?> GetByIdAsync(int id)
    {
        var constructor = await constructorRepository.GetByIdAsync(id);
        return constructor?.ToDto();
    }

    /// <summary>
    /// Создает нового конструктора
    /// </summary>
    public async Task<ConstructorDto> CreateAsync(ConstructorEditDto dto)
    {
        var created = await constructorRepository.AddAsync(dto.ToEntity());
        return created.ToDto();
    }

    /// <summary>
    /// Обновляет данные существующего конструктора
    /// </summary>
    public async Task<bool> UpdateAsync(int id, ConstructorEditDto dto)
    {
        var constructor = dto.ToEntity();
        constructor.Id = id;

        return await constructorRepository.UpdateAsync(constructor);
    }

    /// <summary>
    /// Удаляет конструктора по его Id
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        var constructor = await constructorRepository.GetByIdAsync(id);
        if (constructor is null) return false;

        var drivers = await driverRepository.GetAllAsync();
        if (drivers.Any(d => d.ConstructorId == id))
            throw new ConflictException("Нельзя удалить команду, у которой есть пилоты");

        return await constructorRepository.DeleteAsync(id);
    }
}