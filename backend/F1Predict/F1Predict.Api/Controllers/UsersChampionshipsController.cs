using F1Predict.Contracts.Dto;
using F1Predict.Contracts.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace F1Predict.Api.Controllers;

/// <summary>
/// Закрытые чемпионаты пользователей: создание, вступление по коду, таблица участников.
/// Текущий пользователь передаётся в заголовке X-User-Id (временно, до авторизации).
/// </summary>
[ApiController]
[Route("api/championships")]
public class UsersChampionshipsController(
    IUsersChampionshipService championshipService,
    IUserInChampionshipService membershipService) : ControllerBase
{
    /// <summary>
    /// Чемпионаты, в которых состоит или которые создал текущий пользователь.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<UsersChampionshipDto>>> GetAll(
        [FromHeader(Name = ApiHeaders.CurrentUser)] int currentUserId)
        => Ok(await championshipService.GetAllAsync(currentUserId));

    /// <summary>
    /// Чемпионат по Id (только для участников).
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UsersChampionshipDto>> GetById(
        int id,
        [FromHeader(Name = ApiHeaders.CurrentUser)] int currentUserId)
    {
        var championship = await championshipService.GetByIdAsync(id, currentUserId);
        return championship is null ? NotFound() : Ok(championship);
    }

    /// <summary>
    /// Таблица участников чемпионата по убыванию очков (только для участников).
    /// </summary>
    [HttpGet("{id:int}/standings")]
    public async Task<ActionResult<List<UserInChampionshipDto>>> GetStandings(
        int id,
        [FromHeader(Name = ApiHeaders.CurrentUser)] int currentUserId)
        => Ok(await championshipService.GetStandingsAsync(id, currentUserId));

    /// <summary>
    /// Код приглашения (только создатель чемпионата).
    /// </summary>
    [HttpGet("{id:int}/invite-code")]
    public async Task<ActionResult<InviteCodeDto>> GetInviteCode(
        int id,
        [FromHeader(Name = ApiHeaders.CurrentUser)] int currentUserId)
    {
        var code = await championshipService.GetInviteCodeAsync(id, currentUserId);
        return code is null ? NotFound() : Ok(new InviteCodeDto { InviteCode = code });
    }

    /// <summary>
    /// Создать чемпионат; создатель становится его участником.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<UsersChampionshipDto>> Create(
        [FromBody] UsersChampionshipEditDto dto,
        [FromHeader(Name = ApiHeaders.CurrentUser)] int currentUserId)
    {
        var created = await championshipService.CreateAsync(currentUserId, dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Вступить в чемпионат по коду приглашения.
    /// </summary>
    [HttpPost("join")]
    public async Task<ActionResult<UserInChampionshipDto>> Join(
        [FromBody] UserInChampionshipEditDto dto,
        [FromHeader(Name = ApiHeaders.CurrentUser)] int currentUserId)
    {
        var joined = await membershipService.JoinAsync(currentUserId, dto);
        return CreatedAtAction(nameof(GetById), new { id = joined.ChampionshipId }, joined);
    }

    /// <summary>
    /// Изменить чемпионат (только создатель).
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UsersChampionshipEditDto dto,
        [FromHeader(Name = ApiHeaders.CurrentUser)] int currentUserId)
        => await championshipService.UpdateAsync(id, currentUserId, dto) ? NoContent() : NotFound();

    /// <summary>
    /// Удалить чемпионат вместе с записями участников (только создатель).
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        [FromHeader(Name = ApiHeaders.CurrentUser)] int currentUserId)
        => await championshipService.DeleteAsync(id, currentUserId) ? NoContent() : NotFound();

    /// <summary>
    /// Выйти из чемпионата (создатель выйти не может).
    /// </summary>
    [HttpDelete("{id:int}/members/me")]
    public async Task<IActionResult> Leave(
        int id,
        [FromHeader(Name = ApiHeaders.CurrentUser)] int currentUserId)
        => await membershipService.LeaveAsync(id, currentUserId) ? NoContent() : NotFound();
}