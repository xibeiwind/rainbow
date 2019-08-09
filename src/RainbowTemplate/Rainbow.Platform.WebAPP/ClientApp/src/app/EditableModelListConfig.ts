import { Observable } from 'rxjs';

export interface EditableModelListConfig {
  modelType: string;
  create?: string;
  update?: string;
  detail: string;
  query: string;

}
