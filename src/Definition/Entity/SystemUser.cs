namespace Entity;

public class SystemUser : IEntityBase
{
	/// <summary>
	/// 用户名
	/// </summary>
	[MaxLength(30)]
	public required string UserName { get; set; }
	/// <summary>
	/// 真实姓名
	/// </summary>
	[MaxLength(30)]
	public string? RealName { get; set; }
	[MaxLength(100)]
	public string? Email { get; set; } = null!;
	public bool EmailConfirmed { get; set; } = false;
	[MaxLength(100)]
	public string PasswordHash { get; set; } = default!;
	[MaxLength(60)]
	public string PasswordSalt { get; set; } = default!;
	[MaxLength(20)]
	public string? PhoneNumber { get; set; }
	public bool PhoneNumberConfirmed { get; set; } = false;
	public bool TwoFactorEnabled { get; set; } = false;
	public DateTimeOffset? LockoutEnd { get; set; }
	public bool LockoutEnabled { get; set; } = false;
	public int AccessFailedCount { get; set; } = 0;
	/// <summary>
	/// 最后登录时间
	/// </summary>
	public DateTimeOffset? LastLoginTime { get; set; }
	/// <summary>
	/// 密码重试次数
	/// </summary>
	public int RetryCount { get; set; } = 0;
	/// <summary>
	/// 头像url
	/// </summary>
	[MaxLength(200)]
	public string? Avatar { get; set; }

	public ICollection<SystemRole>? SystemRoles { get; set; }

	public List<SystemLogs>? SystemLogs { get; set; }

	/// <summary>
	/// 性别
	/// </summary>
	public Sex Sex { get; set; } = Sex.Male;
    public Guid Id { get; set; }
    public DateTimeOffset CreatedTime { get; set; }
    public DateTimeOffset UpdatedTime { get; set; }
    public bool IsDeleted { get; set; }
}
/// <summary>
/// 性别
/// </summary>
public enum Sex
{
	Male,
	Female,
	Else
}