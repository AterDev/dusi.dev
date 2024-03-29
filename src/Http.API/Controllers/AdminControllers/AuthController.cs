using Share.Models.AuthDtos;

namespace Http.API.Controllers.AdminControllers;

/// <summary>
/// 系统用户授权登录
/// </summary>
[AllowAnonymous]
[Route("api/admin/[controller]")]
[ApiExplorerSettings(GroupName = "admin")]
public class AuthController(
    IConfiguration config,
    SystemUserManager userManager
        ) : RestControllerBase
{
    private readonly IConfiguration _config = config;
    private readonly SystemUserManager userManager = userManager;

    /// <summary>
    /// 登录获取Token
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<AuthResult>> LoginAsync(LoginDto dto)
    {
        // 查询用户
        SystemUser? user = await userManager.Query.Db.Where(u => u.UserName.Equals(dto.UserName))
            .Include(u => u.SystemRoles)
            .FirstOrDefaultAsync();
        if (user == null)
        {
            return NotFound("不存在该用户");
        }

        if (HashCrypto.Validate(dto.Password, user.PasswordSalt, user.PasswordHash))
        {
            string? sign = _config.GetSection("Jwt")["Sign"];
            string? issuer = _config.GetSection("Jwt")["Issuer"];
            string? audience = _config.GetSection("Jwt")["Audience"];
            string[]? roles = user.SystemRoles?.Select(r => r.NameValue)?.ToArray();
            //var time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            //1天后过期
            if (!string.IsNullOrWhiteSpace(sign) &&
                !string.IsNullOrWhiteSpace(issuer) &&
                !string.IsNullOrWhiteSpace(audience))
            {
                JwtService jwt = new(sign, audience, issuer)
                {
                    TokenExpires = 60 * 24 * 7,
                };
                string token = jwt.GetToken(user.Id.ToString(), roles ?? ["Unknown"]);
                // 登录状态存储到Redis
                //await _redis.SetValueAsync("login" + user.Id.ToString(), true, 60 * 24 * 7);

                return new AuthResult
                {
                    Id = user.Id,
                    Roles = roles ?? ["Unknown"],
                    Token = token,
                    Username = user.UserName
                };
            }
            else
            {
                throw new Exception("缺少Jwt配置内容");
            }
        }
        else
        {
            return Problem("用户名或密码错误", title: "");
        }
    }

    /// <summary>
    /// 退出 
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<bool>> LogoutAsync([FromRoute] Guid id)
    {
        //var user = await _store.FindAsync(id);
        //if (user == null) return NotFound();
        // 清除redis登录状态
        //await _redis.Cache.RemoveAsync(id.ToString());
        return await Task.FromResult(true);
    }

}
