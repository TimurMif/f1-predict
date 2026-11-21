using F1Predict.Contracts.Dto;
using F1Predict.Contracts.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace F1Predict.Api.Controllers;

/// <summary>
/// Пользователи сервиса прогнозов
/// </summary>
[ApiController]
[Route("api/users")]
public class UsersController(IUserService userService) : ControllerBase
{
    // Временная замена авторизации: Id текущего пользователя передаётся в заголовке
    private const string CurrentUserHeader = ApiHeaders.CurrentUser;

    /// <summary>
    /// Список всех пользователей.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetAll()
        => Ok(await userService.GetAllAsync());

    /// <summary>
    /// Пользователь по Id.
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserDto>> GetById(int id)
    {
        var user = await userService.GetByIdAsync(id);
        return user is null ? NotFound() : Ok(user);
    }

    /// <summary>
    /// Зарегистрировать пользователя (нужен VkId или TelegramId).
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<UserDto>> Create([FromBody] UserEditDto dto)
    {
        var created = await userService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Изменить имя и фамилию (только свой профиль).
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UserEditDto dto,
        [FromHeader(Name = CurrentUserHeader)] int currentUserId)
        => await userService.UpdateAsync(id, currentUserId, dto) ? NoContent() : NotFound();

    /// <summary>
    /// Удалить пользователя (только свой профиль).
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        [FromHeader(Name = CurrentUserHeader)] int currentUserId)
        => await userService.DeleteAsync(id, currentUserId) ? NoContent() : NotFound();
}