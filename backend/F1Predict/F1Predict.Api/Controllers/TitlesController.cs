using F1Predict.Contracts.Dto;
using F1Predict.Contracts.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace F1Predict.Api.Controllers;

/// <summary>
/// Титулы чемпионов прогнозов
/// </summary>
[ApiController]
[Route("api/titles")]
public class TitlesController(ITitleService titleService) : ControllerBase
{
    /// <summary>
    /// Список титулов; параметр userId оставляет только титулы пользователя.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<TitleDto>>> GetAll([FromQuery] int? userId)
        => Ok(await titleService.GetAllAsync(userId));

    /// <summary>
    /// Титул по Id.
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TitleDto>> GetById(int id)
    {
        var title = await titleService.GetByIdAsync(id);
        return title is null ? NotFound() : Ok(title);
    }

    /// <summary>
    /// Выдать титул пользователю за сезон.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<TitleDto>> Create([FromBody] TitleEditDto dto)
    {
        var created = await titleService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Изменить год титула (владелец не меняется).
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] TitleEditDto dto)
        => await titleService.UpdateAsync(id, dto) ? NoContent() : NotFound();

    /// <summary>
    /// Удалить титул.
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
        => await titleService.DeleteAsync(id) ? NoContent() : NotFound();
}