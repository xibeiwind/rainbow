import { Component, EventEmitter, Input, OnInit, Output, QueryList, TemplateRef, ViewChild, ViewChildren } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { EnumCacheService } from 'src/app/services/EnumCacheService';
import { InputTypeService } from 'src/app/services/InputTypeService';
import { SelectImageFileComponent } from '../select-image-file/select-image-file.component';

@Component({
  selector: 'app-create-modal',
  templateUrl: './create-modal.component.html',
  styleUrls: ['./create-modal.component.scss']
})
export class CreateModalComponent implements OnInit {
  createForm: FormGroup;
  @Input()
  title: string;
  @Input()
  fields: Rainbow.ViewModels.FieldDisplayVM[];
  @Input()
  largeModal: boolean;

  @Output()
  onsubmit: EventEmitter<any> = new EventEmitter<any>();

  @ViewChild('createTemplate', { static: true })
  template: TemplateRef<any>;

  @ViewChildren(SelectImageFileComponent)
  files?: QueryList<SelectImageFileComponent>;

  protected createModalRef: BsModalRef;
  protected enumObj = {};

  constructor(
    private enumService: EnumCacheService,
    private modalService: BsModalService,
    private inputTypeService: InputTypeService,
    public formBuilder: FormBuilder) { }


  ngOnInit() {
  }
  openCreateModal(data?: any) {
    const formFields = {};
    this.fields
      .forEach((field: Rainbow.ViewModels.FieldDisplayVM) => {
        if (field.IsEnum) {
          this.enumObj[field.Name] = this.enumService.GetEnumDisplay(field.FieldType).Fields;
        }
        if (data != null && data.hasOwnProperty(field.Name)) {
          formFields[field.Name] = [data[field.Name], Validators.required];
        } else {
          formFields[field.Name] = [null, Validators.required];
        }
      });
    this.createForm = this.formBuilder.group(formFields);

    this.createModalRef = this.modalService.show(this.template, { ignoreBackdropClick: true, class: this.largeModal ? 'modal-lg' : '' });
  }

  createSubmit() {
    var data = { ...this.createForm.value };
    // Object.assign(data, this.getFiles());
    this.onsubmit.next(data);
  }
  public hide() {
    this.createModalRef.hide();
  }

  getInputControlType(field: Rainbow.ViewModels.FieldDisplayVM): string {
    return this.inputTypeService.getInputControlType(field);
  }
  getInputType(field: Rainbow.ViewModels.FieldDisplayVM): string {
    return this.inputTypeService.getInputType(field);
  }

  getFiles() {
    var result = {};
    this.files.forEach(file => {
      const data = file.getFileData();
      Object.assign(result, data);
    });
    return result;
  }
}
