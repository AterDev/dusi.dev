﻿using System.Security.Claims;
using Ater.Web.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Ater.Web.Extension.Middleware;

/// <summary>
/// 在进入验证前，对token进行额外验证
/// </summary>
public class JwtMiddleware(RequestDelegate next, CacheService redis, ILogger<JwtMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly CacheService _cache = redis;
    private readonly ILogger<JwtMiddleware> _logger = logger;

    public async Task Invoke(HttpContext context)
    {
        // 可匿名访问的放行
        Endpoint? endpoint = context.GetEndpoint();
        bool allowAnon = endpoint?.Metadata.GetMetadata<IAllowAnonymous>() != null;
        if (allowAnon)
        {
            await _next(context);
            return;
        }

        string token = context.Request.Headers[AppConst.Authorization].FirstOrDefault()?.Split(" ").Last() ?? string.Empty;
        string client = context.Request.Headers[AppConst.ClientHeader].FirstOrDefault() ?? AppConst.Web;

        if (token == null)
        {
            await SetResponseAndComplete(context, 401);
            return;
        }
        try
        {
            string id = JwtService.GetClaimValue(token, ClaimTypes.NameIdentifier);
            // TODO:策略判断
            if (id.NotEmpty())
            {
                LoginSecurityPolicy securityPolicy = _cache.GetValue<LoginSecurityPolicy>(AppConst.LoginSecurityPolicy) ?? new LoginSecurityPolicy();
                if (securityPolicy.SessionLevel == SessionLevel.OnlyOne)
                {
                    client = AppConst.AllPlatform;
                }
                string key = AppConst.LoginCachePrefix + client + id;
                string? cacheToken = _cache.GetValue<string>(key);
                if (cacheToken.IsEmpty())
                {
                    await SetResponseAndComplete(context, 401);
                    return;
                }

                if (securityPolicy.SessionLevel != SessionLevel.None && cacheToken != token)
                {
                    await SetResponseAndComplete(context, 401, "账号已在其他客户端登录");
                    return;
                }

                _cache.Cache.Refresh(key);
                await _next(context);
                return;
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
        }
    }
    private async Task SetResponseAndComplete(HttpContext context, int statusCode, string? msg = "无效的凭证")
    {
        var res = new
        {
            Title = msg,
            Detail = msg,
            Status = statusCode,
            TraceId = context.TraceIdentifier
        };

        string content = JsonSerializer.Serialize(res);
        byte[] byteArray = Encoding.UTF8.GetBytes(content);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        await context.Response.Body.WriteAsync(byteArray);
        await context.Response.CompleteAsync();
    }
}