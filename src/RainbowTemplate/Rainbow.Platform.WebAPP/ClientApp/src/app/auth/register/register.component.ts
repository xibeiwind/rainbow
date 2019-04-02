import { Component, OnInit } from '@angular/core';
import { SiteService } from 'src/app/services/site.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  private site = { title: "管理彩虹" };
  private registerInfo = {};
  constructor(site:SiteService) {
    this.site = site.getSite();
   }

  ngOnInit() {
  }

  register() {

  }

}
