import { Component, OnInit } from '@angular/core';
import { SiteService } from 'src/app/services/site.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  private loginInfo = {};
  private site = { title: "管理彩虹" };
  constructor(site: SiteService) {
    this.site = site.getSite();
  }

  ngOnInit() {
  }

}
