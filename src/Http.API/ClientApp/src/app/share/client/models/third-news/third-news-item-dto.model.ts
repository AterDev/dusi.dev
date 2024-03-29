import { NewsSource } from '../enum/news-source.model';
import { NewsType } from '../enum/news-type.model';
import { TechType } from '../enum/tech-type.model';
import { NewsStatus } from '../enum/news-status.model';
export interface ThirdNewsItemDto {
  authorName?: string | null;
  authorAvatar?: string | null;
  title: string;
  url?: string | null;
  thumbnailUrl?: string | null;
  provider?: string | null;
  datePublished?: Date | null;
  category?: string | null;
  /**
   * 概要
   */
  description?: string | null;
  identityId?: string | null;
  type?: NewsSource | null;
  newsType?: NewsType | null;
  techType?: TechType | null;
  /**
   * 第三方资讯状态
   */
  newsStatus?: NewsStatus | null;
  id: string;
  createdTime: Date;
  updatedTime: Date;

}
