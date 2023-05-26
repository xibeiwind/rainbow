import { Component, OnInit, Input, Output, EventEmitter, ViewChild, TemplateRef, AfterContentInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { EnumCacheService } from 'src/app/services/EnumCacheService';
import { InputTypeService } from 'src/app/services/InputTypeService';

@Component({
  selector: 'app-edit-form',
  templateUrl: './edit-form.component.html',
  styleUrls: ['./edit-form.component.scss']
})
export class EditFormComponent implements OnInit, AfterContentInit {
  protected editForm: FormGroup;
  protected enumObj = {};


  @Input()
  title: string;
  @Input()
  fields: Rainbow.ViewModels.FieldDisplayVM[];
  @Input()
  largeModal: boolean;
  @Output()
  onsubmit: EventEmitter<any> = new EventEmitter();
  editItemId: string;
  @Input()
  data: Yunyong.Core.ViewModels.VMBase | any = {};

  @Output()
  oncancel: EventEmitter<void> = new EventEmitter();
  @Input()
  showCancelButton: boolean = true;


  private isEdit = false;


  constructor(
    private enumService: EnumCacheService,
    private inputTypeService: InputTypeService,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit() {
  }
  ngAfterContentInit(): void {
    if (this.fields!.length > 0) {
      const formFields = {};
      this.fields
        .forEach((field: Rainbow.ViewModels.FieldDisplayVM) => {
          if (field.IsEnum) {
            this.enumObj[field.Name] = this.enumService.GetEnumDisplay(field.FieldType).Fields;
          }
          formFields[field.Name] = [this.data[field.Name], Validators.required];
        });

      this.editForm = this.formBuilder.group(formFields);
      this.editItemId = this.data.Id;
    }
  }

  editSubmit() {
    this.onsubmit.next({ Id: this.editItemId, ...this.editForm.value });
  }
  cancleEdit() {
    this.oncancel.next();
  }
  getInputControlType(field: Rainbow.ViewModels.FieldDisplayVM): string {
    return this.inputTypeService.getInputControlType(field);
  }
  getInputType(field: Rainbow.ViewModels.FieldDisplayVM): string {
    return this.inputTypeService.getInputType(field);
  }
}
