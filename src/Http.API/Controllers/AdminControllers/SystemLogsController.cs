using Entity;
using Share.Models.SystemLogsDtos;
namespace Http.API.Controllers.AdminControllers;

/// <summary>
/// 系统日志
/// </summary>
public class SystemLogsController(
    IUserContext user,
    ILogger<SystemLogsController> logger,
    SystemLogsManager manager,
    SystemUserManager systemUserManager
        ) : RestControllerBase<SystemLogsManager>(manager, user, logger)
{
    private readonly SystemUserManager _systemUserManager = systemUserManager;

    /// <summary>
    /// 筛选
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpPost("filter")]
    public async Task<ActionResult<PageList<SystemLogsItemDto>>> FilterAsync(SystemLogsFilterDto filter)
    {
        return await manager.FilterAsync(filter);
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<SystemLogs>> AddAsync(SystemLogsAddDto dto)
    {
        if (!await _systemUserManager.ExistAsync(dto.SystemUserId))
            return NotFound("不存在的");
        SystemLogs entity = await manager.CreateNewEntityAsync(dto);
        return await manager.AddAsync(entity);
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<SystemLogs?>> UpdateAsync([FromRoute] Guid id, SystemLogsUpdateDto dto)
    {
        SystemLogs? current = await manager.GetOwnedAsync(id);
        if (current == null) return NotFound(ErrorMsg.NotFoundResource);
        if (current.SystemUser.Id != dto.SystemUserId)
        {
            SystemUser? systemUser = await _systemUserManager.GetCurrentAsync(dto.SystemUserId);
            if (systemUser == null) return NotFound("不存在的");
            current.SystemUser = systemUser;
        }
        return await manager.UpdateAsync(current, dto);
    }

    /// <summary>
    /// 详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<SystemLogs?>> GetDetailAsync([FromRoute] Guid id)
    {
        SystemLogs? res = await manager.FindAsync(id);
        return (res == null) ? NotFound() : res;
    }

    /// <summary>
    /// ⚠删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // [ApiExplorerSettings(IgnoreApi = true)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<SystemLogs?>> DeleteAsync([FromRoute] Guid id)
    {
        // TODO:实现删除逻辑,注意删除权限
        SystemLogs? entity = await manager.GetOwnedAsync(id);
        if (entity == null) return NotFound();
        return Forbid();
        // return await manager.DeleteAsync(entity);
    }
}