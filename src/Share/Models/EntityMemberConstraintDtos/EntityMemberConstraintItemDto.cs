using Core.Entities.EntityDesign;
namespace Share.Models.EntityMemberConstraintDtos;
/// <summary>
/// 属性的约束列表元素
/// </summary>
/// <inheritdoc cref="Core.Entities.EntityDesign.EntityMemberConstraint"/>
public class EntityMemberConstraintItemDto
{
    /// <summary>
    /// 字符串最长长度
    /// </summary>
    public int? MaxLength { get; set; }
    /// <summary>
    /// 字符串最短长度
    /// </summary>
    public int? MinLength { get; set; }
    /// <summary>
    /// 固定长度
    /// </summary>
    public int? Length { get; set; }
    /// <summary>
    /// 数值最小
    /// </summary>
    public int? Min { get; set; }
    /// <summary>
    /// 数值最大
    /// </summary>
    public long? Max { get; set; }
    public Guid EntityMemberId { get; set; }
    public Guid Id { get; set; }
    public DateTimeOffset CreatedTime { get; set; }
    public DateTimeOffset UpdatedTime { get; set; }
    
}
