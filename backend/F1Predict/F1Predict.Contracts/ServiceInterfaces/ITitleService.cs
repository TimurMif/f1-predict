using F1Predict.Contracts.Dto;

namespace F1Predict.Contracts.ServiceInterfaces;

/// <summary>
/// Интерфейс сервиса для титулов пользователей
/// </summary>
public interface ITitleService
{
    /// <summary>
    /// Возвращает список всех титулов
    /// </summary>
    public Task<List<TitleDto>> GetAllAsync();

    /// <summary>
    /// Возвращает титула по его Id
    /// </summary>
    public Task<TitleDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создает нового титула
    /// </summary>
    public Task<TitleDto> CreateAsync(TitleEditDto dto);

    /// <summary>
    /// Обновляет данные существующего титула
    /// </summary>
    public Task<bool> UpdateAsync(int id, TitleEditDto dto);

    /// <summary>
    /// Удаляет титул по его Id
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}