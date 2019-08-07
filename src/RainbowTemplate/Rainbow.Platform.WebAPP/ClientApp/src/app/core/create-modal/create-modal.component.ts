import { Component, OnInit, Input, Output, EventEmitter, TemplateRef, ViewChild } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';

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

  constructor(private modalService: BsModalService, public formBuilder: FormBuilder) { }

  ngOnInit() {
  }
  openCreateModal() {
    const formFields = {};
    this.fields
      .forEach((field: Rainbow.ViewModels.FieldDisplayVM) => {
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

}
