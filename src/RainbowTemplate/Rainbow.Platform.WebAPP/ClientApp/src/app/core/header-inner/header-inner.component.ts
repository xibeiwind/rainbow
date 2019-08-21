import { Component, OnInit } from '@angular/core';
import { MessageService } from '../../services/MessageService';
// import { AccountService } from '../../services/AccountService';
import { CustomerServiceAccountService } from '../../services/CustomerServiceAccountService';

@Component({
  selector: 'app-header-inner',
  templateUrl: './header-inner.component.html',
  styleUrls: ['./header-inner.component.scss']
})
export class HeaderInnerComponent implements OnInit {
  msgData: Yunyong.Core.PagingList<Rainbow.ViewModels.Messages.MessageVM>;
  user: Rainbow.ViewModels.Users.CustomerServiceVM;

  constructor(private msgService: MessageService, private accountSevice: CustomerServiceAccountService) { }

  ngOnInit() {
    this.msgService.QueryAsync({ PageSize: 5, PageIndex: 1, OrderBys: [] }).subscribe(res => {
      this.msgData = res;
    });

    this.accountSevice.GetCustomerService().subscribe(res => {
      this.user = res.Data;
    });
    // this.accountSevice().subscribe(res => {
    //   this.user = res;
    // });
  }

}
