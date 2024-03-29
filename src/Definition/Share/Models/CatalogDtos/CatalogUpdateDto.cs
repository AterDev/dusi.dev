using Entity.CMS;
namespace Share.Models.CatalogDtos;
/// <summary>
/// 目录更新时请求结构
/// </summary>
/// <inheritdoc cref="Entity.CMS.Catalog"/>
public class CatalogUpdateDto
{
    /// <summary>
    /// 目录名称
    /// </summary>
    [MaxLength(50)]
    public string Name { get; set; } = default!;
}
