import { Component, OnInit, Input, Output, EventEmitter, TemplateRef, ViewChild } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { EnumDisplayService } from '../../services/EnumDisplayService';
import { InputTypeService } from 'src/app/services/InputTypeService';

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

  @ViewChild('createTemplate')
  template: TemplateRef<any>;

  private createModalRef: BsModalRef;
  private enumObj = {};

  constructor(
    private enumService: EnumDisplayService,
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
          this.enumService.GetEnumDisplay(field.FieldType).subscribe(res => {
            this.enumObj[field.Name] = res.Data.Fields;
          });
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
    if (field.IsEnum) {
      return 'select';
    }
    if (field.DataType === System.ComponentModel.DataAnnotations.DataType.Html ||
      field.DataType === System.ComponentModel.DataAnnotations.DataType.MultilineText) {
      return 'html';
    } else {
      return 'input';
    }
  }
  getInputType(field: Rainbow.ViewModels.FieldDisplayVM): string {
    return this.inputTypeService.getInputType(field);
  }
}
