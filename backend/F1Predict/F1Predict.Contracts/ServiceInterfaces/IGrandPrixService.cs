using F1Predict.Contracts.Dto;

namespace F1Predict.Contracts.ServiceInterfaces;

/// <summary>
/// Интерфейс сервиса для гран при
/// </summary>
public interface IGrandPrixService
{
    /// <summary>
    /// Возвращает список всех гран при
    /// </summary>
    public Task<List<GrandPrixDto>> GetAllAsync();

    /// <summary>
    /// Возвращает гран при по его Id
    /// </summary>
    public Task<GrandPrixDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создает новое гран при
    /// </summary>
    public Task<GrandPrixDto> CreateAsync(GrandPrixEditDto dto);

    /// <summary>
    /// Обновляет данные существующего гран при
    /// </summary>
    public Task<bool> UpdateAsync(int id, GrandPrixEditDto dto);

    /// <summary>
    /// Удаляет гран при по его Id
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}