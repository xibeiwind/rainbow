import { Component, OnInit, Input, Output, EventEmitter, TemplateRef, ViewChild } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { InputTypeService } from 'src/app/services/InputTypeService';
import { EnumCacheService } from 'src/app/services/EnumCacheService';

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
    this.onsubmit.next({ ...this.createForm.value });
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
}
