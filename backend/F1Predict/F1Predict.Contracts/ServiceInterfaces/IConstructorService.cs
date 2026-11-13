using F1Predict.Contracts.Dto;

namespace F1Predict.Contracts.ServiceInterfaces;

/// <summary>
/// Интерфейс сервиса для конструкторов
/// </summary>
public interface IConstructorService
{
    /// <summary>
    /// Возвращает список всех конструкторов
    /// </summary>
    public Task<List<ConstructorDto>> GetAllAsync();

    /// <summary>
    /// Возвращает конструктора по его Id
    /// </summary>
    public Task<ConstructorDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создает нового конструктора
    /// </summary>
    public Task<ConstructorDto> CreateAsync(ConstructorEditDto dto);

    /// <summary>
    /// Обновляет данные существующего конструктора
    /// </summary>
    public Task<bool> UpdateAsync(int id, ConstructorEditDto dto);

    /// <summary>
    /// Удаляет конструктора по его Id
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}