import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { stringify } from 'querystring';
import { getHttpOptions } from './httpOptions';

@Injectable()
export class ModelService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }


  /**
   * 创建更新设置
   */
  public CreateUpdateFiles(vm: Rainbow.ViewModels.Models.CreateModelSuitApplyVM)
    : Observable<boolean> {
    return this.http.post<boolean>
      (`${this.baseUrl}CreateUpdate`,{ ...vm }, { ...getHttpOptions() });
  }

  /**
   * 获取model列表
   */
  public GetModelTypes()
    : Observable<Rainbow.ViewModels.Models.ModelTypeVM[]> {
    return this.http.get<Rainbow.ViewModels.Models.ModelTypeVM[]>
      (`${this.baseUrl}List`, { ...getHttpOptions() });
  }

  /**
   * 重新生成TS代码
   */
  public RegenerateTsCode()
    : Observable<Yunyong.Core.AsyncTaskTResult<boolean>> {
    return this.http.post<Yunyong.Core.AsyncTaskTResult<boolean>>
      (`${this.baseUrl}RegenerateTsCode`,{ ...{} }, { ...getHttpOptions() });
  }

  /**
   * 更新AppRouting
   */
  public UpdateAppRoutingModule()
    : Observable<Yunyong.Core.AsyncTaskTResult<boolean>> {
    return this.http.post<Yunyong.Core.AsyncTaskTResult<boolean>>
      (`${this.baseUrl}UpdateAppRoutingModule`,{ ...{} }, { ...getHttpOptions() });
  }
}
