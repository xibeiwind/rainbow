import { Component, OnInit, Input, ViewChild, ElementRef } from '@angular/core';

@Component({
  selector: 'app-select-image-file',
  templateUrl: './select-image-file.component.html',
  styleUrls: ['./select-image-file.component.scss']
})
export class SelectImageFileComponent implements OnInit {

  fileResult: string | ArrayBuffer;

  @Input('formControlName')
  fieldName: string;
  @Input()
  showImg: boolean = true;

  @ViewChild('file', { static: true })
  file: ElementRef;

  constructor() { }

  ngOnInit() {
  }

  fileChange(event) {
    if (!this.showImg) {
      return;
    }
    var reader = new FileReader();
    reader.onload = (res) => {

      this.fileResult = res.target['result'];
    };

    reader.readAsDataURL(event.target.files[0]);
  }
  getFileData() {
    var result = {};
    if (this.file.nativeElement.files.length) {
      result[this.fieldName] = this.file.nativeElement.files[0];
    }
    return result;
  }
}
