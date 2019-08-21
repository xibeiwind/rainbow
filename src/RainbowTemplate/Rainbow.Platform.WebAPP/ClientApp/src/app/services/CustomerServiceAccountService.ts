
import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { stringify } from 'querystring';
import { getHttpOptions } from './httpOptions';

@Injectable()
export class CustomerServiceAccountService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string){ }

  public Login(vm: Rainbow.ViewModels.Users.LoginVM)
    : Observable<Yunyong.Core.AsyncTaskTResult<Rainbow.ViewModels.Users.LoginResultVM>> {
    return this.http.post<Yunyong.Core.AsyncTaskTResult<Rainbow.ViewModels.Users.LoginResultVM>>
      (`${this.baseUrl}api/CustomerServiceAccount/Login`, vm, getHttpOptions());
  }

  public GetCustomerService()
    : Observable<Yunyong.Core.AsyncTaskTResult<Rainbow.ViewModels.Users.CustomerServiceVM>> {
    return this.http.get<Yunyong.Core.AsyncTaskTResult<Rainbow.ViewModels.Users.CustomerServiceVM>>
      (`${this.baseUrl}api/CustomerServiceAccount/CustomerService`, getHttpOptions());
  }

  public Logout()
    : Observable<Yunyong.Core.AsyncTaskTResult<Boolean>> {
    return this.http.post<Yunyong.Core.AsyncTaskTResult<Boolean>>
      (`${this.baseUrl}api/CustomerServiceAccount/Logout`,{}, getHttpOptions());
  }

}

