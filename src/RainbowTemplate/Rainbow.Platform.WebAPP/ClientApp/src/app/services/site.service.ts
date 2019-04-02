import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SiteService {

  getSite() {
    return {
      siteName: '彩虹',
      siteEngName: 'Rainbow',
      title: 'RAINBOW',
      producer: 'XIBEIWIND',
      version: '1.0.0'
    };
  }
}
