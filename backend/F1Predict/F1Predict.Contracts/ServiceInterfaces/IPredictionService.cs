using F1Predict.Contracts.Dto;

namespace F1Predict.Contracts.ServiceInterfaces;

/// <summary>
/// Интерфейс сервиса для прогнозов
/// </summary>
public interface IPredictionService
{
    /// <summary>
    /// Возвращает список всех прогнозов
    /// </summary>
    public Task<List<PredictionDto>> GetAllAsync();

    /// <summary>
    /// Возвращает прогноз по его Id
    /// </summary>
    public Task<PredictionDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создает новый прогноз
    /// </summary>
    public Task<PredictionDto?> CreateAsync(int userId, PredictionEditDto dto);

    /// <summary>
    /// Обновляет данные существующего прогноза
    /// </summary>
    public Task<bool> UpdateAsync(int id, int userId, PredictionEditDto dto);

    /// <summary>
    /// Удаляет прогноз по его Id
    /// </summary>
    public Task<bool> DeleteAsync(int id, int userId);
}