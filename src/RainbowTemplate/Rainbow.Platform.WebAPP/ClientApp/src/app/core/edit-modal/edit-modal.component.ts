import { Component, OnInit, Input, Output, EventEmitter, ViewChild, TemplateRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { InputTypeService } from 'src/app/services/InputTypeService';
import { EnumDisplayService } from 'src/app/services/EnumDisplayService';

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
  @Input()
  largeModal: boolean;
  @Output()
  onsubmit: EventEmitter<any> = new EventEmitter<any>();
  @ViewChild('editTemplate')
  template: TemplateRef<any>;
  editItemId: string;
  private enumObj = {};

  constructor(
    private enumService: EnumDisplayService,
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
          this.enumService.GetEnumDisplay(field.FieldType).subscribe(res => {
            this.enumObj[field.Name] = res.Data.Fields;
          });
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
