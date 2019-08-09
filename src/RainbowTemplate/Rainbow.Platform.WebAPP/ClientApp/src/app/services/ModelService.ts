
import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { stringify } from 'querystring';

@Injectable()
export class ModelService {
  constructor(private http: HttpClient,@Inject('BASE_URL') private baseUrl: string){ }

  public CreateUpdateFiles(vm: Rainbow.ViewModels.Models.CreateModelSuitApplyVM)
    : Observable<Boolean> {
    return this.http.post<Boolean>
      (`${this.baseUrl}api/Model/CreateUpdate`, vm);
  }

  public GetModelTypes()
    : Observable<Rainbow.ViewModels.Models.ModelTypeVM[]> {
    return this.http.get<Rainbow.ViewModels.Models.ModelTypeVM[]>
      (`${this.baseUrl}api/Model/List`);
  }

  public RegenerateTsCode()
    : Observable<Yunyong.Core.AsyncTaskTResult<Boolean>> {
    return this.http.post<Yunyong.Core.AsyncTaskTResult<Boolean>>
      (`${this.baseUrl}api/Model/RegenerateTsCode`, {});
  }

}

