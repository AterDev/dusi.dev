using Entity.CMS;
using Share.Models.ThirdNewsDtos;
namespace Http.API.Controllers;

/// <summary>
/// 资讯
/// </summary>
public class ThirdNewsController(
    IUserContext user,
    ILogger<ThirdNewsController> logger,
    ThirdNewsManager manager
        ) : ClientControllerBase<ThirdNewsManager>(manager, user, logger)
{

    /// <summary>
    /// 筛选
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpPost("filter")]
    [AllowAnonymous]
    public async Task<ActionResult<PageList<ThirdNewsItemDto>>> FilterAsync(ThirdNewsFilterDto filter)
    {
        filter.NewsStatus = NewsStatus.Public;
        filter.StartDate = DateTimeOffset.UtcNow.AddDays(-10);
        filter.EndDate = DateTimeOffset.UtcNow;
        filter.IsClassified = true;
        return await manager.FilterAsync(filter);
    }


    /// <summary>
    /// 获取枚举选项
    /// </summary>
    /// <returns></returns>
    [HttpGet("enumOptions")]
    [AllowAnonymous]
    public ActionResult<ThirdNewsOptionsDto> GetEnumOptions()
    {
        ThirdNewsOptionsDto res = manager.GetEnumOptions();
        return Ok(res);
    }


    /// <summary>
    /// 详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ThirdNews?>> GetDetailAsync([FromRoute] Guid id)
    {
        ThirdNews? res = await manager.FindAsync(id);
        return (res == null) ? NotFound() : res;
    }

}