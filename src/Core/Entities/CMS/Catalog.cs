﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Core.Entities.CMS;
/// <summary>
/// 目录
/// </summary>
[Index(nameof(Name))]
[Index(nameof(Level))]
public class Catalog : EntityBase
{
    /// <summary>
    /// 目录名称
    /// </summary>
    [MaxLength(50)]
    public required string Name { get; set; }

    /// <summary>
    /// 层级
    /// </summary>
    public short Level { get; set; } = 0;
    /// <summary>
    /// 子目录
    /// </summary>
    public List<Catalog>? Children { get; set; }

    /// <summary>
    /// 父目录
    /// </summary>
    public Catalog? Parent { get; set; }
    public List<Blog>? Blogs { get; set; }
    public required User User { get; set; }
}
