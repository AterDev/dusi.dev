using System.Security.Claims;
using Share.Models.AuthDtos;
using Share.Models.UserDtos;
namespace Http.API.Controllers;

/// <summary>
/// 用户账户
/// </summary>
public class UserController(
    IUserContext user,
    ILogger<UserController> logger,
    UserManager manager,
    IConfiguration config,
    SystemUserManager systemUserManager) : ClientControllerBase<UserManager>(manager, user, logger)
{
    private readonly IConfiguration _config = config;
    private readonly SystemUserManager systemUserManager = systemUserManager;

    /// <summary>
    /// 登录获取Token
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResult>> LoginAsync(LoginDto dto)
    {
        // 查询用户
        User? user = await manager.Query.Db.Where(u => u.UserName.Equals(dto.UserName))
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

                string[] roles = new string[] { AppConst.User, user.UserType.ToString() };
                jwt.Claims =
                [
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email??""),
                ];
                string token = jwt.GetToken(user.Id.ToString(), roles);

                return new AuthResult
                {
                    Id = user.Id,
                    Roles = roles,
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
            return Problem("用户名或密码错误");
        }
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<User>> SignUpAsync(UserAddDto form)
    {
        if (await manager.FindAsync<User>(u => u.UserName == form.UserName) != null)
        {
            return Conflict("该用户名已被使用");
        }
        User entity = await manager.CreateNewEntityAsync(form);
        return await manager.AddAsync(entity);
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="id"></param>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<User?>> UpdateAsync([FromRoute] Guid id, UserUpdateDto form)
    {
        User? current = await manager.GetOwnedAsync(id);
        if (current == null) return NotFound();
        return await manager.UpdateAsync(current, form);
    }

    /// <summary>
    /// 详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetDetailAsync([FromRoute] Guid id)
    {
        User? user = _user.IsAdmin
            ? await systemUserManager.GetInfoAsUserAsync(id)
            : await manager.FindAsync(id);
        return user == null ? NotFound() : user;
    }

    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    [HttpPut("password")]
    public async Task<ActionResult<bool>> ChangeMyPassword(string password)
    {
        User? user = await manager.GetCurrentAsync(_user.UserId);
        if (user == null) return NotFound("未找到该用户");
        return await manager.ChangePasswordAsync(user, password);
    }

    /// <summary>
    /// ⚠删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<User?>> DeleteAsync([FromRoute] Guid id)
    {
        User? entity = await manager.GetOwnedAsync(id);
        if (entity == null) return Forbid("无法删除当前用户");
        return await manager.DeleteAsync(entity, false);
    }
}