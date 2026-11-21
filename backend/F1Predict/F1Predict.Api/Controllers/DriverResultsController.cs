using F1Predict.Contracts.Dto;
using F1Predict.Contracts.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace F1Predict.Api.Controllers;

/// <summary>
/// Результаты пилотов в сессиях
/// </summary>
[ApiController]
[Route("api/driver-results")]
public class DriverResultsController(IDriverResultService driverResultService) : ControllerBase
{
    /// <summary>
    /// Результаты пилотов; параметр sessionId оставляет только результаты сессии по порядку позиций.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<DriverResultDto>>> GetAll([FromQuery] int? sessionId)
        => Ok(await driverResultService.GetAllAsync(sessionId));

    /// <summary>
    /// Результат пилота по Id.
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<DriverResultDto>> GetById(int id)
    {
        var result = await driverResultService.GetByIdAsync(id);
        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Внести результат пилота (только для завершённой сессии).
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<DriverResultDto>> Create([FromBody] DriverResultEditDto dto)
    {
        var created = await driverResultService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Изменить позицию и время круга (сессия и пилот не меняются).
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] DriverResultEditDto dto)
        => await driverResultService.UpdateAsync(id, dto) ? NoContent() : NotFound();

    /// <summary>
    /// Удалить результат пилота.
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
        => await driverResultService.DeleteAsync(id) ? NoContent() : NotFound();
}