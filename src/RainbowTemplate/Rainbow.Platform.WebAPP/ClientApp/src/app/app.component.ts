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
  constructor(private layoutService: LayoutService, siteService: SiteService) {
    this.site = siteService.getSite();
  }
  ngOnInit(): void {
    this.layoutService.isCustomLayout.subscribe(value => {
      this.customLayout = value;
    });
  }
}
