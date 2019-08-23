import { Component, OnInit } from '@angular/core';
import { MessageService } from '../../services/MessageService';
// import { AccountService } from '../../services/AccountService';
import { CustomerServiceAccountService } from '../../services/CustomerServiceAccountService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header-inner',
  templateUrl: './header-inner.component.html',
  styleUrls: ['./header-inner.component.scss']
})
export class HeaderInnerComponent implements OnInit {
  msgData: Yunyong.Core.PagingList<Rainbow.ViewModels.Messages.MessageVM>;
  user: Rainbow.ViewModels.CustomerServices.CustomerServiceVM;

  constructor(
    private router: Router,
    private msgService: MessageService,
    private accountService: CustomerServiceAccountService) { }

  ngOnInit() {
    this.msgService.QueryAsync({ PageSize: 5, PageIndex: 1, OrderBys: [] }).subscribe(res => {
      this.msgData = res;
    });

    this.accountService.IsLogin().subscribe(isLogin => {
      if (isLogin === true) {
        this.accountService.GetCustomerService().subscribe(res => {
          this.user = res.Data;
          this.user.AvatarUrl = this.user.AvatarUrl || '/assets/img/avatar.png';
        });
      }
    });

    // this.accountSevice().subscribe(res => {
    //   this.user = res;
    // });

  }

  signout() {
    this.accountService.Logout().subscribe(res => {
      localStorage.removeItem('token');
      this.router.navigate(['/auth/login']);
    });
  }
}
