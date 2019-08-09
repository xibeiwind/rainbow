
import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { stringify } from 'querystring';

@Injectable()
export class UserService {
  constructor(private http: HttpClient,@Inject('BASE_URL') private baseUrl: string){ }

  public GetAsync(id: String)
    : Observable<any> {
    return this.http.get<any>
      (`${this.baseUrl}api/User?${stringify(id)}`);
  }

  public GetListAsync()
    : Observable<any> {
    return this.http.get<any>
      (`${this.baseUrl}api/User/List`);
  }

  public QueryAsync(option: Rainbow.ViewModels.Users.QueryUserVM)
    : Observable<any> {
    return this.http.get<any>
      (`${this.baseUrl}api/User/Query?${stringify(option)}`);
  }

  public CreateAsync(vm: Rainbow.ViewModels.Users.CreateUserVM)
    : Observable<any> {
    return this.http.post<any>
      (`${this.baseUrl}api/User/Create`, vm);
  }

  public UpdateAsync(vm: Rainbow.ViewModels.Users.UpdateUserVM)
    : Observable<any> {
    return this.http.put<any>
      (`${this.baseUrl}api/User/Update`, vm);
  }

  public DeleteAsync(vm: Rainbow.ViewModels.Users.DeleteUserVM)
    : Observable<any> {
    return this.http.delete<any>
      (`${this.baseUrl}api/User/Delete?${stringify(vm)}`);
  }

}

