import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { stringify } from 'querystring';
import { getHttpOptions } from './httpOptions';

@Injectable()
export class ClientModuleService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }


  /**
   * 创建客户端模块
   */
  public CreateAsync(vm: Rainbow.ViewModels.ClientModules.CreateClientModuleVM)
    : Observable<Yunyong.Core.AsyncTaskTResult<string>> {
    return this.http.post<Yunyong.Core.AsyncTaskTResult<string>>
      (`${this.baseUrl}api/ClientModule/Create`, vm, getHttpOptions());
  }

  /**
   * 更新客户端模块
   */
  public UpdateAsync(vm: Rainbow.ViewModels.ClientModules.UpdateClientModuleVM)
    : Observable<Yunyong.Core.AsyncTaskTResult<string>> {
    return this.http.put<Yunyong.Core.AsyncTaskTResult<string>>
      (`${this.baseUrl}api/ClientModule/Update`, vm, getHttpOptions());
  }

  /**
   * 查询客户端模块列表（分页）
   */
  public QueryAsync(option: Rainbow.ViewModels.ClientModules.QueryClientModuleVM)
    : Observable<Yunyong.Core.PagingList<Rainbow.ViewModels.ClientModules.ClientModuleVM>> {
    return this.http.get<Yunyong.Core.PagingList<Rainbow.ViewModels.ClientModules.ClientModuleVM>>
      (`${this.baseUrl}api/ClientModule/Query?${stringify(option)}`, getHttpOptions());
  }

  /**
   * 获取客户端模块
   */
  public GetAsync(id: string)
    : Observable<Rainbow.ViewModels.ClientModules.ClientModuleVM> {
    return this.http.get<Rainbow.ViewModels.ClientModules.ClientModuleVM>
      (`${this.baseUrl}api/ClientModule?${stringify(id)}`, getHttpOptions());
  }

  /**
   * 获取客户端模块列表
   */
  public GetListAsync()
    : Observable<Rainbow.ViewModels.ClientModules.ClientModuleVM[]> {
    return this.http.get<Rainbow.ViewModels.ClientModules.ClientModuleVM[]>
      (`${this.baseUrl}api/ClientModule/List`, getHttpOptions());
  }

  /**
   * 删除客户端模块
   */
  public DeleteAsync(vm: Rainbow.ViewModels.ClientModules.DeleteClientModuleVM)
    : Observable<Yunyong.Core.AsyncTaskResult> {
    return this.http.delete<Yunyong.Core.AsyncTaskResult>
      (`${this.baseUrl}api/ClientModule/Delete?${stringify(vm)}`, getHttpOptions());
  }
}
