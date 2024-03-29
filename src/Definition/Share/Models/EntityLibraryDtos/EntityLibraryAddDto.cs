using Entity.EntityDesign;
namespace Share.Models.EntityLibraryDtos;
/// <summary>
/// 实体库添加时请求结构
/// </summary>
/// <inheritdoc cref="Entity.EntityDesign.EntityLibrary"/>
public class EntityLibraryAddDto
{
    /// <summary>
    /// 库名称
    /// </summary>
    [MaxLength(60)]
    public required string Name { get; set; }
    /// <summary>
    /// 库描述
    /// </summary>
    [MaxLength(200)]
    public string? Description { get; set; }
    /// <summary>
    /// 是否公开
    /// </summary>
    public bool IsPublic { get; set; }
    /// <summary>
    /// 包含的实体类
    /// </summary>
    public List<EntityModel>? EntityModels { get; set; }
}
