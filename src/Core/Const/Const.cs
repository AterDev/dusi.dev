﻿namespace Core.Const;
/// <summary>
/// 静态变量
/// </summary>
public static class Const
{
    /// <summary>
    /// 管理员policy
    /// </summary>
    public const string Admin = "Admin";
    public const string AdminUser = "AdminUser";
    /// <summary>
    /// 普通用户policy
    /// </summary>
    public const string User = "User";

    /// <summary>
    /// 版本
    /// </summary>
    public const string Version = "Version";

    /// <summary>
    /// blog浏览 pub主题
    /// </summary>
    public const string PubBlogView = "PubBlogView";
}

/// <summary>
/// 错误信息
/// </summary>
public static class ErrorMsg
{
    /// <summary>
    /// 未找到该用户
    /// </summary>
    public const string NotFoundUser = "未找到该用户!";
    /// <summary>
    /// 未找到的资源
    /// </summary>
    public const string NotFoundResource = "未找到的资源!";
    /// <summary>
    /// 未找到实体库
    /// </summary>
    public const string NotFoundEntityLib = "未找到实体库!";

}
