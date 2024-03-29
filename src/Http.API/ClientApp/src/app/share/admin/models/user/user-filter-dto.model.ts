/**
 * 用户账户查询筛选
 */
export interface UserFilterDto {
  pageIndex: number;
  /**
   * 默认最大1000
   */
  pageSize: number;
  /**
   * 排序
   */
  orderBy?: any | null;
  /**
   * 用户名
   */
  userName?: string | null;
  email?: string | null;
  emailConfirmed?: boolean | null;

}
