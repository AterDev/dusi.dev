using Share.Models.ThirdNewsDtos;
namespace Http.API.Controllers;

/// <summary>
/// 资讯
/// </summary>
public class ThirdNewsController : ClientControllerBase<IThirdNewsManager>
{
    public ThirdNewsController(
        IUserContext user,
        ILogger<ThirdNewsController> logger,
        IThirdNewsManager manager
        ) : base(manager, user, logger)
    {
    }

    /// <summary>
    /// 筛选
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpPost("filter")]
    public async Task<ActionResult<PageList<ThirdNewsItemDto>>> FilterAsync(ThirdNewsFilterDto filter)
    {
        filter.NewsStatus = NewsStatus.Public;
        return await manager.FilterAsync(filter);
    }


    /// <summary>
    /// 详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ThirdNews?>> GetDetailAsync([FromRoute] Guid id)
    {
        var res = await manager.FindAsync(id);
        return (res == null) ? NotFound() : res;
    }

}