using F1Predict.Context;
using F1Predict.Domain.Models;
using F1Predict.Domain.ValueObjects;

namespace F1Predict.Data;

public class DataSeeder
{
    public static readonly DateTime ReferenceDateNow =
        new(2026, 4, 1, 12, 0, 0, DateTimeKind.Utc);

    public static F1PredictContext Seed()
    {
        var context = new F1PredictContext();

        SeedConstructors(context);
        SeedDrivers(context);
        SeedGrandPrix(context);
        SeedSessions(context);
        SeedUsers(context);
        SeedPredictions(context);
        SeedDriverResults(context);
        SeedTitles(context);
        SeedUsersChampionships(context);
        SeedUserInChampionship(context);

        return context;
    }

    private static void SeedConstructors(F1PredictContext context)
    {
        var data = new (string Name, string Nationality, int Points)[]
        {
            ("Red Bull", "Austria", 143),
            ("Mercedes", "German", 151),
            ("Ferrari", "Italy", 120),
            ("McLaren", "Britain", 174),
            ("Williams", "Britain", 56),
            ("Haas", "USA", 23),
            ("Racing Bulls", "Austria", 41),
            ("Audi", "German", 11),
            ("Alpine", "France", 19),
            ("Aston Martin", "Britain", 9),
            ("Cadillac", "USA", 0),
        };
        for (var i = 0; i < data.Length; i++)
        {
            context.Constructors.Add(new Constructor
            {
                Id = i + 1,
                Name = data[i].Name,
                Nationality = data[i].Nationality,
                Points = data[i].Points,
            });
        }
    }

    private static void SeedDrivers(F1PredictContext context)
    {
        // ConstructorId: 1 Red Bull, 2 Mercedes, 3 Ferrari, 4 McLaren, 5 Williams, 6 Haas,
        // 7 Racing Bulls, 8 Audi, 9 Alpine, 10 Aston Martin, 11 Cadillac
        // Очки пилотов команды в сумме дают очки команды из SeedConstructors
        var data = new (string Name, string LastName, int Number, string Nationality, int Points, int ConstructorId)[]
        {
            ("George", "Russell", 63, "Britain", 65, 2),
            ("Kimi", "Antonelli", 12, "Italy", 86, 2),
            ("Max", "Verstappen", 3, "Netherlands", 98, 1),
            ("Isack", "Hadjar", 6, "France", 45, 1),
            ("Lando", "Norris", 1, "Britain", 91, 4),
            ("Oscar", "Piastri", 81, "Australia", 83, 4),
            ("Charles", "Leclerc", 16, "Monaco", 68, 3),
            ("Lewis", "Hamilton", 44, "Britain", 52, 3),
            ("Alexander", "Albon", 23, "Thailand", 24, 5),
            ("Carlos", "Sainz", 55, "Spain", 32, 5),
            ("Liam", "Lawson", 30, "New Zealand", 22, 7),
            ("Arvid", "Lindblad", 41, "Britain", 19, 7),
            ("Esteban", "Ocon", 31, "France", 9, 6),
            ("Oliver", "Bearman", 87, "Britain", 14, 6),
            ("Nico", "Hulkenberg", 27, "Germany", 4, 8),
            ("Gabriel", "Bortoleto", 5, "Brazil", 7, 8),
            ("Pierre", "Gasly", 10, "France", 12, 9),
            ("Franco", "Colapinto", 43, "Argentina", 7, 9),
            ("Fernando", "Alonso", 14, "Spain", 6, 10),
            ("Lance", "Stroll", 18, "Canada", 3, 10),
            ("Sergio", "Perez", 11, "Mexico", 0, 11),
            ("Valtteri", "Bottas", 77, "Finland", 0, 11),
        };

        for (var i = 0; i < data.Length; i++)
        {
            var constructor = context.Constructors.First(c => c.Id == data[i].ConstructorId);
            context.Drivers.Add(new Driver
            {
                Id = i + 1,
                FirstName = data[i].Name,
                LastName = data[i].LastName,
                Number = data[i].Number.ToString(),
                Nationality = data[i].Nationality,
                Points = data[i].Points,
                ConstructorId = constructor.Id,
                Constructor = constructor,
            });
        }
    }

    private static void SeedGrandPrix(F1PredictContext context)
    {
        var data = new (string Name, string CircuitName, string Country, int RoundNumber, int Year)[]
        {
            ("Australian Grand Prix", "Albert Park", "Australia", 1, 2026),
            ("Chinese Grand Prix", "Shanghai GP", "China", 2, 2026),
            ("Japanese Grand Prix", "Suzuka", "Japan", 3, 2026),
        };

        for (var i = 0; i < data.Length; i++)
        {
            context.GrandPrix.Add(new GrandPrix
            {
                Id = i + 1,
                Name = data[i].Name,
                CircuitName = data[i].CircuitName,
                Country = data[i].Country,
                RoundNumber = data[i].RoundNumber,
                Year = data[i].Year,
            });
        }
    }

    private static void SeedSessions(F1PredictContext context)
    {
        // Id сессий идут подряд: 1-2 Австралия, 3-6 Китай (спринт-уикенд), 7-8 Япония.
        // Все сессии раньше ReferenceDateNow, поэтому завершены. Время условное, в UTC.
        var data = new (int GrandPrixId, string Name, SessionType Type, DateTime DateStart, DateTime DateEnd, SessionStatus Status)[]
        {
            (1, "Qualifying", SessionType.Qualifying, Utc(3, 7, 5), Utc(3, 7, 6), SessionStatus.Completed),
            (1, "Race", SessionType.Race, Utc(3, 8, 4), Utc(3, 8, 6), SessionStatus.Completed),

            (2, "Sprint Qualifying", SessionType.SprintQualifying, Utc(3, 13, 7, 30), Utc(3, 13, 8, 15), SessionStatus.Completed),
            (2, "Sprint", SessionType.Sprint, Utc(3, 14, 3), Utc(3, 14, 4), SessionStatus.Completed),
            (2, "Qualifying", SessionType.Qualifying, Utc(3, 14, 7), Utc(3, 14, 8), SessionStatus.Completed),
            (2, "Race", SessionType.Race, Utc(3, 15, 7), Utc(3, 15, 9), SessionStatus.Completed),

            (3, "Qualifying", SessionType.Qualifying, Utc(3, 28, 6), Utc(3, 28, 7), SessionStatus.Completed),
            (3, "Race", SessionType.Race, Utc(3, 29, 5), Utc(3, 29, 7), SessionStatus.Completed),
        };

        for (var i = 0; i < data.Length; i++)
        {
            var grandPrix = context.GrandPrix.First(g => g.Id == data[i].GrandPrixId);
            var session = new Session
            {
                Id = i + 1,
                Name = data[i].Name,
                GrandPrixId = grandPrix.Id,
                GrandPrix = grandPrix,
                Type = data[i].Type,
                DateStart = data[i].DateStart,
                DateEnd = data[i].DateEnd,
                Status = data[i].Status,
            };
            context.Sessions.Add(session);
            grandPrix.Sessions.Add(session);
        }
    }

    private static void SeedUsers(F1PredictContext context)
    {
        // Очки пользователей накапливаются в SeedPredictions
        var data = new (string FirstName, string? LastName, string? VkId, string? TelegramId)[]
        {
            ("Алексей", "Иванов", "100001", null),
            ("Мария", "Петрова", "100002", null),
            ("Дмитрий", "Смирнов", null, "tg_300003"),
            ("Анна", null, null, "tg_400004"),
            ("Иван", "Козлов", "100005", "tg_500005"),
        };

        for (var i = 0; i < data.Length; i++)
        {
            context.Users.Add(new User
            {
                Id = i + 1,
                FirstName = data[i].FirstName,
                LastName = data[i].LastName,
                VkId = data[i].VkId,
                TelegramId = data[i].TelegramId,
            });
        }
    }

    private static void SeedPredictions(F1PredictContext context)
    {
        // Пилоты указаны по фамилии. Очки за прогнозы условные (правила подсчёта пока не реализованы).
        // HoursBeforeStart — за сколько часов до начала сессии создан прогноз,
        // EditedHoursBeforeStart — за сколько часов до начала он был изменён (null, если не менялся).
        // Один пользователь — один прогноз на одну сессию.
        var data = new (int UserId, int SessionId, string TopOne, string TopTwo, string TopThree,
            int? BestLapMs, int Points, int HoursBeforeStart, int? EditedHoursBeforeStart)[]
        {
            // Австралия, гонка (сессия 2): точный, частично верный и неверный прогнозы
            (1, 2, "Russell", "Leclerc", "Norris", null, 30, 24, null),
            (2, 2, "Russell", "Norris", "Leclerc", null, 12, 30, null),
            (3, 2, "Verstappen", "Norris", "Piastri", null, 0, 5, null),

            // Австралия, квалификация (сессия 1) с прогнозом времени
            (1, 1, "Russell", "Antonelli", "Piastri", 75_800, 22, 26, null),

            // Китай: спринт-квалификация (3), квалификация (5), гонка (6)
            (4, 3, "Norris", "Piastri", "Russell", null, 30, 12, null),
            (2, 5, "Antonelli", "Russell", "Norris", 91_500, 35, 18, null),
            (4, 6, "Norris", "Antonelli", "Russell", null, 14, 20, 2),

            // Япония: квалификация (7), гонка (8)
            (3, 7, "Verstappen", "Norris", "Antonelli", 88_900, 20, 15, null),
            (5, 8, "Norris", "Piastri", "Russell", null, 20, 10, null),
            (1, 8, "Norris", "Piastri", "Verstappen", null, 30, 22, 3),
        };

        for (var i = 0; i < data.Length; i++)
        {
            var item = data[i];
            var user = context.Users.First(u => u.Id == item.UserId);
            var session = context.Sessions.First(s => s.Id == item.SessionId);
            var topOne = FindDriver(context, item.TopOne);
            var topTwo = FindDriver(context, item.TopTwo);
            var topThree = FindDriver(context, item.TopThree);

            var prediction = new Prediction
            {
                Id = i + 1,
                UserId = user.Id,
                User = user,
                SessionId = session.Id,
                Session = session,
                DriverTopOneId = topOne.Id,
                DriverTopOne = topOne,
                DriverTopTwoId = topTwo.Id,
                DriverTopTwo = topTwo,
                DriverTopThreeId = topThree.Id,
                DriverTopThree = topThree,
                BestSessionLapTimeMs = item.BestLapMs,
                TotalPointsForThisPrediction = item.Points,
                Created = session.DateStart.AddHours(-item.HoursBeforeStart),
                Edited = item.EditedHoursBeforeStart is { } editedHours
                    ? session.DateStart.AddHours(-editedHours)
                    : null,
            };

            context.Predictions.Add(prediction);
            user.Predictions.Add(prediction);
            user.Points += prediction.TotalPointsForThisPrediction;
        }
    }

    private static void SeedDriverResults(F1PredictContext context)
    {
        // Для каждой сессии: топ-5 пилотов по порядку финиша (позиция = номер в массиве, начиная с 1)
        // и лучший круг каждого пилота в миллисекундах (75_800 = 1:15.800). Результаты условные.
        var data = new (int SessionId, (string Driver, int BestLapMs)[] Results)[]
        {
            // 1. Австралия, квалификация
            (1, new[]
            {
                ("Russell", 75_800),
                ("Antonelli", 75_934),
                ("Norris", 76_105),
                ("Piastri", 76_240),
                ("Leclerc", 76_388),
            }),
            // 2. Австралия, гонка
            (2, new[]
            {
                ("Russell", 81_212),
                ("Leclerc", 80_950),
                ("Norris", 81_340),
                ("Hamilton", 81_105),
                ("Antonelli", 81_020),
            }),
            // 3. Китай, спринт-квалификация
            (3, new[]
            {
                ("Norris", 92_310),
                ("Piastri", 92_455),
                ("Russell", 92_601),
                ("Verstappen", 92_780),
                ("Hamilton", 92_902),
            }),
            // 4. Китай, спринт
            (4, new[]
            {
                ("Piastri", 94_100),
                ("Norris", 94_050),
                ("Hamilton", 94_320),
                ("Russell", 94_410),
                ("Leclerc", 94_555),
            }),
            // 5. Китай, квалификация
            (5, new[]
            {
                ("Antonelli", 91_480),
                ("Russell", 91_533),
                ("Norris", 91_702),
                ("Leclerc", 91_890),
                ("Verstappen", 91_975),
            }),
            // 6. Китай, гонка
            (6, new[]
            {
                ("Antonelli", 96_020),
                ("Norris", 95_870),
                ("Russell", 96_150),
                ("Piastri", 96_300),
                ("Hamilton", 96_410),
            }),
            // 7. Япония, квалификация
            (7, new[]
            {
                ("Verstappen", 88_950),
                ("Norris", 89_010),
                ("Piastri", 89_220),
                ("Antonelli", 89_340),
                ("Russell", 89_401),
            }),
            // 8. Япония, гонка
            (8, new[]
            {
                ("Norris", 93_100),
                ("Piastri", 93_250),
                ("Verstappen", 93_420),
                ("Russell", 93_390),
                ("Leclerc", 93_640),
            }),
        };

        var id = 1;
        foreach (var (sessionId, results) in data)
        {
            var session = context.Sessions.First(s => s.Id == sessionId);

            for (var position = 1; position <= results.Length; position++)
            {
                var (driverLastName, bestLapMs) = results[position - 1];
                var driver = FindDriver(context, driverLastName);

                var result = new DriverResult
                {
                    Id = id++,
                    SessionId = session.Id,
                    Session = session,
                    DriverId = driver.Id,
                    Driver = driver,
                    Position = position,
                    BestLapTimeMs = bestLapMs,
                };

                context.DriverResults.Add(result);
                session.Results.Add(result);
            }
        }
    }

    private static void SeedTitles(F1PredictContext context)
    {
        var data = new (int UserId, int Year)[]
        {
            (1, 2025),
            (1, 2023),
            (2, 2024),
        };

        for (var i = 0; i < data.Length; i++)
        {
            var user = context.Users.First(u => u.Id == data[i].UserId);
            var title = new Title
            {
                Id = i + 1,
                UserId = user.Id,
                User = user,
                Year = data[i].Year,
            };

            context.Titles.Add(title);
            user.Titles.Add(title);
        }
    }

    private static void SeedUsersChampionships(F1PredictContext context)
    {
        var data = new (int Year, int CreatedByUserId, string InviteCode)[]
        {
            (2026, 1, "A1B2C3"),
            (2026, 3, "X9Y8Z7"),
        };

        for (var i = 0; i < data.Length; i++)
        {
            var creator = context.Users.First(u => u.Id == data[i].CreatedByUserId);
            context.UsersChampionships.Add(new UsersChampionship
            {
                Id = i + 1,
                Year = data[i].Year,
                CreatedByUserId = creator.Id,
                CreatedByUser = creator,
                InviteCode = data[i].InviteCode,
            });
        }
    }

    private static void SeedUserInChampionship(F1PredictContext context)
    {
        // Один пользователь состоит максимум в одном чемпионате.
        // Все вступили до первой сессии сезона, поэтому очки в чемпионате равны очкам пользователя.
        var data = new (int ChampionshipId, int UserId, DateTime JoinedAt)[]
        {
            (1, 1, Utc(3, 1, 10)),
            (1, 2, Utc(3, 2, 12)),
            (1, 4, Utc(3, 3, 9)),
            (2, 3, Utc(3, 1, 18)),
            (2, 5, Utc(3, 4, 20)),
        };

        for (var i = 0; i < data.Length; i++)
        {
            var championship = context.UsersChampionships.First(c => c.Id == data[i].ChampionshipId);
            var user = context.Users.First(u => u.Id == data[i].UserId);

            var userInChampionship = new UserInChampionship
            {
                Id = i + 1,
                ChampionshipId = championship.Id,
                Championship = championship,
                UserId = user.Id,
                User = user,
                TotalPoints = user.Points,
                JoinedAt = data[i].JoinedAt,
            };

            context.UserInChampionships.Add(userInChampionship);
            championship.Users.Add(userInChampionship);
            user.UserInChampionship = userInChampionship;
        }
    }

    /// <summary>
    /// Дата и время 2026 года в UTC.
    /// </summary>
    private static DateTime Utc(int month, int day, int hour, int minute = 0) =>
        new(2026, month, day, hour, minute, 0, DateTimeKind.Utc);

    private static Driver FindDriver(F1PredictContext context, string lastName) =>
        context.Drivers.First(d => d.LastName == lastName);
}