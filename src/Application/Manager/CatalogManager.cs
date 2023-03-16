using Application.IManager;
using Share.Models.CatalogDtos;

namespace Application.Manager;

/// <summary>
/// 目录管理
/// </summary>
public class CatalogManager : DomainManagerBase<Catalog, CatalogUpdateDto, CatalogFilterDto, CatalogItemDto>, ICatalogManager
{

    private readonly IUserContext _userContext;
    public CatalogManager(DataStoreContext storeContext, IUserContext userContext) : base(storeContext)
    {
        _userContext = userContext;
    }

    /// <summary>
    /// 创建待添加实体
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<Catalog> CreateNewEntityAsync(CatalogAddDto dto)
    {
        var entity = dto.MapTo<CatalogAddDto, Catalog>();
        var user = await _userContext.GetUserAsync();
        entity.User = user!;
        if (dto.ParentId != null)
        {
            var parent = await GetCurrentAsync(dto.ParentId.Value);
            if (parent != null)
            {
                entity.ParentId = parent.Id;
                entity.Parent = parent;
                entity.Level = (short)(parent.Level + 1);
            }
            else
            {
                entity.Level = 0;
            }
        }
        return entity;
    }

    public override async Task<Catalog> UpdateAsync(Catalog entity, CatalogUpdateDto dto)
    {
        return await base.UpdateAsync(entity, dto);
    }

    public override async Task<PageList<CatalogItemDto>> FilterAsync(CatalogFilterDto filter)
    {
        // TODO:根据实际业务构建筛选条件
        // if ... Queryable = ...
        return await Query.FilterAsync<CatalogItemDto>(Queryable, filter.PageIndex, filter.PageSize);
    }


    /// <summary>
    /// 获取树型目录
    /// </summary>
    /// <returns></returns>
    public async Task<List<Catalog>> GetTreeAsync()
    {
        var data = await ListAsync(null);
        var tree = data.BuildTree();
        return tree;
    }

    /// <summary>
    /// 获取叶结点目录
    /// </summary>
    /// <returns></returns>
    public async Task<Dictionary<string, List<Catalog>>> GetLeafCatalogsAsync()
    {
        var parentIds = await Query.Db
            .Select(s => s.ParentId)
            .ToListAsync();

        var source = await Query.Db.Where(c => !parentIds.Contains(c.Id))
            .Include(c => c.Parent)
            .ToListAsync();

        var group = source.GroupBy(c => c.Parent!.Id)
            .ToDictionary(k => k.First().Parent!.Name, v => v.ToList());

        return group;

    }

    /// <summary>
    /// 当前用户所拥有的对象
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Catalog?> GetOwnedAsync(Guid id)
    {
        var query = Command.Db.Where(q => q.Id == id);
        // 属于当前角色的对象
        query = query.Where(q => q.User.Id == _userContext.UserId);
        return await query.FirstOrDefaultAsync();
    }

}
