import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { stringify } from 'qs';
import { getHttpOptions } from './httpOptions';

@Injectable()
export class AccountService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }


  /**
   * 用户注册
   */
  public Register(vm: Rainbow.ViewModels.Users.RegisterUserVM)
    : Observable<Yunyong.Core.AsyncTaskResult> {
    return this.http.post<Yunyong.Core.AsyncTaskResult>
      (`${this.baseUrl}api/Account/Register`, { ...vm }, { ...getHttpOptions() });
  }

  /**
   * 登录
   */
  public Login(vm: Rainbow.ViewModels.Users.LoginVM)
    : Observable<Rainbow.ViewModels.Users.LoginResultVM> {
    return this.http.post<Rainbow.ViewModels.Users.LoginResultVM>
      (`${this.baseUrl}api/Account/Login`, { ...vm }, { ...getHttpOptions() });
  }

  /**
   * 退出登录
   */
  public Logout()
    : Observable<Yunyong.Core.AsyncTaskTResult<boolean>> {
    return this.http.post<Yunyong.Core.AsyncTaskTResult<boolean>>
      (`${this.baseUrl}api/Account/Logout`, { ...{} }, { ...getHttpOptions() });
  }

  /**
   * 获取用户信息
   */
  public GetUserAsync()
    : Observable<Rainbow.ViewModels.Users.UserProfileVM> {
    return this.http.get<Rainbow.ViewModels.Users.UserProfileVM>
      (`${this.baseUrl}api/Account/UserInfo`, { ...getHttpOptions() });
  }

  /**
   * 是否已登录
   */
  public IsLogin()
    : Observable<boolean> {
    return this.http.get<boolean>
      (`${this.baseUrl}api/Account/IsLogin`, { ...getHttpOptions() });
  }

  /**
   * 用户是否具有某角色
   */
  public UserInRole(roleName: string)
    : Observable<Yunyong.Core.AsyncTaskTResult<boolean>> {
    return this.http.get<Yunyong.Core.AsyncTaskTResult<boolean>>
      (`${this.baseUrl}api/Account/UserInRole/${roleName}?roleName=${roleName}`, { ...getHttpOptions() });
  }
}
