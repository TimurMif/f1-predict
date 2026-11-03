using F1Predict.Domain.Models;
namespace F1Predict.Context;

/// <summary>
/// Контекст приложения
/// </summary>
public class F1PredictContext
{
    /// <summary>
    /// Список пользователей
    /// </summary>
    public List<User> Users { get; set; } = [];
    
    /// <summary>
    /// Список команд Формулы 1
    /// </summary>
    public List<Constructor> Constructors { get; set; } = [];
    
    /// <summary>
    /// Список пилотов
    /// </summary>
    public List<Driver> Drivers { get; set; } = [];

    /// <summary>
    /// Список этапов
    /// </summary>
    public List<GrandPrix> GrandPrix { get; set; } = [];
    
    /// <summary>
    /// Список сессий
    /// </summary>
    public List<Session> Sessions { get; set; } = [];
    
    /// <summary>
    /// Список с результатами каждого пилота
    /// </summary>
    public List<DriverResult> DriverResults { get; set; } = [];
    
    /// <summary>
    /// Список прогнозов
    /// </summary>
    public List<Prediction> Predictions { get; set; } = [];
    
    /// <summary>
    /// Чемпионаты пользователей
    /// </summary>
    public List<UsersChampionship> UsersChampionships { get; set; } = [];
    
    /// <summary>
    /// Список данных пользователей в своих чемпионатах
    /// </summary>
    public List<UserInChampionship> UserInChampionships { get; set; } = [];
    
    /// <summary>
    /// Список титулов всех пользователей
    /// </summary>
    public List<Title> Titles { get; set; } = [];
}