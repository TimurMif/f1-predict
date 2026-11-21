using F1Predict.Contracts.Dto;

namespace F1Predict.Contracts.ServiceInterfaces;

/// <summary>
/// Интерфейс сервиса для пилотов
/// </summary>
public interface IDriverService
{
    /// <summary>
    /// Возвращает список всех пилотов
    /// </summary>
    public Task<List<DriverDto>> GetAllAsync();

    /// <summary>
    /// Возвращает пилота по его Id
    /// </summary>
    public Task<DriverDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создает нового пилота
    /// </summary>
    public Task<DriverDto> CreateAsync(DriverEditDto dto);

    /// <summary>
    /// Обновляет данные существующего пилота
    /// </summary>
    public Task<bool> UpdateAsync(int id, DriverEditDto dto);

    /// <summary>
    /// Удаляет пилота по его Id
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}