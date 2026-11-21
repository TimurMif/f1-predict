using F1Predict.Contracts.Dto;
using F1Predict.Contracts.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace F1Predict.Api.Controllers;

/// <summary>
/// Пилоты Формулы 1
/// </summary>
[ApiController]
[Route("api/drivers")]
public class DriversController(IDriverService driverService) : ControllerBase
{
    /// <summary>
    /// Список всех пилотов с их командами.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<DriverDto>>> GetAll()
        => Ok(await driverService.GetAllAsync());

    /// <summary>
    /// Пилот по Id.
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<DriverDto>> GetById(int id)
    {
        var driver = await driverService.GetByIdAsync(id);
        return driver is null ? NotFound() : Ok(driver);
    }

    /// <summary>
    /// Создать пилота.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<DriverDto>> Create([FromBody] DriverEditDto dto)
    {
        var created = await driverService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Изменить пилота (в том числе перевести в другую команду).
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] DriverEditDto dto)
        => await driverService.UpdateAsync(id, dto) ? NoContent() : NotFound();

    /// <summary>
    /// Удалить пилота (только если нет его результатов и прогнозов на него).
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
        => await driverService.DeleteAsync(id) ? NoContent() : NotFound();
}