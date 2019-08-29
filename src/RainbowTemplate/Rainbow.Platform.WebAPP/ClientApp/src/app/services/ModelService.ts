import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { stringify } from 'querystring';
import { getHttpOptions } from './httpOptions';

@Injectable()
export class ModelService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  // CreateUpdateFiles
  public CreateUpdateFiles(vm: Rainbow.ViewModels.Models.CreateModelSuitApplyVM)
    : Observable<boolean> {
    return this.http.post<boolean>
      (`${this.baseUrl}api/Model/CreateUpdate`, vm, getHttpOptions());
  }
  // GetModelTypes
  public GetModelTypes()
    : Observable<Rainbow.ViewModels.Models.ModelTypeVM[]> {
    return this.http.get<Rainbow.ViewModels.Models.ModelTypeVM[]>
      (`${this.baseUrl}api/Model/List`, getHttpOptions());
  }
  // RegenerateTsCode
  public RegenerateTsCode()
    : Observable<Yunyong.Core.AsyncTaskTResult<boolean>> {
    return this.http.post<Yunyong.Core.AsyncTaskTResult<boolean>>
      (`${this.baseUrl}api/Model/RegenerateTsCode`, {}, getHttpOptions());
  }
}