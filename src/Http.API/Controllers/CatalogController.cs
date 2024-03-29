using Entity.CMS;
using Share.Models.CatalogDtos;
namespace Http.API.Controllers;

/// <summary>
/// 目录
/// </summary>
public class CatalogController(
    IUserContext user,
    ILogger<CatalogController> logger,
    CatalogManager manager
        ) : ClientControllerBase<CatalogManager>(manager, user, logger)
{

    /// <summary>
    /// 筛选
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpPost("filter")]
    public async Task<ActionResult<PageList<CatalogItemDto>>> FilterAsync(CatalogFilterDto filter)
    {
        return await manager.FilterAsync(filter);
    }

    /// <summary>
    /// 获取树型结构
    /// </summary>
    /// <returns></returns>
    [HttpGet("tree")]
    public async Task<ActionResult<List<Catalog>>> GetTreeAsync()
    {
        return await manager.GetTreeAsync();
    }

    /// <summary>
    /// 获取可选目录
    /// </summary>
    /// <returns></returns>
    [HttpGet("leaf")]
    public async Task<List<Catalog>> GetLeafAsync()
    {
        return await manager.GetLeafCatalogsAsync();
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Catalog>> AddAsync(CatalogAddDto form)
    {
        Catalog entity = await manager.CreateNewEntityAsync(form);
        return await manager.AddAsync(entity);
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="id"></param>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<Catalog?>> UpdateAsync([FromRoute] Guid id, CatalogUpdateDto form)
    {
        Catalog? current = await manager.GetOwnedAsync(id);
        if (current == null) return NotFound();
        return await manager.UpdateAsync(current, form);
    }

    /// <summary>
    /// 详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Catalog?>> GetDetailAsync([FromRoute] Guid id)
    {
        Catalog? res = await manager.FindAsync(id);
        return res == null ? NotFound() : res;
    }

    /// <summary>
    /// ⚠删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Catalog?>> DeleteAsync([FromRoute] Guid id)
    {
        Catalog? entity = await manager.GetOwnedAsync(id);
        if (entity == null) return NotFound();
        return await manager.DeleteAsync(entity);
    }
}