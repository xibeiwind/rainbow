import { Injectable } from '@angular/core';
import { CanLoad, Route, UrlSegment, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, CanActivate } from '@angular/router';
import { Observable } from 'rxjs';
import { AccountService } from './services/AccountService';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class SysAdminAuthGuard implements CanLoad, CanActivate {
  constructor(private service: AccountService, private toastr: ToastrService) {

  }
  canLoad(
    route: Route,
    segments: UrlSegment[]): Observable<boolean> | Promise<boolean> | boolean {
    return new Observable<boolean>(observer => {
      this.service.UserInRole('SysAdmin').subscribe(res => {
        if (res.Data === false) {
          this.toastr.error('没有权限');
        }
        observer.next(res.Data);
        observer.complete();
      });
    });
  }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    return new Observable<boolean>(observer => {
      this.service.UserInRole('SysAdmin').subscribe(res => {
        if (res.Data === false) {
          this.toastr.error('没有权限');
        }
        observer.next(res.Data);
        observer.complete();
      });
    });
  }

}
