import { Catalog } from '../catalog/catalog.model';
import { Blog } from '../blog/blog.model';
import { User } from '../user/user.model';
/**
 * 目录添加时请求结构
 */
export interface CatalogAddDto {
  /**
   * 目录名称
   */
  name?: string | null;
  /**
   * 层级
   */
  level: number;
  /**
   * 子目录
   */
  children?: Catalog[] | null;
  /**
   * 目录
   */
  parent?: Catalog | null;
  parentId?: string | null;
  blogs?: Blog[] | null;
  /**
   * 用户账户
   */
  user?: User | null;
  userId: string;

}