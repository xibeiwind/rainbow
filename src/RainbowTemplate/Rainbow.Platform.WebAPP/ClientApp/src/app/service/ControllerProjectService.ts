import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { stringify } from 'querystring';
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
      (`${this.baseUrl}Create`,{ ...vm }, { ...getHttpOptions() });
  }

  /**
   * 更新Controller项目
   */
  public UpdateAsync(vm: Rainbow.ViewModels.ControllerProjects.UpdateControllerProjectVM)
    : Observable<Yunyong.Core.AsyncTaskTResult<string>> {
    return this.http.put<Yunyong.Core.AsyncTaskTResult<string>>
      (`${this.baseUrl}Update`,{ ...vm }, { ...getHttpOptions() });
  }

  /**
   * 查询Controller项目列表（分页）
   */
  public QueryAsync(option: Rainbow.ViewModels.ControllerProjects.QueryControllerProjectVM)
    : Observable<Yunyong.Core.PagingList<Rainbow.ViewModels.ControllerProjects.ControllerProjectVM>> {
    return this.http.get<Yunyong.Core.PagingList<Rainbow.ViewModels.ControllerProjects.ControllerProjectVM>>
      (`${this.baseUrl}Query`, { params: {option: option}, ...getHttpOptions() });
  }

  /**
   * 获取Controller项目
   */
  public GetAsync(id: string)
    : Observable<Rainbow.ViewModels.ControllerProjects.ControllerProjectVM> {
    return this.http.get<Rainbow.ViewModels.ControllerProjects.ControllerProjectVM>
      (`${this.baseUrl}`, { params: {id: id}, ...getHttpOptions() });
  }

  /**
   * 获取Controller项目列表
   */
  public GetListAsync()
    : Observable<Rainbow.ViewModels.ControllerProjects.ControllerProjectVM[]> {
    return this.http.get<Rainbow.ViewModels.ControllerProjects.ControllerProjectVM[]>
      (`${this.baseUrl}List`, { ...getHttpOptions() });
  }

  /**
   * 删除Controller项目
   */
  public  DeleteAsync(vm: Rainbow.ViewModels.ControllerProjects.DeleteControllerProjectVM)
    : Observable<Yunyong.Core.AsyncTaskResult> {
    return this.http.delete<Yunyong.Core.AsyncTaskResult>
      (`${this.baseUrl}Delete`, { params: { ...vm }, ...getHttpOptions() });
  }
}
