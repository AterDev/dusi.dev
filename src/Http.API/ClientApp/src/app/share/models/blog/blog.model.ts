import { LanguageType } from '../enum/language-type.model';
import { User } from '../user/user.model';
import { Catalog } from '../catalog/catalog.model';
import { Tags } from '../tags/tags.model';
/**
 * 博客
 */
export interface Blog {
  id: string;
  createdTime: Date;
  updatedTime: Date;
  /**
   * 软删除
   */
  isDeleted: boolean;
  /**
   * 标题
   */
  title?: string | null;
  /**
   * 描述
   */
  description?: string | null;
  /**
   * 内容
   */
  content?: string | null;
  /**
   * 作者
   */
  authors?: string | null;
  /**
   * 标题
   */
  translateTitle?: string | null;
  /**
   * 翻译内容
   */
  translateContent?: string | null;
  languageType?: LanguageType | null;
  /**
   * 是否公开
   */
  isPublic: boolean;
  /**
   * 是否原创
   */
  isOriginal: boolean;
  /**
   * 用户账户
   */
  user?: User | null;
  /**
   * 目录
   */
  catalog?: Catalog | null;
  tags?: Tags[] | null;

}