import { Component, OnInit, Input, Output, EventEmitter, ViewChild, TemplateRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';

@Component({
  selector: 'app-edit-modal',
  templateUrl: './edit-modal.component.html',
  styleUrls: ['./edit-modal.component.scss']
})
export class EditModalComponent implements OnInit {
  editForm: FormGroup;
  private editModalRef: BsModalRef;
  @Input()
  title: string;
  @Input()
  fields: Rainbow.ViewModels.FieldDisplayVM[];

  @Output()
  onsubmit: EventEmitter<any> = new EventEmitter<any>();
  @ViewChild('editTemplate')
  template: TemplateRef<any>;
  editItemId: string;


  constructor(private modalService: BsModalService, public formBuilder: FormBuilder) { }

  ngOnInit() {
  }

  openEditModal(data: any) {
    const formFields = {};
    this.fields
      .forEach((field: Rainbow.ViewModels.FieldDisplayVM) => {
        formFields[field.Name] = [data[field.Name], Validators.required];
      });

    this.editForm = this.formBuilder.group(formFields);
    this.editItemId = data.Id;
    this.editModalRef = this.modalService.show(this.template, { ignoreBackdropClick: true });

  }
  editSubmit() {
    this.onsubmit.next({ Id: this.editItemId, ...this.editForm.value });
  }
  public hide() {
    this.editModalRef.hide();
  }
}