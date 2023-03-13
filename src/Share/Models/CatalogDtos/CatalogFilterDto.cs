using Core.Entities.CMS;
namespace Share.Models.CatalogDtos;
/// <summary>
/// 目录查询筛选
/// </summary>
/// <inheritdoc cref="Core.Entities.CMS.Catalog"/>
public class CatalogFilterDto : FilterBase
{
    /// <summary>
    /// 目录名称
    /// </summary>
    [MaxLength(50)]
    public string? Name { get; set; }
}
