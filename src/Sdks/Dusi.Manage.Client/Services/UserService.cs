using Share.Models.UserDtos;
namespace Dusi.Manage.Client.Services;
/// <summary>
/// 用户账户
/// </summary>
public class UserService : BaseService
{
    public UserService(HttpClient httpClient) : base(httpClient)
    {

    }
    /// <summary>
    /// 筛选
    /// </summary>
    /// <param name="data">UserFilterDto</param>
    /// <returns></returns>
    public async Task<PageList<UserItemDto>?> Filter(UserFilterDto data) {
        var url = $"/api/admin/User/filter";
        return await PostJsonAsync<PageList<UserItemDto>?>(url, data);
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="data">UserAddDto</param>
    /// <returns></returns>
    public async Task<User?> Add(UserAddDto data) {
        var url = $"/api/admin/User";
        return await PostJsonAsync<User?>(url, data);
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="id"> </param>
    /// <param name="data">UserUpdateDto</param>
    /// <returns></returns>
    public async Task<User?> Update(string id, UserUpdateDto data) {
        var url = $"/api/admin/User/{id}";
        return await PutJsonAsync<User?>(url, data);
    }

    /// <summary>
    /// 详情
    /// </summary>
    /// <param name="id"> </param>
    /// <returns></returns>
    public async Task<User?> GetDetail(string id) {
        var url = $"/api/admin/User/{id}";
        return await GetJsonAsync<User?>(url);
    }

    /// <summary>
    /// ⚠删除
    /// </summary>
    /// <param name="id"> </param>
    /// <returns></returns>
    public async Task<User?> Delete(string id) {
        var url = $"/api/admin/User/{id}";
        return await DeleteJsonAsync<User?>(url);
    }

    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="password"> </param>
    /// <returns></returns>
    public async Task<bool?> ChangeMyPassword(string? password) {
        var url = $"/api/admin/User/password?password={password}";
        return await PutJsonAsync<bool?>(url);
    }

}