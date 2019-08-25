import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { stringify } from 'querystring';
import { getHttpOptions } from './httpOptions';

@Injectable()
export class EnumDisplayService {
  enumObj = {};
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.GetEnumDisplayList().subscribe(res => {
      res.Data.forEach(item => {
        this.enumObj[item.Name] = item;
      });
    });
  }


  public GetEnumDisplayList()
    : Observable<Yunyong.Core.AsyncTaskTResult<Rainbow.ViewModels.EnumDisplayVM[]>> {
    return this.http.get<Yunyong.Core.AsyncTaskTResult<Rainbow.ViewModels.EnumDisplayVM[]>>
      (`${this.baseUrl}api/EnumDisplay/List`, getHttpOptions());
  }

  public GetEnumDisplay(name: string)
    : Rainbow.ViewModels.EnumDisplayVM {
    if (this.enumObj.hasOwnProperty(name)) {
      return this.enumObj[name];
    } else {
      return undefined;
    }
  }
}
