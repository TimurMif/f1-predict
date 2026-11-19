using F1Predict.Contracts.Dto;
using F1Predict.Contracts.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace F1Predict.Api.Controllers;

/// <summary>
/// Этапы чемпионата Формулы 1
/// </summary>
[ApiController]
[Route("api/grand-prix")]
public class GrandPrixController(IGrandPrixService grandPrixService) : ControllerBase
{
    /// <summary>Список всех этапов с сессиями.</summary>
    [HttpGet]
    public async Task<ActionResult<List<GrandPrixDto>>> GetAll()
        => Ok(await grandPrixService.GetAllAsync());

    /// <summary>Этап по Id.</summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<GrandPrixDto>> GetById(int id)
    {
        var grandPrix = await grandPrixService.GetByIdAsync(id);
        return grandPrix is null ? NotFound() : Ok(grandPrix);
    }

    /// <summary>Создать этап.</summary>
    [HttpPost]
    public async Task<ActionResult<GrandPrixDto>> Create([FromBody] GrandPrixEditDto dto)
    {
        var created = await grandPrixService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>Изменить этап.</summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] GrandPrixEditDto dto)
        => await grandPrixService.UpdateAsync(id, dto) ? NoContent() : NotFound();

    /// <summary>Удалить этап (только если в нём нет сессий).</summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
        => await grandPrixService.DeleteAsync(id) ? NoContent() : NotFound();
}