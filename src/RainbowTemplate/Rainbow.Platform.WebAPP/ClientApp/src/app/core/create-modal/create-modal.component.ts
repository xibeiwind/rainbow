import { Component, OnInit, Input, Output, EventEmitter, TemplateRef, ViewChild, ViewChildren, QueryList } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { InputTypeService } from 'src/app/services/InputTypeService';
import { EnumCacheService } from 'src/app/services/EnumCacheService';
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
  files!: QueryList<SelectImageFileComponent>;

  private createModalRef: BsModalRef;
  private enumObj = {};

  constructor(
    private enumService: EnumCacheService,
    private modalService: BsModalService,
    private inputTypeService: InputTypeService,
    public formBuilder: FormBuilder) { }


  ngOnInit() {
  }
  openCreateModal() {
    const formFields = {};
    this.fields
      .forEach((field: Rainbow.ViewModels.FieldDisplayVM) => {
        if (field.IsEnum) {
          this.enumObj[field.Name] = this.enumService.GetEnumDisplay(field.FieldType).Fields;
        }
        formFields[field.Name] = ['', Validators.required];
      });

    this.createForm = this.formBuilder.group(formFields);

    this.createModalRef = this.modalService.show(this.template, { ignoreBackdropClick: true, class: this.largeModal ? 'modal-lg' : '' });
  }

  createSubmit() {
    var data = { ...this.createForm.value };
    Object.assign(data, this.getFiles());

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
  private getFiles() {
    var result = {};
    this.files.forEach(file => {
      const data = file.getFileData();
      Object.assign(result, data);
    });
    return result;
  }
}
