using F1Predict.Application.Exceptions;
using F1Predict.Application.Interfaces;
using F1Predict.Application.Mapping;
using F1Predict.Contracts.Dto;
using F1Predict.Contracts.ServiceInterfaces;
using F1Predict.Domain.Models;

namespace F1Predict.Application.Services;

/// <summary>
/// Сервис для работы с сессиями
/// </summary>
public class SessionService(
    IRepository<Session> sessionRepository,
    IRepository<GrandPrix> grandPrixRepository) : ISessionService
{
    /// <summary>
    /// Возвращает список сессий
    /// </summary>
    public async Task<List<SessionDto>> GetAllAsync()
    {
        var sessions = await sessionRepository.GetAllAsync();
        return sessions.Select(s => s.ToDto()).ToList();
    }

    /// <summary>
    /// Возвращает сессию по ее Id
    /// </summary>
    public async Task<SessionDto?> GetByIdAsync(int id)
    {
        var session = await sessionRepository.GetByIdAsync(id);
        return session?.ToDto();
    }

    /// <summary>
    /// Создает новую сессию
    /// </summary>
    public async Task<SessionDto?> CreateAsync(SessionEditDto dto)
    {
        var grandPrix = await grandPrixRepository.GetByIdAsync(dto.GrandPrixId);
        if (grandPrix is null) return null;

        await EnsureTypeUnique(dto.GrandPrixId, dto.Type, excludeId: null);

        var created = await sessionRepository.AddAsync(dto.ToEntity(grandPrix));
        return created.ToDto();
    }

    /// <summary>
    /// Обновляем данные существующей сессии
    /// </summary>
    public async Task<bool> UpdateAsync(int id, SessionEditDto dto)
    {
        var session = await sessionRepository.GetByIdAsync(id);
        if (session is null) return false;

        // GrandPrixId — идентичность сессии, перенос между этапами не поддерживаем
        await EnsureTypeUnique(session.GrandPrixId, dto.Type, excludeId: id);

        session.Name = dto.Name;
        session.Type = dto.Type;
        session.DateStart = dto.DateStart;
        session.DateEnd = dto.DateEnd;
        session.Status = dto.Status;
        return await sessionRepository.UpdateAsync(session);
    }

    /// <summary>
    /// Удаляем сессию по ее Id
    /// </summary>
    public Task<bool> DeleteAsync(int id)
    {
        return sessionRepository.DeleteAsync(id);
    }
    
    private async Task EnsureTypeUnique(int grandPrixId, SessionType type, int? excludeId)
    {
        var all = await sessionRepository.GetAllAsync();
        if (all.Any(s => s.GrandPrixId == grandPrixId && s.Type == type && s.Id != excludeId))
            throw new ConflictException("Сессия этого типа уже существует в данном этапе");
    }
}