import { Observable } from 'rxjs';

export interface EditableModelListConfig {
  modelType: string;
  create?: string;
  update?: string;
  list: string;
  query: string;

}
