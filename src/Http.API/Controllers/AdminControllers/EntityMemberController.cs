using Entity.EntityDesign;
using Share.Models.EntityMemberDtos;
namespace Http.API.Controllers.AdminControllers;

/// <summary>
/// 实体属性
/// </summary>
public class EntityMemberController(
    IUserContext user,
    ILogger<EntityMemberController> logger,
    EntityMemberManager manager
        ) : RestControllerBase<EntityMemberManager>(manager, user, logger)
{

    /// <summary>
    /// 筛选
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpPost("filter")]
    public async Task<ActionResult<PageList<EntityMemberItemDto>>> FilterAsync(EntityMemberFilterDto filter)
    {
        return await manager.FilterAsync(filter);
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<EntityMember>> AddAsync(EntityMemberAddDto form)
    {
        EntityMember entity = form.MapTo<EntityMemberAddDto, EntityMember>();
        return await manager.AddAsync(entity);
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="id"></param>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<EntityMember?>> UpdateAsync([FromRoute] Guid id, EntityMemberUpdateDto form)
    {
        EntityMember? current = await manager.GetCurrentAsync(id);
        if (current == null) return NotFound();
        return await manager.UpdateAsync(current, form);
    }

    /// <summary>
    /// 详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<EntityMember?>> GetDetailAsync([FromRoute] Guid id)
    {
        EntityMember? res = await manager.FindAsync(id);
        return res == null ? NotFound() : res;
    }

    /// <summary>
    /// ⚠删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // [ApiExplorerSettings(IgnoreApi = true)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<EntityMember?>> DeleteAsync([FromRoute] Guid id)
    {
        EntityMember? entity = await manager.GetCurrentAsync(id);
        if (entity == null) return NotFound();
        return await manager.DeleteAsync(entity);
    }
}