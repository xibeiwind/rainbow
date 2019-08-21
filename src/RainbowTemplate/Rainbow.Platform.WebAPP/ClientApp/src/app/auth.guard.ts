import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { CustomerServiceAccountService } from './services/CustomerServiceAccountService';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private service: CustomerServiceAccountService,
    private router: Router, private toastr: ToastrService) {

  }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {

    return new Observable<boolean>((observer) => {

      this.service.IsLogin().subscribe(res => {
        if (res === false) {
          this.toastr.info('未登录');
          this.router.navigate(['/auth/login']);
        }
        observer.next(res);
        observer.complete();
      });
      // observer.complete();
    });




    return this.service.IsLogin();
  }
}
