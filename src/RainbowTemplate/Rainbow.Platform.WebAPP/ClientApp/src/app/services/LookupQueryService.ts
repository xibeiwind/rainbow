import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { stringify } from 'querystring';
import { getHttpOptions } from './httpOptions';

@Injectable()
export class LookupQueryService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }


  /**
   * Lookup查询
   */
  public QueryAsync(vm: Rainbow.ViewModels.Utils.LookupQueryVM)
    : Observable<Rainbow.ViewModels.Utils.LookupResultVM[]> {
    return this.http.get<Rainbow.ViewModels.Utils.LookupResultVM[]>
      (`${this.baseUrl}api/LookupQuery/Query?${stringify(vm)}`, { ...getHttpOptions() });
  }

  /**
   * Lookup获取
   */
  public GetAsync(vm: Rainbow.ViewModels.Utils.LookupQueryVM)
    : Observable<Rainbow.ViewModels.Utils.LookupResultVM> {
    return this.http.get<Rainbow.ViewModels.Utils.LookupResultVM>
      (`${this.baseUrl}api/LookupQuery/?${stringify(vm)}`, { ...getHttpOptions() });
  }
}
