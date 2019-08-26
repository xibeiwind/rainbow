import { Component, OnInit } from '@angular/core';
import { LayoutService } from 'angular-admin-lte';
import { SiteService } from './services/site.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  public customLayout: boolean;
  site: any;
  title = 'RainbowAdmin';
  constructor(private layoutService: LayoutService, private siteService: SiteService) {
  }
  ngOnInit(): void {
    this.site = this.siteService.getSite();
    this.layoutService.isCustomLayout.subscribe(value => {
      this.customLayout = value;
    });
  }
}
