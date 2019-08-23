
import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { stringify } from 'querystring';
import { getHttpOptions } from './httpOptions';

@Injectable()
export class RoleInfoService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string){ }

  public CreateAsync(vm: Rainbow.ViewModels.RoleInfos.CreateRoleInfoVM)
    : Observable<Yunyong.Core.AsyncTaskTResult<String>> {
    return this.http.post<Yunyong.Core.AsyncTaskTResult<String>>
      (`${this.baseUrl}api/RoleInfo/Create`, vm, getHttpOptions());
  }

  public UpdateAsync(vm: Rainbow.ViewModels.RoleInfos.UpdateRoleInfoVM)
    : Observable<Yunyong.Core.AsyncTaskTResult<String>> {
    return this.http.put<Yunyong.Core.AsyncTaskTResult<String>>
      (`${this.baseUrl}api/RoleInfo/Update`, vm, getHttpOptions());
  }

  public QueryAsync(option: Rainbow.ViewModels.RoleInfos.QueryRoleInfoVM)
    : Observable<Yunyong.Core.PagingList<Rainbow.ViewModels.RoleInfos.RoleInfoVM>> {
    return this.http.get<Yunyong.Core.PagingList<Rainbow.ViewModels.RoleInfos.RoleInfoVM>>
      (`${this.baseUrl}api/RoleInfo/Query?${stringify(option)}`, getHttpOptions());
  }

  public GetAsync(id: String)
    : Observable<Rainbow.ViewModels.RoleInfos.RoleInfoVM> {
    return this.http.get<Rainbow.ViewModels.RoleInfos.RoleInfoVM>
      (`${this.baseUrl}api/RoleInfo?${stringify(id)}`, getHttpOptions());
  }

  public GetListAsync()
    : Observable<Rainbow.ViewModels.RoleInfos.RoleInfoVM[]> {
    return this.http.get<Rainbow.ViewModels.RoleInfos.RoleInfoVM[]>
      (`${this.baseUrl}api/RoleInfo/List`, getHttpOptions());
  }

  public DeleteAsync(vm: Rainbow.ViewModels.RoleInfos.DeleteRoleInfoVM)
    : Observable<Yunyong.Core.AsyncTaskResult> {
    return this.http.delete<Yunyong.Core.AsyncTaskResult>
      (`${this.baseUrl}api/RoleInfo/Delete?${stringify(vm)}`, getHttpOptions());
  }

}

