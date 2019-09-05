import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { stringify } from 'querystring';
import { getHttpOptions } from './httpOptions';

@Injectable()
export class DataFieldTypeService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }


  /**
   * 创建DataFieldType
   */
  public CreateAsync(vm: Rainbow.ViewModels.DataFieldTypes.CreateDataFieldTypeVM)
    : Observable<Yunyong.Core.AsyncTaskTResult<string>> {
    return this.http.post<Yunyong.Core.AsyncTaskTResult<string>>
      (`${this.baseUrl}Create`,{ ...vm }, { ...getHttpOptions() });
  }

  /**
   * 更新DataFieldType
   */
  public UpdateAsync(vm: Rainbow.ViewModels.DataFieldTypes.UpdateDataFieldTypeVM)
    : Observable<Yunyong.Core.AsyncTaskTResult<string>> {
    return this.http.put<Yunyong.Core.AsyncTaskTResult<string>>
      (`${this.baseUrl}Update`,{ ...vm }, { ...getHttpOptions() });
  }

  /**
   * 查询DataFieldType列表（分页）
   */
  public QueryAsync(option: Rainbow.ViewModels.DataFieldTypes.QueryDataFieldTypeVM)
    : Observable<Yunyong.Core.PagingList<Rainbow.ViewModels.DataFieldTypes.DataFieldTypeVM>> {
    return this.http.get<Yunyong.Core.PagingList<Rainbow.ViewModels.DataFieldTypes.DataFieldTypeVM>>
      (`${this.baseUrl}Query`, { params: {option: option}, ...getHttpOptions() });
  }

  /**
   * 获取DataFieldType
   */
  public GetAsync(id: string)
    : Observable<Rainbow.ViewModels.DataFieldTypes.DataFieldTypeVM> {
    return this.http.get<Rainbow.ViewModels.DataFieldTypes.DataFieldTypeVM>
      (`${this.baseUrl}`, { params: {id: id}, ...getHttpOptions() });
  }

  /**
   * 获取DataFieldType列表
   */
  public GetListAsync()
    : Observable<Rainbow.ViewModels.DataFieldTypes.DataFieldTypeVM[]> {
    return this.http.get<Rainbow.ViewModels.DataFieldTypes.DataFieldTypeVM[]>
      (`${this.baseUrl}List`, { ...getHttpOptions() });
  }

  /**
   * 删除DataFieldType
   */
  public  DeleteAsync(vm: Rainbow.ViewModels.DataFieldTypes.DeleteDataFieldTypeVM)
    : Observable<Yunyong.Core.AsyncTaskResult> {
    return this.http.delete<Yunyong.Core.AsyncTaskResult>
      (`${this.baseUrl}Delete`, { params: { ...vm }, ...getHttpOptions() });
  }
}
