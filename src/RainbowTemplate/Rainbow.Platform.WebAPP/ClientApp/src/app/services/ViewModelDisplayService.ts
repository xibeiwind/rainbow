import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { stringify } from 'querystring';
import { getHttpOptions } from './httpOptions';

@Injectable()
export class ViewModelDisplayService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }


  /**
   * GetVMDisplay
   */
  public GetVMDisplay(vm: Rainbow.ViewModels.DisplayQueryVM)
    : Observable<Yunyong.Core.AsyncTaskTResult<Rainbow.ViewModels.ViewModelDisplayVM>> {
    return this.http.get<Yunyong.Core.AsyncTaskTResult<Rainbow.ViewModels.ViewModelDisplayVM>>
      (`${this.baseUrl}api/ViewModelDisplay/VMDisplay?${stringify(vm)}`, getHttpOptions());
  }

  /**
   * GetModelVMDisplays
   */
  public GetModelVMDisplays(vm: Rainbow.ViewModels.DisplayQueryVM)
    : Observable<Yunyong.Core.AsyncTaskTResult<Rainbow.ViewModels.ModelDisplaySuitVM>> {
    return this.http.get<Yunyong.Core.AsyncTaskTResult<Rainbow.ViewModels.ModelDisplaySuitVM>>
      (`${this.baseUrl}api/ViewModelDisplay/ModelVMDisplays?${stringify(vm)}`, getHttpOptions());
  }
}
