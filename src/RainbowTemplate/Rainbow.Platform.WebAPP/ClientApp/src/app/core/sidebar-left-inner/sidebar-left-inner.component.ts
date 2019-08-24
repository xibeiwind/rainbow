import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../services/AccountService';

@Component({
  selector: 'app-sidebar-left-inner',
  templateUrl: './sidebar-left-inner.component.html',
  styleUrls: ['./sidebar-left-inner.component.scss']
})
export class SidebarLeftInnerComponent implements OnInit {
  user: Rainbow.ViewModels.Users.UserProfileVM;

  constructor(private service: AccountService) { }

  ngOnInit() {
    this.service.IsLogin().subscribe(isLogin => {
      if (isLogin === true) {
        this.service.GetUserAsync().subscribe(res => {
          this.user = res;
          this.user.AvatarUrl = this.user.AvatarUrl || '/assets/img/avatar.png';
        });
      }
    });
  }

}
