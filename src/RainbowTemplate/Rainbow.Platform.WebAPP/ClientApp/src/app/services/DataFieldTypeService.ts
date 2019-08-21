
import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { stringify } from 'querystring';
import { getHttpOptions } from './httpOptions';

@Injectable()
export class DataFieldTypeService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string){ }

  public GetAsync(id: String)
    : Observable<any> {
    return this.http.get<any>
      (`${this.baseUrl}api/DataFieldType?${stringify(id)}`, getHttpOptions());
  }

  public GetListAsync()
    : Observable<any> {
    return this.http.get<any>
      (`${this.baseUrl}api/DataFieldType/List`, getHttpOptions());
  }

  public QueryAsync(option: Rainbow.ViewModels.DataFieldTypes.QueryDataFieldTypeVM)
    : Observable<any> {
    return this.http.get<any>
      (`${this.baseUrl}api/DataFieldType/Query?${stringify(option)}`, getHttpOptions());
  }

  public CreateAsync(vm: Rainbow.ViewModels.DataFieldTypes.CreateDataFieldTypeVM)
    : Observable<any> {
    return this.http.post<any>
      (`${this.baseUrl}api/DataFieldType/Create`, vm, getHttpOptions());
  }

  public UpdateAsync(vm: Rainbow.ViewModels.DataFieldTypes.UpdateDataFieldTypeVM)
    : Observable<any> {
    return this.http.put<any>
      (`${this.baseUrl}api/DataFieldType/Update`, vm, getHttpOptions());
  }

  public DeleteAsync(vm: Rainbow.ViewModels.DataFieldTypes.DeleteDataFieldTypeVM)
    : Observable<any> {
    return this.http.delete<any>
      (`${this.baseUrl}api/DataFieldType/Delete?${stringify(vm)}`, getHttpOptions());
  }

}

