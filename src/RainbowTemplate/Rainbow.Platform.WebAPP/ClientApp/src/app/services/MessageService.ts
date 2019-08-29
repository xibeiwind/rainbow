import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { stringify } from 'querystring';
import { getHttpOptions } from './httpOptions';

@Injectable()
export class MessageService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  // QueryAsync
  public QueryAsync(option: Rainbow.ViewModels.Messages.MessageQueryOption)
    : Observable<Yunyong.Core.PagingList<Rainbow.ViewModels.Messages.MessageVM>> {
    return this.http.get<Yunyong.Core.PagingList<Rainbow.ViewModels.Messages.MessageVM>>
      (`${this.baseUrl}api/Message/Query?${stringify(option)}`, getHttpOptions());
  }
  // GetAsync
  public GetAsync(msgId: string)
    : Observable<Rainbow.ViewModels.Messages.MessageVM> {
    return this.http.get<Rainbow.ViewModels.Messages.MessageVM>
      (`${this.baseUrl}api/Message/Get/${msgId}`, getHttpOptions());
  }
  // ReadedAsync
  public ReadedAsync(msgId: string)
    : Observable<Yunyong.Core.AsyncTaskResult> {
    return this.http.put<Yunyong.Core.AsyncTaskResult>
      (`${this.baseUrl}api/Message/Readed/${msgId}`, msgId, getHttpOptions());
  }
}