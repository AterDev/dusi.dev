using Share.Models.SystemLogsDtos;

namespace Application.Manager;

public class SystemLogsManager(
    DataAccessContext<SystemLogs> storeContext,
    IUserContext userContext, ILogger<SystemLogsManager> logger) : ManagerBase<SystemLogs, SystemLogsUpdateDto, SystemLogsFilterDto, SystemLogsItemDto>(storeContext, logger)
{

    private readonly IUserContext _userContext = userContext;

    /// <summary>
    /// 创建待添加实体
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task<SystemLogs> CreateNewEntityAsync(SystemLogsAddDto dto)
    {
        SystemLogs entity = dto.MapTo<SystemLogsAddDto, SystemLogs>();
        Command.Db.Entry(entity).Property("SystemUserId").CurrentValue = dto.SystemUserId;
        // or entity.SystemUserId = dto.SystemUserId;
        // other required props
        return Task.FromResult(entity);
    }

    public override async Task<SystemLogs> UpdateAsync(SystemLogs entity, SystemLogsUpdateDto dto)
    {
        return await base.UpdateAsync(entity, dto);
    }

    public override async Task<PageList<SystemLogsItemDto>> FilterAsync(SystemLogsFilterDto filter)
    {

        Queryable = Queryable
            .WhereNotNull(filter.ActionUserName, q => q.ActionUserName == filter.ActionUserName)
            .WhereNotNull(filter.TargetName, q => q.TargetName == filter.TargetName)
            .WhereNotNull(filter.ActionType, q => q.ActionType == filter.ActionType)
            .WhereNotNull(filter.SystemUserId, q => q.SystemUser.Id == filter.SystemUserId);

        // TODO: other filter conditions
        return await Query.FilterAsync<SystemLogsItemDto>(Queryable, filter.PageIndex, filter.PageSize, filter.OrderBy);
    }

    /// <summary>
    /// 当前用户所拥有的对象
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<SystemLogs?> GetOwnedAsync(Guid id)
    {
        IQueryable<SystemLogs> query = Command.Db.Where(q => q.Id == id);
        // TODO:获取用户所属的对象
        // query = query.Where(q => q.User.Id == _userContext.UserId);
        return await query.FirstOrDefaultAsync();
    }

}
