using System.Xml.Linq;

namespace TaskService.Implement.NewsCollector.RssFeeds;

/// <summary>
/// 基础feed
/// </summary>
public class BaseFeed(ILogger logger, string provider)
{
    /// <summary>
    /// 链接
    /// </summary>
    public string[] Urls { get; set; } = [];
    /// <summary>
    /// 作者过滤
    /// </summary>
    protected string[] Authorfilter { get; set; } = [];
    /// <summary>
    /// 内容html标签过滤
    /// </summary>
    protected string[] HtmlTagFilter { get; set; } = ["<h1>", "<h2>", "<h3>", "<h4>", "<h5>", "<p></p>"];
    /// <summary>
    /// xml root名称
    /// </summary>
    protected string RootName { get; set; } = "channel";
    protected string Provider { get; set; } = provider;
    /// <summary>
    /// xml item 名称
    /// </summary>
    protected string ItemName { get; set; } = "item";
    public XName Creator { get; set; } = XName.Get("creator", "http://purl.org/dc/elements/1.1/");
    public XName Content { get; set; } = XName.Get("encoded", "http://purl.org/rss/1.0/modules/content/");
    public XName PubDate { get; set; } = "pubDate";
    public XName Category { get; set; } = "category";
    public XName Title { get; set; } = "title";
    public XName Description { get; set; } = "description";
    public XName Link { get; set; } = "link";
    /// <summary>
    /// 是否包含内容
    /// </summary>
    public bool HasContent { get; set; } = true;
    protected XDocument? xmlDoc;
    protected HttpClient httpClient = new();

    private readonly ILogger _logger = logger;

    /// <summary>
    /// 解析返回
    /// </summary>
    /// <param name="number">数量</param>
    /// <returns></returns>
    public virtual async Task<List<Rss>> GetBlogsAsync(int number = 3)
    {
        List<Rss> result = [];
        foreach (string url in Urls)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Clear();
                string xmlString = await httpClient.GetStringAsync(url);
                if (!string.IsNullOrEmpty(xmlString))
                {
                    xmlDoc = XDocument.Parse(xmlString);
                    IEnumerable<XElement>? xmlList = xmlDoc?.Root?.Element(RootName)?.Elements(ItemName);
                    List<Rss>? blogs = xmlList?.Select(x =>
                        {
                            DateTimeOffset createTime = DateTimeOffset.UtcNow;
                            string? createTimeString = x.Element(PubDate)?.Value;
                            if (!string.IsNullOrEmpty(createTimeString))
                            {
                                DateTimeOffset.TryParse(createTimeString, out createTime);
                            }

                            string? description = x.Element(Description)?.Value;
                            // 去除html标签
                            //description = Regex.Replace(description, "<.*?>", String.Empty);

                            if (!string.IsNullOrEmpty(description))
                            {
                                if (description.Length > 5000)
                                {
                                    description = description[..5000];
                                }
                            }
                            string? content = x.Element(Content)?.Value;
                            if (!string.IsNullOrEmpty(content))
                            {
                                content = content.Replace("<pre", "<pre class=\"notranslate\"");
                            }

                            return new Rss
                            {
                                Title = x.Element(Title)?.Value ?? "",
                                Content = content,
                                Description = description ?? "",
                                CreateTime = createTime,
                                Author = x.Element(Creator)?.Value,
                                Link = x.Element(Link)?.Value,
                                Categories = GetCategories(x),
                                LastUpdateTime = createTime,
                                ThumbUrl = GetThumb(x.Element("guid")),
                                Provider = Provider
                            };
                        })
                        .Take(number)
                        .ToList();
                    if (blogs != null) result.AddRange(blogs);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + e.InnerException + e.StackTrace);
            }
        }

        // 处理没有内容的博客
        result.Where(r => string.IsNullOrEmpty(r.Content)).ToList()
            .ForEach(item =>
            {
                item.Content = GetContent(item.Link ?? "");
            });

        return result;
    }

    /// <summary>
    /// 获取内容
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    protected virtual string GetContent(string url) => "";

    /// <summary>
    /// 获取标签目录
    /// </summary>
    /// <returns></returns>
    protected virtual string GetCategories(XElement element)
    {
        string[]? categories = element.Elements()
            .Where(e => e.Name.Equals(Category))?
            .Select(s => s.Value)
            .ToArray();
        if (categories == null) return string.Empty;
        return string.Join(";", categories);
    }

    /// <summary>
    /// 获取缩略图
    /// </summary>
    /// <returns></returns>
    protected virtual string? GetThumb(XElement? x) => "";
    /// <summary>
    /// 是否包含
    /// </summary>
    /// <param name="strArray"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    protected bool IsContainKey(string[] strArray, string key)
    {
        if (strArray == null || strArray.Length < 1 || string.IsNullOrEmpty(key))
        {
            return true;
        }
        foreach (string item in strArray)
        {
            if (key.Contains(item))
            {
                return true;
            }
        }
        return false;
    }
}
