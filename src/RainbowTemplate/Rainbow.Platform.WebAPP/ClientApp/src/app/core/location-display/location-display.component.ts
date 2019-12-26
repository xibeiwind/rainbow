import { Component, OnInit, Input, ElementRef, NgZone, Inject } from '@angular/core';
declare const qq: any;

@Component({
  selector: 'app-location-display',
  templateUrl: './location-display.component.html',
  styleUrls: ['./location-display.component.scss']
})
export class LocationDisplayComponent implements OnInit {
  @Input()
  class: string;

  private _location: Rainbow.ViewModels.Utils.LocationVM;
  @Input()
  public get location(): Rainbow.ViewModels.Utils.LocationVM {
    return this._location;
  }
  public set location(value: Rainbow.ViewModels.Utils.LocationVM) {
    this._location = value;
    if (value.Latitude !== 0 && value.Longitude !== 0) {
      this.mapImgUrl = `https://apis.map.qq.com/ws/staticmap/v2/?center=${value.Latitude},${value.Longitude}&zoom=16&size=600*300&maptype=roadmap&markers=size:large|color:0xFFCCFF|label:k|${value.Latitude},${value.Longitude}&key=${this.mapKey}`;
    }
  }

  mapImgUrl: string;

  constructor(
    @Inject('MAP_KEY') private mapKey: string) { }

  ngOnInit() {
  }
}
