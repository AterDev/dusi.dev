using Share.Models.SystemRoleDtos;
namespace Http.API.Controllers.AdminControllers;

/// <summary>
/// 角色表
/// </summary>
public class SystemRoleController(
    IUserContext user,
    ILogger<SystemRoleController> logger,
    SystemRoleManager manager
        ) : RestControllerBase<SystemRoleManager>(manager, user, logger)
{

    /// <summary>
    /// 筛选
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpPost("filter")]
    public async Task<ActionResult<PageList<SystemRoleItemDto>>> FilterAsync(SystemRoleFilterDto filter)
    {
        return await manager.FilterAsync(filter);
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<SystemRole>> AddAsync(SystemRoleAddDto form)
    {
        SystemRole entity = form.MapTo<SystemRoleAddDto, SystemRole>();
        return await manager.AddAsync(entity);
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="id"></param>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<SystemRole?>> UpdateAsync([FromRoute] Guid id, SystemRoleUpdateDto form)
    {
        SystemRole? current = await manager.GetCurrentAsync(id);
        if (current == null) return NotFound();
        return await manager.UpdateAsync(current, form);
    }

    /// <summary>
    /// 详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<SystemRole?>> GetDetailAsync([FromRoute] Guid id)
    {
        SystemRole? res = await manager.FindAsync(id);
        if (res == null) return NotFound();
        return res;
    }

    /// <summary>
    /// ⚠删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // [ApiExplorerSettings(IgnoreApi = true)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<SystemRole?>> DeleteAsync([FromRoute] Guid id)
    {
        SystemRole? entity = await manager.GetCurrentAsync(id);
        if (entity == null) return NotFound();
        return await manager.DeleteAsync(entity);
    }
}