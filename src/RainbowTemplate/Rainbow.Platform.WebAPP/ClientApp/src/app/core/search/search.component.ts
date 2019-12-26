import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { EnumCacheService } from 'src/app/services/EnumCacheService';
import { InputTypeService } from 'src/app/services/InputTypeService';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {
  _fields: Rainbow.ViewModels.FieldDisplayVM[];
  queryForm: any;

  @Input()
  set fields(items: Rainbow.ViewModels.FieldDisplayVM[]) {
    this._fields = items;

    const formFields = {};
    items.forEach(field => {
      formFields[field.Name] = [''];
      if (field.IsEnum) {
        this.enumObj[field.Name] = this.enumService.GetEnumDisplay(field.FieldType).Fields;
      }
    });
    this.queryForm = this.formBuilder.group(formFields);
  }

  get fields(): Rainbow.ViewModels.FieldDisplayVM[] {
    return this._fields;
  }

  @Output()
  onsubmit: EventEmitter<Yunyong.Core.QueryOption> = new EventEmitter<Yunyong.Core.QueryOption>();
  protected enumObj = {};



  constructor(
    private enumService: EnumCacheService,
    private inputTypeService: InputTypeService,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit() {
  }

  querySubmit() {
    const queryData = { ...this.queryForm.value };
    this.onsubmit.next(queryData);
  }
  getInputControlType(field: Rainbow.ViewModels.FieldDisplayVM): string {
    return this.inputTypeService.getInputControlType(field);
  }
  getInputType(field: Rainbow.ViewModels.FieldDisplayVM) {
    return this.inputTypeService.getInputType(field);
  }
}
