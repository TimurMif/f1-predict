using F1Predict.Contracts.Dto;

namespace F1Predict.Contracts.ServiceInterfaces;

/// <summary>
/// Интерфейс сервиса для сессий
/// </summary>
public interface ISessionService
{
    /// <summary>
    /// Возвращает список сессий по порядку начала; если указан grandPrixId, только сессии этого этапа
    /// </summary>
    public Task<List<SessionDto>> GetAllAsync(int? grandPrixId = null);
    
    /// <summary>
    /// Возвращает сессию по ее Id
    /// </summary>
    public Task<SessionDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создает новую сессию
    /// </summary>
    public Task<SessionDto> CreateAsync(SessionEditDto dto);

    /// <summary>
    /// Обновляет данные существующей сессии
    /// </summary>
    public Task<bool> UpdateAsync(int id, SessionEditDto dto);

    /// <summary>
    /// Удаляет сессию по ее Id
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}