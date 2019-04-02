
import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { stringify } from 'querystring';

@Injectable()
export class AccountService {    
    constructor(private http: HttpClient,@Inject('BASE_URL') private baseUrl: string){ }

    public RegisterAsync(vm: Rainbow.ViewModels.Users.RegisterUserVM): Observable<Yunyong.Core.AsyncTaskResult> {
        return this.http.post<Yunyong.Core.AsyncTaskResult>(`${this.baseUrl}api/Account/Register`, vm);
    }

    public LoginAsync(vm: Rainbow.ViewModels.Users.LoginVM): Observable<Rainbow.ViewModels.Users.LoginResultVM> {
        return this.http.post<Rainbow.ViewModels.Users.LoginResultVM>(`${this.baseUrl}api/Account/Login`, vm);
    }

    public ForgetPasswordAsync(vm: Rainbow.ViewModels.Users.ForgetPasswordVM): Observable<Yunyong.Core.AsyncTaskResult> {
        return this.http.get<Yunyong.Core.AsyncTaskResult>(`${this.baseUrl}api/Account/ForgetPassword?${stringify(vm)}`);
    }

    public GetUserAsync(): Observable<Rainbow.ViewModels.Users.UserVM> {
        return this.http.get<Rainbow.ViewModels.Users.UserVM>(`${this.baseUrl}api/Account/UserInfo`);
    }

    public UserAvatar(userId: String): Observable<String> {
        return this.http.get<String>(`${this.baseUrl}api/Account/UserAvatar/${userId}`);
    }

}

