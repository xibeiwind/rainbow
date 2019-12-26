import { Component, EventEmitter, Input, OnInit, Output, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { EnumCacheService } from 'src/app/services/EnumCacheService';
import { InputTypeService } from 'src/app/services/InputTypeService';

@Component({
  selector: 'app-edit-modal',
  templateUrl: './edit-modal.component.html',
  styleUrls: ['./edit-modal.component.scss']
})
export class EditModalComponent implements OnInit {
  editForm: FormGroup;
  protected editModalRef: BsModalRef;
  @Input()
  title: string;
  @Input()
  fields: Rainbow.ViewModels.FieldDisplayVM[];
  @Input()
  largeModal: boolean;
  @Output()
  onsubmit: EventEmitter<any> = new EventEmitter<any>();
  @ViewChild('editTemplate', { static: true })
  template: TemplateRef<any>;
  editItemId: string;
  protected enumObj = {};

  constructor(
    private enumService: EnumCacheService,
    private modalService: BsModalService,
    private inputTypeService: InputTypeService,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit() {
  }

  openEditModal(data: any) {
    const formFields = {};
    this.fields
      .forEach((field: Rainbow.ViewModels.FieldDisplayVM) => {
        if (field.IsEnum) {
          this.enumObj[field.Name] = this.enumService.GetEnumDisplay(field.FieldType).Fields;
        }
        formFields[field.Name] = [data[field.Name], Validators.required];
      });

    this.editForm = this.formBuilder.group(formFields);
    this.editItemId = data.Id;
    this.editModalRef = this.modalService.show(this.template, { ignoreBackdropClick: true, class: this.largeModal ? 'modal-lg' : '' });

  }
  editSubmit() {
    this.onsubmit.next({ Id: this.editItemId, ...this.editForm.value });
  }
  public hide() {
    this.editModalRef.hide();
  }
  getInputControlType(field: Rainbow.ViewModels.FieldDisplayVM): string {
    return this.inputTypeService.getInputControlType(field);
  }
  getInputType(field: Rainbow.ViewModels.FieldDisplayVM): string {
    return this.inputTypeService.getInputType(field);
  }
}
