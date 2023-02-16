using Application.IManager;
using Share.Models.UserDtos;

namespace Application.Manager;

public class UserManager : DomainManagerBase<User, UserUpdateDto, UserFilterDto, UserItemDto>, IUserManager
{

    private readonly IUserContext _userContext;
    public UserManager(DataStoreContext storeContext, IUserContext userContext) : base(storeContext)
    {
        _userContext = userContext;
    }


    /// <summary>
    /// 创建待添加实体
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task<User> CreateNewEntityAsync(UserAddDto dto)
    {
        var entity = dto.MapTo<UserAddDto, User>();
        // TODO:构建实体
        return Task.FromResult(entity);
    }

    public override async Task<User> UpdateAsync(User entity, UserUpdateDto dto)
    {
        // TODO:根据实际业务更新
        return await base.UpdateAsync(entity, dto);
    }

    public override async Task<PageList<UserItemDto>> FilterAsync(UserFilterDto filter)
    {
        // TODO:根据实际业务构建筛选条件
        // if ... Queryable = ...
        return await Query.FilterAsync<UserItemDto>(Queryable, filter.PageIndex, filter.PageSize);
    }

    /// <summary>
    /// 当前用户所拥有的对象
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<User?> GetOwnedAsync(Guid id)
    {
        var query = Command.Db.Where(q => q.Id == id);
        if (!_userContext.IsAdmin)
        {
            // TODO:属于当前角色的对象
            // query = query.Where(q => q.User.Id == _userContext.UserId);
        }
        return await query.FirstOrDefaultAsync();
    }
}
