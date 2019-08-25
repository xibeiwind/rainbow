import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { stringify } from 'querystring';
import { getHttpOptions } from './httpOptions';

@Injectable()
export class EnumDisplayService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  // GetEnumDisplayList
  public GetEnumDisplayList()
    : Observable<Yunyong.Core.AsyncTaskTResult<Rainbow.ViewModels.EnumDisplayVM[]>> {
    return this.http.get<Yunyong.Core.AsyncTaskTResult<Rainbow.ViewModels.EnumDisplayVM[]>>
      (`${this.baseUrl}api/EnumDisplay/List`, getHttpOptions());
  }
  // GetEnumDisplay
  public GetEnumDisplay(name: String)
    : Observable<Yunyong.Core.AsyncTaskTResult<Rainbow.ViewModels.EnumDisplayVM>> {
    return this.http.get<Yunyong.Core.AsyncTaskTResult<Rainbow.ViewModels.EnumDisplayVM>>
      (`${this.baseUrl}api/EnumDisplay/${name}`, getHttpOptions());
  }
}