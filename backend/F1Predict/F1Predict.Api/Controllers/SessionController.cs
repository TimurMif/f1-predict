using F1Predict.Contracts.Dto;
using F1Predict.Contracts.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace F1Predict.Api.Controllers;

/// <summary>
/// Сессии этапов Формулы 1 (квалификации, гонки, спринты)
/// </summary>
[ApiController]
[Route("api/sessions")]
public class SessionsController(ISessionService sessionService) : ControllerBase
{
    /// <summary>
    /// Список сессий по порядку начала; параметр grandPrixId оставляет только сессии этапа.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<SessionDto>>> GetAll([FromQuery] int? grandPrixId)
        => Ok(await sessionService.GetAllAsync(grandPrixId));

    /// <summary>
    /// Сессия по Id.
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<SessionDto>> GetById(int id)
    {
        var session = await sessionService.GetByIdAsync(id);
        return session is null ? NotFound() : Ok(session);
    }

    /// <summary>
    /// Создать сессию.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<SessionDto>> Create([FromBody] SessionEditDto dto)
    {
        var created = await sessionService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Изменить сессию (перенос в другой этап не поддерживается).
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] SessionEditDto dto)
        => await sessionService.UpdateAsync(id, dto) ? NoContent() : NotFound();

    /// <summary>
    /// Удалить сессию (только если на неё нет прогнозов; результаты удаляются вместе с ней).
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
        => await sessionService.DeleteAsync(id) ? NoContent() : NotFound();
}