export interface PagingDataListConfig {
  canCreate: boolean;
  canEdit: boolean;
  extraAction?:{
    icon:string;
    text?:string;
  };
  canDelete: boolean;
  canSelect: boolean;
  pageSize: number;
  maxSize: number;
  showDetail?: {
    icon?:string;
    routerLink: string;
  };
}
