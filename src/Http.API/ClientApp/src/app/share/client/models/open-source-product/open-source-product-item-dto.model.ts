import { User } from '../user/user.model';
/**
 * 开源作品列表元素
 */
export interface OpenSourceProductItemDto {
  /**
   * 标题
   */
  title: string;
  /**
   * project url address
   */
  projectUrl: string;
  /**
   * 描述
   */
  description: string;
  /**
   * 缩略图
   */
  thumbnail?: string | null;
  /**
   * 作者
   */
  authors?: string | null;
  /**
   * 标签
   */
  tags?: string | null;
  id: string;
  createdTime: Date;
  updatedTime: Date;
  /**
   * 用户账户
   */
  user?: User | null;

}
