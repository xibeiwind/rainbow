export interface PagingDataListConfig {
  canCreate: boolean;
  canEdit: boolean;
  canDelete: boolean;
  canSelect: boolean;
  pageSize: number;
  maxSize: number;
  detailLink?: string;
}
