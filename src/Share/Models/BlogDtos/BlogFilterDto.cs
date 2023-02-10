using Core.Entities.CMS;
namespace Share.Models.BlogDtos;

/// <inheritdoc cref="Core.Entities.CMS.Blog"/>
public class BlogFilterDto : FilterBase
{
    /// <summary>
    /// 标题
    /// </summary>
    public string? TranslateTitle { get; set; }
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
    /// 作者
    /// </summary>
    [MaxLength(200)]
    public string? Authors { get; set; }
    
}
