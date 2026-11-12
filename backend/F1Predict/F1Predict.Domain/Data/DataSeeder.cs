using F1Predict.Domain.Models;

namespace F1Predict.Data;

/// <summary>
/// Статические seed-данные для EF Core HasData.
/// </summary>
public static class DataSeeder
{
    /// <summary>
    /// Фиксированная «текущая» дата для тестов и сценариев. Все сессии раньше неё.
    /// </summary>
    public static readonly DateTime ReferenceDateNow =
        new(2026, 4, 1, 12, 0, 0, DateTimeKind.Utc);

    public static readonly object[] Constructors =
    {
        new { Id = 1, Name = "Red Bull", Nationality = "Austria", Points = 143 },
        new { Id = 2, Name = "Mercedes", Nationality = "German", Points = 151 },
        new { Id = 3, Name = "Ferrari", Nationality = "Italy", Points = 120 },
        new { Id = 4, Name = "McLaren", Nationality = "Britain", Points = 174 },
        new { Id = 5, Name = "Williams", Nationality = "Britain", Points = 56 },
        new { Id = 6, Name = "Haas", Nationality = "USA", Points = 23 },
        new { Id = 7, Name = "Racing Bulls", Nationality = "Austria", Points = 41 },
        new { Id = 8, Name = "Audi", Nationality = "German", Points = 11 },
        new { Id = 9, Name = "Alpine", Nationality = "France", Points = 19 },
        new { Id = 10, Name = "Aston Martin", Nationality = "Britain", Points = 9 },
        new { Id = 11, Name = "Cadillac", Nationality = "USA", Points = 0 },
    };

    public static readonly object[] Drivers =
    {
        new { Id = 1, FirstName = "George", LastName = "Russell", Number = "63", Nationality = "Britain", Points = 65, ConstructorId = 2 },
        new { Id = 2, FirstName = "Kimi", LastName = "Antonelli", Number = "12", Nationality = "Italy", Points = 86, ConstructorId = 2 },
        new { Id = 3, FirstName = "Max", LastName = "Verstappen", Number = "3", Nationality = "Netherlands", Points = 98, ConstructorId = 1 },
        new { Id = 4, FirstName = "Isack", LastName = "Hadjar", Number = "6", Nationality = "France", Points = 45, ConstructorId = 1 },
        new { Id = 5, FirstName = "Lando", LastName = "Norris", Number = "1", Nationality = "Britain", Points = 91, ConstructorId = 4 },
        new { Id = 6, FirstName = "Oscar", LastName = "Piastri", Number = "81", Nationality = "Australia", Points = 83, ConstructorId = 4 },
        new { Id = 7, FirstName = "Charles", LastName = "Leclerc", Number = "16", Nationality = "Monaco", Points = 68, ConstructorId = 3 },
        new { Id = 8, FirstName = "Lewis", LastName = "Hamilton", Number = "44", Nationality = "Britain", Points = 52, ConstructorId = 3 },
        new { Id = 9, FirstName = "Alexander", LastName = "Albon", Number = "23", Nationality = "Thailand", Points = 24, ConstructorId = 5 },
        new { Id = 10, FirstName = "Carlos", LastName = "Sainz", Number = "55", Nationality = "Spain", Points = 32, ConstructorId = 5 },
        new { Id = 11, FirstName = "Liam", LastName = "Lawson", Number = "30", Nationality = "New Zealand", Points = 22, ConstructorId = 7 },
        new { Id = 12, FirstName = "Arvid", LastName = "Lindblad", Number = "41", Nationality = "Britain", Points = 19, ConstructorId = 7 },
        new { Id = 13, FirstName = "Esteban", LastName = "Ocon", Number = "31", Nationality = "France", Points = 9, ConstructorId = 6 },
        new { Id = 14, FirstName = "Oliver", LastName = "Bearman", Number = "87", Nationality = "Britain", Points = 14, ConstructorId = 6 },
        new { Id = 15, FirstName = "Nico", LastName = "Hulkenberg", Number = "27", Nationality = "Germany", Points = 4, ConstructorId = 8 },
        new { Id = 16, FirstName = "Gabriel", LastName = "Bortoleto", Number = "5", Nationality = "Brazil", Points = 7, ConstructorId = 8 },
        new { Id = 17, FirstName = "Pierre", LastName = "Gasly", Number = "10", Nationality = "France", Points = 12, ConstructorId = 9 },
        new { Id = 18, FirstName = "Franco", LastName = "Colapinto", Number = "43", Nationality = "Argentina", Points = 7, ConstructorId = 9 },
        new { Id = 19, FirstName = "Fernando", LastName = "Alonso", Number = "14", Nationality = "Spain", Points = 6, ConstructorId = 10 },
        new { Id = 20, FirstName = "Lance", LastName = "Stroll", Number = "18", Nationality = "Canada", Points = 3, ConstructorId = 10 },
        new { Id = 21, FirstName = "Sergio", LastName = "Perez", Number = "11", Nationality = "Mexico", Points = 0, ConstructorId = 11 },
        new { Id = 22, FirstName = "Valtteri", LastName = "Bottas", Number = "77", Nationality = "Finland", Points = 0, ConstructorId = 11 },
    };

    public static readonly object[] GrandPrix =
    {
        new { Id = 1, Name = "Australian Grand Prix", CircuitName = "Albert Park", Country = "Australia", RoundNumber = 1, Year = 2026 },
        new { Id = 2, Name = "Chinese Grand Prix", CircuitName = "Shanghai GP", Country = "China", RoundNumber = 2, Year = 2026 },
        new { Id = 3, Name = "Japanese Grand Prix", CircuitName = "Suzuka", Country = "Japan", RoundNumber = 3, Year = 2026 },
    };

    // Id сессий подряд: 1-2 Австралия, 3-6 Китай (спринт-уикенд), 7-8 Япония.
    public static readonly object[] Sessions =
    {
        new { Id = 1, GrandPrixId = 1, Name = "Qualifying", Type = SessionType.Qualifying, DateStart = Utc(3, 7, 5), DateEnd = Utc(3, 7, 6), Status = SessionStatus.Completed },
        new { Id = 2, GrandPrixId = 1, Name = "Race", Type = SessionType.Race, DateStart = Utc(3, 8, 4), DateEnd = Utc(3, 8, 6), Status = SessionStatus.Completed },
        new { Id = 3, GrandPrixId = 2, Name = "Sprint Qualifying", Type = SessionType.SprintQualifying, DateStart = Utc(3, 13, 7, 30), DateEnd = Utc(3, 13, 8, 15), Status = SessionStatus.Completed },
        new { Id = 4, GrandPrixId = 2, Name = "Sprint", Type = SessionType.Sprint, DateStart = Utc(3, 14, 3), DateEnd = Utc(3, 14, 4), Status = SessionStatus.Completed },
        new { Id = 5, GrandPrixId = 2, Name = "Qualifying", Type = SessionType.Qualifying, DateStart = Utc(3, 14, 7), DateEnd = Utc(3, 14, 8), Status = SessionStatus.Completed },
        new { Id = 6, GrandPrixId = 2, Name = "Race", Type = SessionType.Race, DateStart = Utc(3, 15, 7), DateEnd = Utc(3, 15, 9), Status = SessionStatus.Completed },
        new { Id = 7, GrandPrixId = 3, Name = "Qualifying", Type = SessionType.Qualifying, DateStart = Utc(3, 28, 6), DateEnd = Utc(3, 28, 7), Status = SessionStatus.Completed },
        new { Id = 8, GrandPrixId = 3, Name = "Race", Type = SessionType.Race, DateStart = Utc(3, 29, 5), DateEnd = Utc(3, 29, 7), Status = SessionStatus.Completed },
    };

    // Points предпосчитаны из прогнозов: 1=30+22+30, 2=12+35, 3=0+20, 4=30+14, 5=20.
    public static readonly object[] Users =
    {
        new { Id = 1, FirstName = "Алексей", LastName = (string?)"Иванов", VkId = (string?)"100001", TelegramId = (string?)null, Points = 82 },
        new { Id = 2, FirstName = "Мария", LastName = (string?)"Петрова", VkId = (string?)"100002", TelegramId = (string?)null, Points = 47 },
        new { Id = 3, FirstName = "Дмитрий", LastName = (string?)"Смирнов", VkId = (string?)null, TelegramId = (string?)"tg_300003", Points = 20 },
        new { Id = 4, FirstName = "Анна", LastName = (string?)null, VkId = (string?)null, TelegramId = (string?)"tg_400004", Points = 44 },
        new { Id = 5, FirstName = "Иван", LastName = (string?)"Козлов", VkId = (string?)"100005", TelegramId = (string?)"tg_500005", Points = 20 },
    };

    // Created/Edited вычисляются ТОЛЬКО из констант (детерминированно) — это допустимо.
    public static readonly object[] Predictions =
    {
        // Австралия, гонка (сессия 2)
        new { Id = 1, UserId = 1, SessionId = 2, DriverTopOneId = 1, DriverTopTwoId = 7, DriverTopThreeId = 5, BestSessionLapTimeMs = (int?)null, TotalPointsForThisPrediction = 30, Created = Utc(3, 8, 4).AddHours(-24), Edited = (DateTime?)null },
        new { Id = 2, UserId = 2, SessionId = 2, DriverTopOneId = 1, DriverTopTwoId = 5, DriverTopThreeId = 7, BestSessionLapTimeMs = (int?)null, TotalPointsForThisPrediction = 12, Created = Utc(3, 8, 4).AddHours(-30), Edited = (DateTime?)null },
        new { Id = 3, UserId = 3, SessionId = 2, DriverTopOneId = 3, DriverTopTwoId = 5, DriverTopThreeId = 6, BestSessionLapTimeMs = (int?)null, TotalPointsForThisPrediction = 0, Created = Utc(3, 8, 4).AddHours(-5), Edited = (DateTime?)null },
        // Австралия, квалификация (сессия 1), с прогнозом времени
        new { Id = 4, UserId = 1, SessionId = 1, DriverTopOneId = 1, DriverTopTwoId = 2, DriverTopThreeId = 6, BestSessionLapTimeMs = (int?)75_800, TotalPointsForThisPrediction = 22, Created = Utc(3, 7, 5).AddHours(-26), Edited = (DateTime?)null },
        // Китай: спринт-квалификация (3), квалификация (5), гонка (6)
        new { Id = 5, UserId = 4, SessionId = 3, DriverTopOneId = 5, DriverTopTwoId = 6, DriverTopThreeId = 1, BestSessionLapTimeMs = (int?)null, TotalPointsForThisPrediction = 30, Created = Utc(3, 13, 7, 30).AddHours(-12), Edited = (DateTime?)null },
        new { Id = 6, UserId = 2, SessionId = 5, DriverTopOneId = 2, DriverTopTwoId = 1, DriverTopThreeId = 5, BestSessionLapTimeMs = (int?)91_500, TotalPointsForThisPrediction = 35, Created = Utc(3, 14, 7).AddHours(-18), Edited = (DateTime?)null },
        new { Id = 7, UserId = 4, SessionId = 6, DriverTopOneId = 5, DriverTopTwoId = 2, DriverTopThreeId = 1, BestSessionLapTimeMs = (int?)null, TotalPointsForThisPrediction = 14, Created = Utc(3, 15, 7).AddHours(-20), Edited = (DateTime?)Utc(3, 15, 7).AddHours(-2) },
        // Япония: квалификация (7), гонка (8)
        new { Id = 8, UserId = 3, SessionId = 7, DriverTopOneId = 3, DriverTopTwoId = 5, DriverTopThreeId = 2, BestSessionLapTimeMs = (int?)88_900, TotalPointsForThisPrediction = 20, Created = Utc(3, 28, 6).AddHours(-15), Edited = (DateTime?)null },
        new { Id = 9, UserId = 5, SessionId = 8, DriverTopOneId = 5, DriverTopTwoId = 6, DriverTopThreeId = 1, BestSessionLapTimeMs = (int?)null, TotalPointsForThisPrediction = 20, Created = Utc(3, 29, 5).AddHours(-10), Edited = (DateTime?)null },
        new { Id = 10, UserId = 1, SessionId = 8, DriverTopOneId = 5, DriverTopTwoId = 6, DriverTopThreeId = 3, BestSessionLapTimeMs = (int?)null, TotalPointsForThisPrediction = 30, Created = Utc(3, 29, 5).AddHours(-22), Edited = (DateTime?)Utc(3, 29, 5).AddHours(-3) },
    };

    public static readonly object[] DriverResults = BuildDriverResults();

    public static readonly object[] Titles =
    {
        new { Id = 1, UserId = 1, Year = 2025 },
        new { Id = 2, UserId = 1, Year = 2023 },
        new { Id = 3, UserId = 2, Year = 2024 },
    };

    public static readonly object[] UsersChampionships =
    {
        new { Id = 1, Year = 2026, CreatedByUserId = 1, InviteCode = "A1B2C3" },
        new { Id = 2, Year = 2026, CreatedByUserId = 3, InviteCode = "X9Y8Z7" },
    };

    // Все вступили до первой сессии сезона → TotalPoints равен Points пользователя.
    public static readonly object[] UserInChampionships =
    {
        new { Id = 1, ChampionshipId = 1, UserId = 1, TotalPoints = 82, JoinedAt = Utc(3, 1, 10) },
        new { Id = 2, ChampionshipId = 1, UserId = 2, TotalPoints = 47, JoinedAt = Utc(3, 2, 12) },
        new { Id = 3, ChampionshipId = 1, UserId = 4, TotalPoints = 44, JoinedAt = Utc(3, 3, 9) },
        new { Id = 4, ChampionshipId = 2, UserId = 3, TotalPoints = 20, JoinedAt = Utc(3, 1, 18) },
        new { Id = 5, ChampionshipId = 2, UserId = 5, TotalPoints = 20, JoinedAt = Utc(3, 4, 20) },
    };

    /// <summary>
    /// Топ-5 пилотов каждой сессии: позиция = порядок в массиве, время в мс.
    /// </summary>
    private static object[] BuildDriverResults()
    {
        var driverIds = new Dictionary<string, int>
        {
            ["Russell"] = 1, ["Antonelli"] = 2, ["Verstappen"] = 3, ["Norris"] = 5,
            ["Piastri"] = 6, ["Leclerc"] = 7, ["Hamilton"] = 8,
        };

        var data = new (int SessionId, (string Driver, int BestLapMs)[] Results)[]
        {
            (1, new[] { ("Russell", 75_800), ("Antonelli", 75_934), ("Norris", 76_105), ("Piastri", 76_240), ("Leclerc", 76_388) }),
            (2, new[] { ("Russell", 81_212), ("Leclerc", 80_950), ("Norris", 81_340), ("Hamilton", 81_105), ("Antonelli", 81_020) }),
            (3, new[] { ("Norris", 92_310), ("Piastri", 92_455), ("Russell", 92_601), ("Verstappen", 92_780), ("Hamilton", 92_902) }),
            (4, new[] { ("Piastri", 94_100), ("Norris", 94_050), ("Hamilton", 94_320), ("Russell", 94_410), ("Leclerc", 94_555) }),
            (5, new[] { ("Antonelli", 91_480), ("Russell", 91_533), ("Norris", 91_702), ("Leclerc", 91_890), ("Verstappen", 91_975) }),
            (6, new[] { ("Antonelli", 96_020), ("Norris", 95_870), ("Russell", 96_150), ("Piastri", 96_300), ("Hamilton", 96_410) }),
            (7, new[] { ("Verstappen", 88_950), ("Norris", 89_010), ("Piastri", 89_220), ("Antonelli", 89_340), ("Russell", 89_401) }),
            (8, new[] { ("Norris", 93_100), ("Piastri", 93_250), ("Verstappen", 93_420), ("Russell", 93_390), ("Leclerc", 93_640) }),
        };

        var results = new List<object>();
        var id = 1;
        foreach (var (sessionId, rows) in data)
        {
            for (var position = 1; position <= rows.Length; position++)
            {
                var (driver, bestLapMs) = rows[position - 1];
                results.Add(new
                {
                    Id = id++,
                    SessionId = sessionId,
                    DriverId = driverIds[driver],
                    Position = position,
                    BestLapTimeMs = (int?)bestLapMs,
                });
            }
        }

        return results.ToArray();
    }

    /// <summary>
    /// Дата и время 2026 года в UTC (детерминированно).
    /// </summary>
    private static DateTime Utc(int month, int day, int hour, int minute = 0) =>
        new(2026, month, day, hour, minute, 0, DateTimeKind.Utc);
}