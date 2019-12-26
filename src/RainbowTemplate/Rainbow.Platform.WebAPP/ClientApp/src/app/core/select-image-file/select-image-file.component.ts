import { Component, ElementRef, forwardRef, Input, ViewChild } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

export const EXE_SELECT_IMAGE_FILE_ACCESSOR: any = {
  provide: NG_VALUE_ACCESSOR,
  useExisting: forwardRef(() => SelectImageFileComponent),
  multi: true
}

@Component({
  selector: 'app-select-image-file',
  templateUrl: './select-image-file.component.html',
  styleUrls: ['./select-image-file.component.scss'],
  providers:[EXE_SELECT_IMAGE_FILE_ACCESSOR]
})
export class SelectImageFileComponent implements ControlValueAccessor {
  isDisabled: boolean;

  onChange = (_: any) => { };
  onTouched = () => { };

  writeValue(obj: any): void {
    if (obj !== undefined) {
      this._value = obj;
      this.fileResult = obj;
    }
  }
  registerOnChange(fn: any): void {
    this.onChange = fn;
  }
  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
    // throw new Error("Method not implemented.");
    this.isDisabled = isDisabled;
  }

  fileResult: string | ArrayBuffer;

  @Input('ngModel')
  fieldName: string;
  @Input()
  showImg: boolean = true;

  private _value: any = null;

  get value(): any {
    return this._value;
  }
  set value(val: any) {
    this._value = val;
    this.onChange(val);
    this.onTouched();
  }


  @ViewChild('file', { static: true })
  file: ElementRef;

  constructor() { }

  fileChange(event) {
    if (!this.showImg) {
      return;
    }
    var reader = new FileReader();
    reader.onload = (res) => {
      this.fileResult = res.target['result'];
    };
    reader.readAsDataURL(event.target.files[0]);
    this.value = event.target.files[0];
  }
  getFileData() {
    var result = {};
    if (this.file.nativeElement.files.length) {
      result[this.fieldName] = this.file.nativeElement.files[0];
    }
    return result;
  }
}
