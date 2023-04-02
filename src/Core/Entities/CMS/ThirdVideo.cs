﻿using Microsoft.EntityFrameworkCore;

namespace Core.Entities.CMS;
/// <summary>
/// 三方视频
/// </summary>
[Index(nameof(Title))]
[Index(nameof(Identity))]
[Index(nameof(CreatedTime))]
public class ThirdVideo : EntityBase
{
    [MaxLength(120)]
    public required string Title { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    [MaxLength(200)]
    public string? ThumbnailUrl { get; set; }

    [MaxLength(200)]
    public required string OriginalUrl { get; set; }

    [MaxLength(50)]
    public string? Source { get; set; }

    /// <summary>
    /// 唯一标识 
    /// </summary>
    [MaxLength(100)]
    public required string Identity { get; set; }
}
