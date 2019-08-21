import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../services/AccountService';
import { CustomerServiceAccountService } from '../../services/CustomerServiceAccountService';

@Component({
  selector: 'app-sidebar-left-inner',
  templateUrl: './sidebar-left-inner.component.html',
  styleUrls: ['./sidebar-left-inner.component.scss']
})
export class SidebarLeftInnerComponent implements OnInit {
  user: Rainbow.ViewModels.Users.CustomerServiceVM;

  constructor(private service: CustomerServiceAccountService) { }

  ngOnInit() {
    this.service.IsLogin().subscribe(isLogin => {
      if (isLogin === true) {
        this.service.GetCustomerService().subscribe(res => {
          this.user = res.Data;
        });
      }
    });
  }

}
