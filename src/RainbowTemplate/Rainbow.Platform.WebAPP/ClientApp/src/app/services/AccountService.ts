
import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { stringify } from 'querystring';
import { getHttpOptions } from './httpOptions';

@Injectable()
export class AccountService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string){ }

  public RegisterAsync(vm: Rainbow.ViewModels.Users.RegisterUserVM)
    : Observable<Yunyong.Core.AsyncTaskResult> {
    return this.http.post<Yunyong.Core.AsyncTaskResult>
      (`${this.baseUrl}api/Account/Register`, vm, getHttpOptions());
  }

  public Login(vm: Rainbow.ViewModels.Users.LoginVM)
    : Observable<Rainbow.ViewModels.Users.LoginResultVM> {
    return this.http.post<Rainbow.ViewModels.Users.LoginResultVM>
      (`${this.baseUrl}api/Account/Login`, vm, getHttpOptions());
  }

  public Logout()
    : Observable<Yunyong.Core.AsyncTaskTResult<Boolean>> {
    return this.http.post<Yunyong.Core.AsyncTaskTResult<Boolean>>
      (`${this.baseUrl}api/Account/Logout`,{}, getHttpOptions());
  }

  public GetUserAsync()
    : Observable<Rainbow.ViewModels.Users.UserProfileVM> {
    return this.http.get<Rainbow.ViewModels.Users.UserProfileVM>
      (`${this.baseUrl}api/Account/UserInfo`, getHttpOptions());
  }

  public IsLogin()
    : Observable<Boolean> {
    return this.http.get<Boolean>
      (`${this.baseUrl}api/Account/IsLogin`, getHttpOptions());
  }

}

