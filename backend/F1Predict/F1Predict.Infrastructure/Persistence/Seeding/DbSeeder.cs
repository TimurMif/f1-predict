using F1Predict.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace F1Predict.Infrastructure.Persistence.Seeding;

/// <summary>
/// Заполняет пустую базу тестовыми данными для разработки:
/// команды и пилоты, два гран-при (первый завершён с результатами, второй впереди),
/// три пользователя, прогнозы и один чемпионат.
/// Если пилоты в базе уже есть, повторный вызов ничего не делает.
/// Даты считаются от текущего момента (UTC), поэтому "будущий" гран-при всегда в будущем.
/// </summary>
public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (await db.Drivers.AnyAsync())
            return;

        var now = DateTime.UtcNow;

        // ---------- Команды ----------
        var mercedes = Team("Mercedes", "German");
        var ferrari = Team("Ferrari", "Italian");
        var mclaren = Team("McLaren", "British");
        var redBull = Team("Red Bull Racing", "Austrian");
        var williams = Team("Williams", "British");
        var astonMartin = Team("Aston Martin", "British");
        var alpine = Team("Alpine", "French");
        var racingBulls = Team("Racing Bulls", "Italian");
        var haas = Team("Haas", "American");
        var audi = Team("Audi", "German");
        var cadillac = Team("Cadillac", "American");

        // ---------- Пилоты ----------
        // Порядок в списке = условный порядок финиша в квалификации первого гран-при
        var drivers = new List<Driver>
        {
            Drv("George", "Russell", "63", "British", mercedes),
            Drv("Kimi", "Antonelli", "12", "Italian", mercedes),
            Drv("Lando", "Norris", "1", "British", mclaren),
            Drv("Oscar", "Piastri", "81", "Australian", mclaren),
            Drv("Charles", "Leclerc", "16", "Monegasque", ferrari),
            Drv("Lewis", "Hamilton", "44", "British", ferrari),
            Drv("Max", "Verstappen", "3", "Dutch", redBull),
            Drv("Isack", "Hadjar", "6", "French", redBull),
            Drv("Alexander", "Albon", "23", "Thai", williams),
            Drv("Carlos", "Sainz", "55", "Spanish", williams),
            Drv("Fernando", "Alonso", "14", "Spanish", astonMartin),
            Drv("Lance", "Stroll", "18", "Canadian", astonMartin),
            Drv("Pierre", "Gasly", "10", "French", alpine),
            Drv("Franco", "Colapinto", "43", "Argentine", alpine),
            Drv("Liam", "Lawson", "30", "New Zealander", racingBulls),
            Drv("Arvid", "Lindblad", "41", "British", racingBulls),
            Drv("Esteban", "Ocon", "31", "French", haas),
            Drv("Oliver", "Bearman", "87", "British", haas),
            Drv("Nico", "Hulkenberg", "27", "German", audi),
            Drv("Gabriel", "Bortoleto", "5", "Brazilian", audi),
            Drv("Sergio", "Perez", "11", "Mexican", cadillac),
            Drv("Valtteri", "Bottas", "77", "Finnish", cadillac),
        };

        // ---------- Гран-при и сессии ----------
        // Гран-при 1: полностью завершён
        var gp1 = new GrandPrix
        {
            Name = "Australian Grand Prix",
            CircuitName = "Albert Park Circuit",
            Country = "Australia",
            RoundNumber = 1,
            Year = 2026
        };
        var gp1Quali = Sess("Qualifying", SessionType.Qualifying, gp1, now.AddDays(-9), SessionStatus.Completed);
        var gp1Race = Sess("Race", SessionType.Race, gp1, now.AddDays(-8), SessionStatus.Completed);

        // Гран-при 2: впереди, приём прогнозов открыт
        var gp2 = new GrandPrix
        {
            Name = "Japanese Grand Prix",
            CircuitName = "Suzuka Circuit",
            Country = "Japan",
            RoundNumber = 2,
            Year = 2026
        };
        var gp2Quali = Sess("Qualifying", SessionType.Qualifying, gp2, now.AddDays(2), SessionStatus.Scheduled);
        var gp2Race = Sess("Race", SessionType.Race, gp2, now.AddDays(3), SessionStatus.Scheduled);

        // ---------- Результаты завершённого гран-при ----------
        var qualiOrder = drivers.ToList();

        // в гонке первые двое поменялись местами
        var raceOrder = drivers.ToList();
        (raceOrder[0], raceOrder[1]) = (raceOrder[1], raceOrder[0]);

        var results = new List<SessionResult>();
        results.AddRange(BuildResults(gp1Quali, qualiOrder, 76_000));   // лучшее время 1:16.000
        results.AddRange(BuildResults(gp1Race, raceOrder, 80_500));     // лучший круг гонки 1:20.500

        // ---------- Пользователи ----------
        var user1 = new User { FirstName = "Алекс", LastName = "Тестов", VkId = "vk_100001", Points = 70 };
        var user2 = new User { FirstName = "Борис", LastName = "Проверкин", TelegramId = "tg_200002", Points = 10 };
        var user3 = new User { FirstName = "Виктор", TelegramId = "tg_300003", Points = 0 };
        var users = new List<User> { user1, user2, user3 };

        // у первого пользователя есть титул прошлого сезона
        user1.Titles.Add(new Title { User = user1, Year = 2025 });

        // ---------- Чемпионат ----------
        // Алекс создал чемпионат, Борис вступил, Виктор остался без чемпионата
        var championship = new UsersChampionship
        {
            Year = 2026,
            CreatedByUser = user1,
            InviteCode = "F1TEST"
        };
        championship.Users.Add(new UserInChampionship
        {
            Championship = championship, User = user1, TotalPoints = 70, JoinedAt = now.AddDays(-30)
        });
        championship.Users.Add(new UserInChampionship
        {
            Championship = championship, User = user2, TotalPoints = 10, JoinedAt = now.AddDays(-20)
        });

        // ---------- Прогнозы ----------
        // Очки здесь условные: логика подсчёта будет написана позже в Application.
        var predictions = new List<Prediction>
        {
            // гонка 1-го гран-при: точный, частично верный и неверный прогнозы
            Pred(user1, gp1Race, raceOrder[0], raceOrder[1], raceOrder[2], null, 30, now.AddDays(-9)),
            Pred(user2, gp1Race, raceOrder[1], raceOrder[0], raceOrder[4], null, 10, now.AddDays(-9)),
            Pred(user3, gp1Race, drivers[10], drivers[11], drivers[12], null, 0, now.AddDays(-9)),

            // квалификация 1-го гран-при с прогнозом времени поула
            Pred(user1, gp1Quali, qualiOrder[0], qualiOrder[1], qualiOrder[2], 76_000, 40, now.AddDays(-10)),

            // будущий гран-при: прогнозы ещё не оценены; у Виктора прогнозов нет
            Pred(user1, gp2Quali, drivers[0], drivers[2], drivers[3], 75_500, 0, now.AddHours(-5)),
            Pred(user2, gp2Race, drivers[1], drivers[0], drivers[2], null, 0, now.AddHours(-3)),
        };

        // ---------- Сохранение ----------
        // EF Core сам добавит связанные сущности (команды вместе с пилотами, сессии вместе с гран-при)
        db.AddRange(drivers);
        db.AddRange(gp1, gp2);
        db.AddRange(results);
        db.AddRange(users);
        db.Add(championship);
        db.AddRange(predictions);

        await db.SaveChangesAsync();
    }

    private static Constructor Team(string name, string nationality) =>
        new() { Name = name, Nationality = nationality };

    private static Driver Drv(string firstName, string lastName, string number, string nationality, Constructor team) =>
        new()
        {
            FirstName = firstName,
            LastName = lastName,
            Number = number,
            Nationality = nationality,
            Constructor = team
        };

    private static Session Sess(string name, SessionType type, GrandPrix grandPrix, DateTime start, SessionStatus status) =>
        new()
        {
            Name = name,
            Type = type,
            GrandPrix = grandPrix,
            DateStart = start,
            DateEnd = start.AddHours(type == SessionType.Race ? 2 : 1),
            Status = status
        };

    /// <summary>
    /// Результаты для всех пилотов в порядке финиша; время лучшего круга растёт с позицией.
    /// </summary>
    private static IEnumerable<SessionResult> BuildResults(Session session, IReadOnlyList<Driver> order, int baseLapMs) =>
        order.Select((driver, index) => new SessionResult
        {
            Session = session,
            Driver = driver,
            Position = index + 1,
            BestLapTimeMs = baseLapMs + index * 137
        });

    private static Prediction Pred(User user, Session session, Driver first, Driver second, Driver third,
        int? bestLapMs, int points, DateTime created) =>
        new()
        {
            User = user,
            Session = session,
            DriverTopOne = first,
            DriverTopTwo = second,
            DriverTopThree = third,
            BestSessionLapTimeMs = bestLapMs,
            TotalPointsForThisPrediction = points,
            Created = created
        };
}