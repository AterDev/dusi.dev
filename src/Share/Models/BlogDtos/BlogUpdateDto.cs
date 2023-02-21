using Core.Entities.CMS;
namespace Share.Models.BlogDtos;
/// <summary>
/// 博客更新时请求结构
/// </summary>
/// <inheritdoc cref="Core.Entities.CMS.Blog"/>
public class BlogUpdateDto
{
    /// <summary>
    /// 标题
    /// </summary>
    public string? TranslateTitle { get; set; }
    /// <summary>
    /// 翻译内容
    /// </summary>
    [MaxLength(12000)]
    public string? TranslateContent { get; set; }
    /// <summary>
    /// 语言类型
    /// </summary>
    public LanguageType? LanguageType { get; set; }
    /// <summary>
    /// 标题
    /// </summary>
    [MaxLength(100)]
    public string? Title { get; set; }
    /// <summary>
    /// 描述
    /// </summary>
    [MaxLength(300)]
    public string? Description { get; set; }
    /// <summary>
    /// 内容
    /// </summary>
    [MaxLength(10000)]
    public string? Content { get; set; }
    /// <summary>
    /// 是否公开
    /// </summary>
    public bool? IsPublic { get; set; }
    /// <summary>
    /// 作者
    /// </summary>
    [MaxLength(200)]
    public string? Authors { get; set; }
    /// <summary>
    /// 所属目录
    /// </summary>
    public Guid CatalogId { get; set; } = default!;
    /// <summary>
    /// 标签
    /// </summary>
    public List<string>? Tags { get; set; }
    
}
