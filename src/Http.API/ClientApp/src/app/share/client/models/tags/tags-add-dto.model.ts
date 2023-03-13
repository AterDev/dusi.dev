import { User } from '../user/user.model';
import { Blog } from '../blog/blog.model';
/**
 * 标签添加时请求结构
 */
export interface TagsAddDto {
  /**
   * 标签名称
   */
  name?: string | null;
  /**
   * 标签颜色
   */
  color?: string | null;
  /**
   * 用户账户
   */
  user?: User | null;
  blogs?: Blog[] | null;
  userId: string;

}
