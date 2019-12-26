export interface DataListConfig {
  visible: boolean,
  canCreate: boolean;
  canEdit: boolean;
  canDelete: boolean;
  canSelect: boolean;
  extraAction?:{
    icon:string;
    text?:string
  };
  showDetail?: {
    routerLink: string;
  };
}
