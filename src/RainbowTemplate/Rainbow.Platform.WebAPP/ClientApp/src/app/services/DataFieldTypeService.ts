import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { stringify } from 'querystring';
import { getHttpOptions } from './httpOptions';

@Injectable()
export class DataFieldTypeService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  // 获取显示DataFieldType
  public GetAsync(id: String)
    : Observable<any> {
    return this.http.get<any>
      (`${this.baseUrl}api/DataFieldType?${stringify(id)}`, getHttpOptions());
  }
  // 获取显示DataFieldType列表
  public GetListAsync()
    : Observable<any> {
    return this.http.get<any>
      (`${this.baseUrl}api/DataFieldType/List`, getHttpOptions());
  }
  // 查询DataFieldType列表（分页）
  public QueryAsync(option: Rainbow.ViewModels.DataFieldTypes.QueryDataFieldTypeVM)
    : Observable<any> {
    return this.http.get<any>
      (`${this.baseUrl}api/DataFieldType/Query?${stringify(option)}`, getHttpOptions());
  }
  // 创建DataFieldType
  public CreateAsync(vm: Rainbow.ViewModels.DataFieldTypes.CreateDataFieldTypeVM)
    : Observable<any> {
    return this.http.post<any>
      (`${this.baseUrl}api/DataFieldType/Create`, vm, getHttpOptions());
  }
  // 更新DataFieldType
  public UpdateAsync(vm: Rainbow.ViewModels.DataFieldTypes.UpdateDataFieldTypeVM)
    : Observable<any> {
    return this.http.put<any>
      (`${this.baseUrl}api/DataFieldType/Update`, vm, getHttpOptions());
  }
  // 删除DataFieldType
  public DeleteAsync(vm: Rainbow.ViewModels.DataFieldTypes.DeleteDataFieldTypeVM)
    : Observable<any> {
    return this.http.delete<any>
      (`${this.baseUrl}api/DataFieldType/Delete?${stringify(vm)}`, getHttpOptions());
  }
}