import { Component, forwardRef, NgZone } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
declare const qq: any;
export const EXE_LOCATION_SELECT_ACCESSOR: any = {
  provide: NG_VALUE_ACCESSOR,
  useExisting: forwardRef(() => LocationSelectComponent),
  multi: true
};

@Component({
  selector: 'app-location-select',
  templateUrl: './location-select.component.html',
  styleUrls: ['./location-select.component.scss'],
  providers: [EXE_LOCATION_SELECT_ACCESSOR]
})
export class LocationSelectComponent implements ControlValueAccessor {


  options: any = { scrollwheel: false, };
  private marker: any;
  private map: any;
  private geocoder: any;


  // @ViewChild('map', { static: true })
  // mapComp: AqmComponent;
  isDisabled: boolean;

  constructor(private zone: NgZone) { }

  ngOnInit() {
  }
  onReady(mapNative: any) {
    mapNative.setOptions({
      zoom: 16,
      center: new qq.maps.LatLng(31.300042, 121.515484),
    });
    this.map = mapNative;

    let self = this;
    this.geocoder = new qq.maps.Geocoder({
      complete(res) {
        self.marker = self.marker || new qq.maps.Marker({
          position: res.detail.location,
          map: self.map,
          title: `${res.detail.address} ${res.detail.location}`
        });
        self.marker.setPosition(res.detail.location);

        let addComp = res.detail.addressComponents;
        let addStr = `${addComp.country} ${addComp.province} ${addComp.city} ${addComp.district} ${addComp.town} ${addComp.street}`;

        self.value = {
          Address: res.detail.address,
          Latitude: res.detail.location.lat,
          Longitude: res.detail.location.lng
        };
        self.zone.run(() => {
          // self.status = `${addStr} ${res.detail.address} ${res.detail.location}`;

        });
      }
    });

    if (self.value !== undefined && self.value !== null) {
      if (self.value.Latitude !== 0 && self.value.Longitude !== 0) {
        mapNative.setOptions({
          zoom: 16,
          center: new qq.maps.LatLng(self.value.Latitude, self.value.Longitude),
        });
        self.marker = new qq.maps.Marker({
          position: new qq.maps.LatLng(self.value.Latitude, self.value.Longitude),
          map: self.map,
          title: `${self.value.Address} ${self.value.Latitude} ${self.value.Longitude}`
        });
      }
    }
    // 添加监听事件
    qq.maps.event.addListener(this.map, 'click', (event: any) => {
      this.geocoder.getAddress(event.latLng);
    });
  }
  ngOnDestroy(): void {
    ['click'].forEach(eventName => {
      qq.maps.event.clearListeners(this.map, eventName);
    });
  }

  onChange = (_: any) => { };
  onTouched = () => { };

  writeValue(obj: any): void {
    this._value = obj;
  }
  registerOnChange(fn: any): void {
    this.onChange = fn;
  }
  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }
  setDisabledState?(isDisabled: boolean): void {
    this.isDisabled = isDisabled;
  }


  private _value: Rainbow.ViewModels.Utils.LocationVM = null;

  public get value(): Rainbow.ViewModels.Utils.LocationVM {
    return this._value;
  }
  public set value(v: Rainbow.ViewModels.Utils.LocationVM) {
    if (this._value !== v) {
      this._value = v;
      this.onChange(v);
      this.onTouched();

      let self = this;
      // self.marker = self.marker || new qq.maps.Marker({
      //   position: {v.Latitude},
      //   map: self.map,
      //   title: `${res.detail.address} ${res.detail.location}`
      // });
      // self.marker.setPosition(res.detail.location);
    }
  }

}
