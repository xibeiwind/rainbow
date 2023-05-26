import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { stringify } from 'qs';
import { getHttpOptions } from './httpOptions';

@Injectable()
export class ControllerProjectService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }


  /**
   * 创建Controller项目
   */
  public CreateAsync(vm: Rainbow.ViewModels.ControllerProjects.CreateControllerProjectVM)
    : Observable<Yunyong.Core.AsyncTaskTResult<string>> {
    return this.http.post<Yunyong.Core.AsyncTaskTResult<string>>
      (`${this.baseUrl}api/ControllerProject/Create`, { ...vm }, { ...getHttpOptions() });
  }

  /**
   * 更新Controller项目
   */
  public UpdateAsync(vm: Rainbow.ViewModels.ControllerProjects.UpdateControllerProjectVM)
    : Observable<Yunyong.Core.AsyncTaskTResult<string>> {
    return this.http.put<Yunyong.Core.AsyncTaskTResult<string>>
      (`${this.baseUrl}api/ControllerProject/Update`, { ...vm }, { ...getHttpOptions() });
  }

  /**
   * 查询Controller项目列表（分页）
   */
  public QueryAsync(option: Rainbow.ViewModels.ControllerProjects.QueryControllerProjectVM)
    : Observable<Yunyong.Core.PagingList<Rainbow.ViewModels.ControllerProjects.ControllerProjectVM>> {
    return this.http.get<Yunyong.Core.PagingList<Rainbow.ViewModels.ControllerProjects.ControllerProjectVM>>
      (`${this.baseUrl}api/ControllerProject/Query?${stringify(option, { allowDots: true })}`, { ...getHttpOptions() });
  }

  /**
   * 获取Controller项目
   */
  public GetAsync(id: string)
    : Observable<Rainbow.ViewModels.ControllerProjects.ControllerProjectVM> {
    return this.http.get<Rainbow.ViewModels.ControllerProjects.ControllerProjectVM>
      (`${this.baseUrl}api/ControllerProject/?id=${id}`, { ...getHttpOptions() });
  }

  /**
   * 获取Controller项目列表
   */
  public GetListAsync()
    : Observable<Rainbow.ViewModels.ControllerProjects.ControllerProjectVM[]> {
    return this.http.get<Rainbow.ViewModels.ControllerProjects.ControllerProjectVM[]>
      (`${this.baseUrl}api/ControllerProject/List`, { ...getHttpOptions() });
  }

  /**
   * 删除Controller项目
   */
  public  DeleteAsync(vm: Rainbow.ViewModels.ControllerProjects.DeleteControllerProjectVM)
    : Observable<Yunyong.Core.AsyncTaskResult> {
    return this.http.delete<Yunyong.Core.AsyncTaskResult>
      (`${this.baseUrl}api/ControllerProject/Delete`, { params: { ...vm }, ...getHttpOptions() });
  }
}
