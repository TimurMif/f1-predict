using F1Predict.Contracts.Dto;

namespace F1Predict.Contracts.ServiceInterfaces;

/// <summary>
/// Интерфейс сервиса для результатов пилотов
/// </summary>
public interface IDriverResultService
{
    /// <summary>
    /// Возвращает список всех результатов пилота
    /// </summary>
    public Task<List<DriverResultDto>> GetAllAsync();

    /// <summary>
    /// Возвращает результат пилота по его Id
    /// </summary>
    public Task<DriverResultDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создает новый результат пилота
    /// </summary>
    public Task<DriverResultDto> CreateAsync(DriverResultEditDto dto);

    /// <summary>
    /// Обновляет данные существующего результата пилота
    /// </summary>
    public Task<bool> UpdateAsync(int id, DriverResultEditDto dto);

    /// <summary>
    /// Удаляет результат пилота по его Id
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}