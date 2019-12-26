import { Component, Input, OnInit } from '@angular/core';
import { LookupQueryService } from 'src/app/services/LookupQueryService';

@Component({
  selector: 'app-lookup-display',
  templateUrl: './lookup-display.component.html',
  styleUrls: ['./lookup-display.component.scss']
})
export class LookupDisplayComponent implements OnInit {

  @Input()
  lookup: Rainbow.ViewModels.Utils.LookupSettingVM;
  data: Rainbow.ViewModels.Utils.LookupResultVM;
  
  @Input()
  set value(val: string) {
    this.service.GetAsync({
      TypeName: this.lookup.TypeName,
      DisplayField: this.lookup.DisplayField,
      ValueField: this.lookup.ValueField,
      Filter: val,
    }).subscribe(res => {
      this.data = res;
    });
  }
  constructor(private service: LookupQueryService) { }

  ngOnInit() {
  }

}
