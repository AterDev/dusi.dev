import { ActionType } from '../enum/action-type.model';
/**
 * 系统日志列表元素
 */
export interface SystemLogsItemDto {
  /**
   * 操作人名称
   */
  actionUserName: string;
  /**
   * 操作对象名称
   */
  targetName: string;
  /**
   * 操作路由
   */
  route: string;
  actionType?: ActionType | null;
  /**
   * 描述
   */
  description?: string | null;
  id: string;
  createdTime: Date;
  updatedTime: Date;

}
