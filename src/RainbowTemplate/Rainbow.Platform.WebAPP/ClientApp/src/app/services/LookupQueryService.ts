import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { stringify } from 'querystring';
import { getHttpOptions } from './httpOptions';

@Injectable()
export class LookupQueryService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }


  /**
   * Query
   */
  public Query(vm: Rainbow.ViewModels.LookupQueryVM)
    : Observable<any> {
    return this.http.post<any>
      (`${this.baseUrl}api/LookupQuery/Query`, { ...vm }, { ...getHttpOptions() });
  }
}
