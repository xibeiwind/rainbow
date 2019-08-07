import { Component, OnInit } from '@angular/core';
import { MessageService } from '../../services/MessageService';
import { AccountService } from '../../services/AccountService';

@Component({
  selector: 'app-header-inner',
  templateUrl: './header-inner.component.html',
  styleUrls: ['./header-inner.component.scss']
})
export class HeaderInnerComponent implements OnInit {
  msgData: Yunyong.Core.PagingList<Rainbow.ViewModels.Messages.MessageVM>;
  user: Rainbow.ViewModels.Users.UserVM;

  constructor(private msgService: MessageService, private accountSevice: AccountService) { }

  ngOnInit() {
    this.msgService.QueryAsync({ PageSize: 5, PageIndex: 1, OrderBys: [] }).subscribe(res => {
      this.msgData = res;
    });

    this.accountSevice.GetUserAsync().subscribe(res => {
      this.user = res;
    });
  }

}
