using F1Predict.Contracts.Dto;
using F1Predict.Contracts.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace F1Predict.Api.Controllers;

/// <summary>
/// Контроллер для команд Формулы 1
/// </summary>
[ApiController]
[Route("api/constructor")]
public class ConstructorController(IConstructorService constructorService) : ControllerBase
{
    /// <summary>
    /// Список всех команд Формулы 1.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<ConstructorDto>>> GetAll()
        => Ok(await constructorService.GetAllAsync());

    /// <summary>
    /// Возвращает команду по Id
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ConstructorDto>> GetById(int id)
    {
        var constructor = await constructorService.GetByIdAsync(id);
        return constructor is null ? NotFound() : Ok(constructor);
    }

    /// <summary>
    /// Создание новой команды Формулы 1
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ConstructorDto>> Create([FromBody] ConstructorEditDto dto)
    {
        var created = await constructorService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
    
    /// <summary>
    /// Изменить команду Формулы 1
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ConstructorEditDto dto)
        => await constructorService.UpdateAsync(id, dto) ? NoContent() : NotFound();
    
    /// <summary>
    /// Удалить команду формулы 1
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
        => await constructorService.DeleteAsync(id) ? NoContent() : NotFound();
}