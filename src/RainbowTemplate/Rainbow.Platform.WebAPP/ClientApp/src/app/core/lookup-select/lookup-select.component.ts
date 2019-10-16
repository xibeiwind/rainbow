import { Component, OnInit, Input, forwardRef } from '@angular/core';
import { LookupQueryService } from 'src/app/services/LookupQueryService';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { Subject, Observable } from 'rxjs';

import { debounceTime, distinctUntilChanged, switchMap, mergeMap } from 'rxjs/operators';
import { TypeaheadMatch } from 'ngx-bootstrap';

export const EXE_LOOKUP_SELECT_ACCESSOR:any={
  provide: NG_VALUE_ACCESSOR,
  useExisting: forwardRef(() => LookupSelectComponent),
  multi: true
};
@Component({
  selector: 'app-lookup-select',
  templateUrl: './lookup-select.component.html',
  styleUrls: ['./lookup-select.component.scss'],
  providers:[EXE_LOOKUP_SELECT_ACCESSOR]
})
export class LookupSelectComponent implements OnInit, ControlValueAccessor {
  @Input()
  ctrlclass: string;
  @Input()
  placeholder: string;
  @Input('lookuprequired')
  required:boolean;

  @Input()
  lookup: Rainbow.ViewModels.Utils.LookupSettingVM;

  searchText: string;

  private _value: any = null;
  isDisabled: boolean;
  items$: Observable<Rainbow.ViewModels.Utils.LookupResultVM[]>;
  currentItem: Rainbow.ViewModels.Utils.LookupResultVM;
  selectShow: boolean;


  get value(): any {
    return this._value;
  }
  set value(val: any) {
    this._value = val;
    this.onChange(val);
    this.onTouched();
  }

  searchTerms: Subject<string> = new Subject<string>();

  constructor(private service: LookupQueryService) { }

  ngOnInit() {
    this.items$ = Observable.create(observer => {
      observer.next(this.searchText);
    }).pipe(mergeMap((keyword: string) => {
      return        this.lookupSearch(keyword);

    })
    );
  }
  lookupSearch(keyword: string): Observable<Rainbow.ViewModels.Utils.LookupResultVM[]> {
    return this.service.QueryAsync({
      TypeName: this.lookup.TypeName,
      DisplayField: this.lookup.DisplayField,
      ValueField: this.lookup.ValueField,
      Filter: keyword
    });
  }

  onChange = (_: any) => { };
  onTouched = () => { };

  writeValue(obj: any): void {
    if (obj !== undefined) {
      this._value = obj;
    }
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
  typeaheadOnSelect(e: TypeaheadMatch) {
    this.currentItem = e.item;
    this.value = this.currentItem.Value;
  }
}
