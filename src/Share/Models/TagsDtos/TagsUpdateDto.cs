using Core.Entities.CMS;
namespace Share.Models.TagsDtos;
/// <summary>
/// 标签更新时请求结构
/// </summary>
/// <inheritdoc cref="Core.Entities.CMS.Tags"/>
public class TagsUpdateDto
{
    /// <summary>
    /// 标签名称
    /// </summary>
    [MaxLength(50)]
    public string Name { get; set; } = default!;
    /// <summary>
    /// 标签颜色
    /// </summary>
    [MaxLength(20)]
    public string? Color { get; set; }
    public User User { get; set; } = default!;
    public List<Blog>? Blogs { get; set; }
    public Guid UserId { get; set; } = default!;
    
}