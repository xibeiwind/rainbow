import { Component, OnInit, Input, Output, EventEmitter, TemplateRef, ViewChild } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { EnumDisplayService } from '../../services/EnumDisplayService';

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

  @Output()
  onsubmit: EventEmitter<any> = new EventEmitter<any>();

  @ViewChild('createTemplate')
  template: TemplateRef<any>;

  private createModalRef: BsModalRef;
  private enumObj = {};

  inputType = {

  };

  constructor(
    private enumService: EnumDisplayService,
    private modalService: BsModalService,
    public formBuilder: FormBuilder) {
    this.inputType[System.ComponentModel.DataAnnotations.DataType.DateTime] = 'date';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.Date] = 'datetime';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.Time] = 'time';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.Duration] = 'range';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.PhoneNumber] = 'tel';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.Currency] = 'text';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.Text] = 'text';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.Html] = 'text';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.MultilineText] = 'text';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.EmailAddress] = 'email';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.Password] = 'password';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.Url] = 'url';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.ImageUrl] = 'image';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.CreditCard] = 'text';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.PostalCode] = 'text';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.Upload] = 'file';

    this.inputType['text'] = 'text';
    this.inputType['number'] = 'number';
    this.inputType['checkbox'] = 'checkbox';

  }

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
    this.createModalRef = this.modalService.show(this.template, { ignoreBackdropClick: true });
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
    if (field.DataType === System.ComponentModel.DataAnnotations.DataType.MultilineText) {
      return 'textarea';
    } else {
      return 'input';
    }
  }

  getInputType(field: Rainbow.ViewModels.FieldDisplayVM): string {
    if (this.inputType.hasOwnProperty(field.DataType)) {
      return this.inputType[field.DataType];
    }
    if (this.inputType.hasOwnProperty(field.FieldType)) {
      return this.inputType[field.FieldType];
    }

    return 'text';
  }



}
