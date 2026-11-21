using F1Predict.Contracts.Dto;

namespace F1Predict.Contracts.ServiceInterfaces;

/// <summary>
/// Интерфейс сервиса для прогнозов
/// </summary>
public interface IPredictionService
{
    /// <summary>
    /// Возвращает прогнозы (новые сверху), доступные текущему пользователю;
    /// можно отфильтровать по пользователю и по сессии
    /// </summary>
    public Task<List<PredictionDto>> GetAllAsync(int? userId, int? sessionId, int currentUserId);

    /// <summary>
    /// Возвращает прогноз по его Id, если он доступен текущему пользователю
    /// </summary>
    public Task<PredictionDto?> GetByIdAsync(int id, int currentUserId);

    /// <summary>
    /// Создает новый прогноз от имени пользователя
    /// </summary>
    public Task<PredictionDto> CreateAsync(int userId, PredictionEditDto dto);

    /// <summary>
    /// Обновляет свой прогноз, пока приём прогнозов на сессию открыт
    /// </summary>
    public Task<bool> UpdateAsync(int id, int userId, PredictionEditDto dto);

    /// <summary>
    /// Удаляет прогноз по его Id, пока приём прогнозов на сессию открыт
    /// </summary>
    public Task<bool> DeleteAsync(int id, int userId);
}