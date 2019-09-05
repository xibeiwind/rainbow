import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { stringify } from 'querystring';
import { getHttpOptions } from './httpOptions';

@Injectable()
export class RoleInfoService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }


  /**
   * 创建角色
   */
  public CreateAsync(vm: Rainbow.ViewModels.RoleInfos.CreateRoleInfoVM)
    : Observable<Yunyong.Core.AsyncTaskTResult<string>> {
    return this.http.post<Yunyong.Core.AsyncTaskTResult<string>>
      (`${this.baseUrl}Create`,{ ...vm }, { ...getHttpOptions() });
  }

  /**
   * 更新角色
   */
  public UpdateAsync(vm: Rainbow.ViewModels.RoleInfos.UpdateRoleInfoVM)
    : Observable<Yunyong.Core.AsyncTaskTResult<string>> {
    return this.http.put<Yunyong.Core.AsyncTaskTResult<string>>
      (`${this.baseUrl}Update`,{ ...vm }, { ...getHttpOptions() });
  }

  /**
   * 查询角色列表（分页）
   */
  public QueryAsync(option: Rainbow.ViewModels.RoleInfos.QueryRoleInfoVM)
    : Observable<Yunyong.Core.PagingList<Rainbow.ViewModels.RoleInfos.RoleInfoVM>> {
    return this.http.get<Yunyong.Core.PagingList<Rainbow.ViewModels.RoleInfos.RoleInfoVM>>
      (`${this.baseUrl}Query`, { params: {option: option}, ...getHttpOptions() });
  }

  /**
   * 获取角色
   */
  public GetAsync(id: string)
    : Observable<Rainbow.ViewModels.RoleInfos.RoleInfoVM> {
    return this.http.get<Rainbow.ViewModels.RoleInfos.RoleInfoVM>
      (`${this.baseUrl}`, { params: {id: id}, ...getHttpOptions() });
  }

  /**
   * 获取角色列表
   */
  public GetListAsync()
    : Observable<Rainbow.ViewModels.RoleInfos.RoleInfoVM[]> {
    return this.http.get<Rainbow.ViewModels.RoleInfos.RoleInfoVM[]>
      (`${this.baseUrl}List`, { ...getHttpOptions() });
  }

  /**
   * 删除角色
   */
  public  DeleteAsync(vm: Rainbow.ViewModels.RoleInfos.DeleteRoleInfoVM)
    : Observable<Yunyong.Core.AsyncTaskResult> {
    return this.http.delete<Yunyong.Core.AsyncTaskResult>
      (`${this.baseUrl}Delete`, { params: { ...vm }, ...getHttpOptions() });
  }
}
