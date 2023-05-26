import { Location } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { EnumCacheService } from 'src/app/services/EnumCacheService';

@Component({
  selector: 'app-item-detail',
  templateUrl: './item-detail.component.html',
  styleUrls: ['./item-detail.component.scss']
})
export class ItemDetailComponent implements OnInit {
  innerFields: Rainbow.ViewModels.FieldDisplayVM[];
  enumObj = {};

  @Input()
  modelDisplayName: string;

  @Input()
  item: any = {};

  @Output()
  onrefresh: EventEmitter<void> = new EventEmitter();

  @Input()
  get fields(): Rainbow.ViewModels.FieldDisplayVM[] {
    return this.innerFields;
  }
  set fields(data: Rainbow.ViewModels.FieldDisplayVM[]) {
    this.innerFields = data;
    if (data !== undefined) {
      data.forEach(field => {
        if (field.IsEnum) {
          this.enumObj[field.Name] = {};
          let tmp = this.enumService.GetEnumDisplay(field.FieldType);
          if (tmp) {
            tmp.Fields.forEach(f => {
              this.enumObj[field.Name][f.Value] = f;
            });
          }
          // this.enumService.GetEnumDisplay(field.FieldType).Fields.forEach(f => {
          //   this.enumObj[field.Name][f.Value] = f;
          // });
        }
      });
    }
  }
  constructor(
    private enumService: EnumCacheService,
    private location: Location
  ) { }

  ngOnInit() {

  }

  goBack() {
    this.location.back();
  }

  refresh() {
    this.onrefresh.next();
  }
}
