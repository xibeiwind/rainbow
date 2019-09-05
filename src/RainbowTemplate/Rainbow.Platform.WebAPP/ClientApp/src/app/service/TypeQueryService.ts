import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { stringify } from 'querystring';
import { getHttpOptions } from './httpOptions';

@Injectable()
export class TypeQueryService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }


  /**
   * 类型查询
   */
  public Query(keyword: string)
    : Observable<System.String[]> {
    return this.http.get<System.String[]>
      (`${this.baseUrl}Query/${keyword}`, { params: {keyword: keyword}, ...getHttpOptions() });
  }
}
