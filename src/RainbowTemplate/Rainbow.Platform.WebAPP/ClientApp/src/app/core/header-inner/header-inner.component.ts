import { Component, OnInit } from '@angular/core';
// import { AccountService } from '../../services/AccountService';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/services/AccountService';
import { MessageService } from '../../services/MessageService';

@Component({
  selector: 'app-header-inner',
  templateUrl: './header-inner.component.html',
  styleUrls: ['./header-inner.component.scss']
})
export class HeaderInnerComponent implements OnInit {
  msgData: Yunyong.Core.PagingList<Rainbow.ViewModels.Messages.MessageVM>;
  user: Rainbow.ViewModels.Users.UserProfileVM;

  constructor(
    private router: Router,
    private msgService: MessageService,
    private accountService: AccountService) { }

  ngOnInit() {
    this.msgService.QueryAsync({ PageSize: 5, PageIndex: 1, OrderBys: [] }).subscribe(res => {
      this.msgData = res;
    });

    this.accountService.IsLogin().subscribe(isLogin => {
      if (isLogin === true) {
        this.accountService.GetUserAsync().subscribe(res => {
          this.user = res;
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
