using F1Predict.Contracts.Dto;
using F1Predict.Contracts.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace F1Predict.Api.Controllers;

/// <summary>
/// Прогнозы пользователей на сессии Формулы 1.
/// Текущий пользователь передаётся в заголовке X-User-Id (временно, до авторизации).
/// </summary>
[ApiController]
[Route("api/predictions")]
public class PredictionsController(IPredictionService predictionService) : ControllerBase
{
    /// <summary>
    /// Прогнозы, доступные текущему пользователю (чужие видны после закрытия приёма на сессию).
    /// Параметры userId и sessionId — необязательные фильтры.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<PredictionDto>>> GetAll(
        [FromQuery] int? userId,
        [FromQuery] int? sessionId,
        [FromHeader(Name = ApiHeaders.CurrentUser)] int currentUserId)
        => Ok(await predictionService.GetAllAsync(userId, sessionId, currentUserId));

    /// <summary>
    /// Прогноз по Id.
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PredictionDto>> GetById(
        int id,
        [FromHeader(Name = ApiHeaders.CurrentUser)] int currentUserId)
    {
        var prediction = await predictionService.GetByIdAsync(id, currentUserId);
        return prediction is null ? NotFound() : Ok(prediction);
    }

    /// <summary>
    /// Сделать прогноз на сессию от имени текущего пользователя.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<PredictionDto>> Create(
        [FromBody] PredictionEditDto dto,
        [FromHeader(Name = ApiHeaders.CurrentUser)] int currentUserId)
    {
        var created = await predictionService.CreateAsync(currentUserId, dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Изменить свой прогноз (пока приём на сессию открыт; сессия не меняется).
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] PredictionEditDto dto,
        [FromHeader(Name = ApiHeaders.CurrentUser)] int currentUserId)
        => await predictionService.UpdateAsync(id, currentUserId, dto) ? NoContent() : NotFound();

    /// <summary>
    /// Удалить свой прогноз (пока приём на сессию открыт).
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        [FromHeader(Name = ApiHeaders.CurrentUser)] int currentUserId)
        => await predictionService.DeleteAsync(id, currentUserId) ? NoContent() : NotFound();
}