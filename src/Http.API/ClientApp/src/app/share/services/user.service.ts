import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { LoginDto } from '../models/auth/login-dto.model';
import { UserFilterDto } from '../models/user/user-filter-dto.model';
import { UserAddDto } from '../models/user/user-add-dto.model';
import { UserUpdateDto } from '../models/user/user-update-dto.model';
import { AuthResult } from '../models/auth/auth-result.model';
import { UserItemDtoPageList } from '../models/user/user-item-dto-page-list.model';
import { User } from '../models/user/user.model';

/**
 * 用户账户
 */
@Injectable({ providedIn: 'root' })
export class UserService extends BaseService {
  /**
   * 登录获取Token
   * @param data LoginDto
   */
  login(data: LoginDto): Observable<AuthResult> {
    const url = `/api/User/login`;
    return this.request<AuthResult>('post', url, data);
  }

  /**
   * 筛选
   * @param data UserFilterDto
   */
  filter(data: UserFilterDto): Observable<UserItemDtoPageList> {
    const url = `/api/User/filter`;
    return this.request<UserItemDtoPageList>('post', url, data);
  }

  /**
   * 新增
   * @param data UserAddDto
   */
  add(data: UserAddDto): Observable<User> {
    const url = `/api/User`;
    return this.request<User>('post', url, data);
  }

  /**
   * 更新
   * @param id 
   * @param data UserUpdateDto
   */
  update(id: string, data: UserUpdateDto): Observable<User> {
    const url = `/api/User/${id}`;
    return this.request<User>('put', url, data);
  }

  /**
   * 详情
   * @param id 
   */
  getDetail(id: string): Observable<User> {
    const url = `/api/User/${id}`;
    return this.request<User>('get', url);
  }

  /**
   * ⚠删除
   * @param id 
   */
  delete(id: string): Observable<User> {
    const url = `/api/User/${id}`;
    return this.request<User>('delete', url);
  }

}
