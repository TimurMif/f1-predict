using F1Predict.Application.Interfaces;
using F1Predict.Application.Mapping;
using F1Predict.Contracts.Dto;
using F1Predict.Contracts.ServiceInterfaces;
using F1Predict.Domain.Models;

namespace F1Predict.Application.Services;

/// <summary>
/// Сервис для работы с гран при
/// </summary>
public class GrandPrixService(IRepository<GrandPrix> grandPrixRepository) : IGrandPrixService
{
    /// <summary>
    /// Возвращает все гран при
    /// </summary>
    public async Task<List<GrandPrixDto>> GetAllAsync()
    {
        var grandPrixs = await grandPrixRepository.GetAllAsync();
        return grandPrixs.Select(g => g.ToDto()).ToList();
    }
    
    /// <summary>
    /// Возвращает гран при по его Id
    /// </summary>
    public async Task<GrandPrixDto?> GetByIdAsync(int id)
    {
        var grandPrix = await grandPrixRepository.GetByIdAsync(id);
        return grandPrix?.ToDto();
    }

    /// <summary>
    /// Создает новое гран при
    /// </summary>
    public async Task<GrandPrixDto> CreateAsync(GrandPrixEditDto dto)
    {
        await EnsureRoundUnique(dto.Year, dto.RoundNumber, excludeId: null);
        var created = await grandPrixRepository.AddAsync(dto.ToEntity());
        return created.ToDto();
    }

    /// <summary>
    /// Обновляет данные существующего гран при
    /// </summary>
    public async Task<bool> UpdateAsync(int id, GrandPrixEditDto dto)
    {
        var gp = await grandPrixRepository.GetByIdAsync(id);
        if (gp is null) return false;

        await EnsureRoundUnique(dto.Year, dto.RoundNumber, excludeId: id);

        gp.Name = dto.Name;
        gp.CircuitName = dto.CircuitName;
        gp.Country = dto.Country;
        gp.RoundNumber = dto.RoundNumber;
        gp.Year = dto.Year;
        return await grandPrixRepository.UpdateAsync(gp);
    }

    /// <summary>
    /// Удаляет гран при по его Id
    /// </summary>
    public Task<bool> DeleteAsync(int id)
    {
        return grandPrixRepository.DeleteAsync(id);
    }
    
    private async Task EnsureRoundUnique(int year, int round, int? excludeId)
    {
        var all = await grandPrixRepository.GetAllAsync();
        if (all.Any(g => g.Year == year && g.RoundNumber == round && g.Id != excludeId))
            throw new InvalidOperationException($"Этап №{round} в сезоне {year} уже существует");
    }
}