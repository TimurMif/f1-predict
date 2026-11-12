using F1Predict.Domain.Models;
using F1Predict.Contracts.Dto;

namespace F1Predict.Application.Mapping;

/// <summary>
/// Методы преобразования доменных моделей в DTO и обратно.
/// </summary>
public static class MappingExtensions
{
    /// <summary>
    /// Метод преобразования сущности пользователя в Dto
    /// </summary>
    public static UserDto ToDto(this User u)
    {
        return new UserDto
        {
            Id = u.Id,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Points = u.Points,
            Titles = u.Titles.Select(t => t.ToDto()).ToList(),
            ChampionshipId = u.UserInChampionship.ChampionshipId
        };
    }
    
    /// <summary>
    /// Метод преобразования Dto пользователя в сущность
    /// </summary>
    public static User ToEntity(this UserEditDto dto)
    {
        return new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            VkId = dto.VkId,
            TelegramId = dto.TelegramId,
            Points = 0
        };  
    }
    
    /// <summary>
    /// Преобразует доменную модель титула в объект DTO
    /// </summary>
    public static TitleDto ToDto(this Title t)
    {
        return new TitleDto
        {
            Id = t.Id,
            UserId = t.UserId,
            Year = t.Year
        };
    }

    /// <summary>
    /// Преобразует объект DTO в доменную модель титула
    /// </summary>
    public static Title ToEntity(this TitleEditDto dto, User user)
    {
        return new Title
        {
            UserId = user.Id,
            User = user,
            Year = dto.Year
        };
    }
    
    /// <summary>
    /// Преобразует доменную модель профиля участника чемпионата в объект DTO
    /// </summary>
    public static UserInChampionshipDto ToDto(this UserInChampionship p)
    {
        return new UserInChampionshipDto
        {
            Id = p.Id,
            ChampionshipId = p.ChampionshipId,
            UserId = p.UserId,
            TotalPoints = p.TotalPoints,
            JoinedAt = p.JoinedAt
        };
    }

    /// <summary>
    /// Преобразует объект DTO в доменную модель профиля участника чемпионата
    /// </summary>
    public static UserInChampionship ToEntity(this UserInChampionshipEditDto dto, User user, UsersChampionship championship)
    {
        return new UserInChampionship
        {
            UserId = user.Id,
            User = user,
            ChampionshipId = championship.Id,
            Championship = championship,
            TotalPoints = 0,
            JoinedAt = DateTime.UtcNow
        };
    }
    
    /// <summary>
    /// Преобразует доменную модель чемпионата пользователей в объект DTO
    /// </summary>
    public static UsersChampionshipDto ToDto(this UsersChampionship c)
    {
        return new UsersChampionshipDto
        {
            Id = c.Id,
            Year = c.Year,
            InviteCode = c.InviteCode,
            Users = c.Users.Select(u => u.ToDto()).ToList()
        };
    }

    /// <summary>
    /// Преобразует объект DTO в доменную модель чемпионата пользователей.
    /// InviteCode генерируется на сервере и передаётся параметром.
    /// </summary>
    public static UsersChampionship ToEntity(this UsersChampionshipEditDto dto, User createdByUser, string inviteCode)
    {
        return new UsersChampionship
        {
            Year = dto.Year,
            CreatedByUserId = createdByUser.Id,
            CreatedByUser = createdByUser,
            InviteCode = inviteCode
        };
    }
    
    /// <summary>
    /// Преобразует доменную модель команды в объект DTO
    /// </summary>
    public static ConstructorDto ToDto(this Constructor c)
    {
        return new ConstructorDto
        {
            Id = c.Id,
            Name = c.Name,
            Nationality = c.Nationality,
            Points = c.Points
        };
    }

    /// <summary>
    /// Преобразует объект DTO в доменную модель команды
    /// </summary>
    public static Constructor ToEntity(this ConstructorEditDto dto)
    {
        return new Constructor
        {
            Name = dto.Name,
            Nationality = dto.Nationality,
            Points = dto.Points
        };
    }
    
    /// <summary>
    /// Преобразует доменную модель пилота в объект DTO
    /// </summary>
    public static DriverDto ToDto(this Driver d)
    {
        return new DriverDto
        {
            Id = d.Id,
            FirstName = d.FirstName,
            LastName = d.LastName,
            Number = d.Number,
            Nationality = d.Nationality,
            Points = d.Points,
            Constructor = d.Constructor.ToDto()
        };
    }

    /// <summary>
    /// Преобразует объект DTO в доменную модель пилота
    /// </summary>
    public static Driver ToEntity(this DriverEditDto dto, Constructor constructor)
    {
        return new Driver
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Number = dto.Number,
            Nationality = dto.Nationality,
            Points = dto.Points,
            ConstructorId = constructor.Id,
            Constructor = constructor
        };
    }
    
    /// <summary>
    /// Преобразует доменную модель Гран-при в объект DTO
    /// </summary>
    public static GrandPrixDto ToDto(this GrandPrix g)
    {
        return new GrandPrixDto
        {
            Id = g.Id,
            Name = g.Name,
            CircuitName = g.CircuitName,
            Country = g.Country,
            Year = g.Year,
            Sessions = g.Sessions.Select(s => s.ToDto()).ToList()
        };
    }

    /// <summary>
    /// Преобразует объект DTO в доменную модель Гран-при
    /// </summary>
    public static GrandPrix ToEntity(this GrandPrixEditDto dto)
    {
        return new GrandPrix
        {
            Name = dto.Name,
            CircuitName = dto.CircuitName,
            Country = dto.Country,
            RoundNumber = dto.RoundNumber,
            Year = dto.Year
        };
    }
    
    /// <summary>
    /// Преобразует доменную модель прогноза в объект DTO
    /// </summary>
    public static PredictionDto ToDto(this Prediction p)
    {
        return new PredictionDto
        {
            Id = p.Id,
            UserId = p.UserId,
            SessionId = p.SessionId,
            DriverTopOneId = p.DriverTopOneId,
            DriverTopTwoId = p.DriverTopTwoId,
            DriverTopThreeId = p.DriverTopThreeId,
            BestSessionLapTimeMs = p.BestSessionLapTimeMs,
            TotalPointsForThisPrediction = p.TotalPointsForThisPrediction,
            Created = p.Created,
            Edited = p.Edited
        };
    }

    /// <summary>
    /// Преобразует объект DTO в доменную модель прогноза
    /// </summary>
    public static Prediction ToEntity(this PredictionEditDto dto, User user, Session session,
        Driver topOne, Driver topTwo, Driver topThree)
    {
        return new Prediction
        {
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
            BestSessionLapTimeMs = dto.BestSessionLapMs,
            TotalPointsForThisPrediction = 0,
            Created = DateTime.UtcNow,
            Edited = null
        };
    }
    
    /// <summary>
    /// Преобразует доменную модель сессии в объект DTO
    /// </summary>
    public static SessionDto ToDto(this Session s)
    {
        return new SessionDto
        {
            Id = s.Id,
            GrandPrixId = s.GrandPrixId,
            Name = s.Name,
            Type = s.Type,
            DateStart = s.DateStart,
            DateEnd = s.DateEnd,
            Status = s.Status
        };
    }

    /// <summary>
    /// Преобразует объект DTO в доменную модель сессии
    /// </summary>
    public static Session ToEntity(this SessionEditDto dto, GrandPrix grandPrix)
    {
        return new Session
        {
            GrandPrixId = grandPrix.Id,
            GrandPrix = grandPrix,
            Name = dto.Name,
            Type = dto.Type,
            DateStart = dto.DateStart,
            DateEnd = dto.DateEnd,
            Status = dto.Status
        };
    }
    
    /// <summary>
    /// Преобразует доменную модель результата пилота в объект DTO
    /// </summary>
    public static DriverResultDto ToDto(this DriverResult r)
    {
        return new DriverResultDto
        {
            Id = r.Id,
            SessionId = r.SessionId,
            Driver = r.Driver.ToDto(),
            Position = r.Position,
            BestLapTimeMs = r.BestLapTimeMs
        };
    }

    /// <summary>
    /// Преобразует объект DTO в доменную модель результата пилота
    /// </summary>
    public static DriverResult ToEntity(this DriverResultEditDto dto, Session session, Driver driver)
    {
        return new DriverResult
        {
            SessionId = session.Id,
            Session = session,
            DriverId = driver.Id,
            Driver = driver,
            Position = dto.Position,
            BestLapTimeMs = dto.BestLapTimeMs
        };
    }
}