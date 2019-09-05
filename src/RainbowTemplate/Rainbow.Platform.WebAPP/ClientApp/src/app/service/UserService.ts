import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { stringify } from 'querystring';
import { getHttpOptions } from './httpOptions';

@Injectable()
export class UserService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }


  /**
   * 获取显示User
   */
  public GetAsync(id: string)
    : Observable<any> {
    return this.http.get<any>
      (`${this.baseUrl}`, { params: {id: id}, ...getHttpOptions() });
  }

  /**
   * 获取显示User列表
   */
  public GetListAsync()
    : Observable<any> {
    return this.http.get<any>
      (`${this.baseUrl}List`, { ...getHttpOptions() });
  }

  /**
   * 查询User列表（分页）
   */
  public QueryAsync(option: Rainbow.ViewModels.Users.QueryUserVM)
    : Observable<any> {
    return this.http.get<any>
      (`${this.baseUrl}Query`, { params: {option: option}, ...getHttpOptions() });
  }

  /**
   * 创建User
   */
  public CreateAsync(vm: Rainbow.ViewModels.Users.CreateUserVM)
    : Observable<any> {
    return this.http.post<any>
      (`${this.baseUrl}Create`,{ ...vm }, { ...getHttpOptions() });
  }

  /**
   * 更新User
   */
  public UpdateAsync(vm: Rainbow.ViewModels.Users.UpdateUserVM)
    : Observable<any> {
    return this.http.put<any>
      (`${this.baseUrl}Update`,{ ...vm }, { ...getHttpOptions() });
  }

  /**
   * 删除User
   */
  public  DeleteAsync(vm: Rainbow.ViewModels.Users.DeleteUserVM)
    : Observable<any> {
    return this.http.delete<any>
      (`${this.baseUrl}Delete`, { params: { ...vm }, ...getHttpOptions() });
  }
}
