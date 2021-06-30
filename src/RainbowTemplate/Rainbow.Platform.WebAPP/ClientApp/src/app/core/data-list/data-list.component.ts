import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { SwalComponent } from '@sweetalert2/ngx-sweetalert2';
import { EnumCacheService } from 'src/app/services/EnumCacheService';
import { InputTypeService } from 'src/app/services/InputTypeService';
import { CreateModalComponent } from '../create-modal/create-modal.component';
import { EditModalComponent } from '../edit-modal/edit-modal.component';
import { DataListConfig } from './DataListConfig';

@Component({
  selector: 'app-data-list',
  templateUrl: './data-list.component.html',
  styleUrls: ['./data-list.component.scss']
})
export class DataListComponent implements OnInit {
  config: DataListConfig;
  selectIdObj = {};
  currentItem:any;
  _fields: Rainbow.ViewModels.FieldDisplayVM[];
  enumObj = {};
  deleteItemId: string;

  @Input()
  modelDisplayName: string;
  
  value: {};

  @Input()
  get listFields(): Rainbow.ViewModels.FieldDisplayVM[] {
    return this._fields;
  }
  set listFields(data: Rainbow.ViewModels.FieldDisplayVM[]) {
    this._fields = data;
    data.forEach(field => {
      if (field.IsEnum) {
        this.enumObj[field.Name] = {};
        this.enumService.GetEnumDisplay(field.FieldType).Fields.forEach(f => {
          this.enumObj[field.Name][f.Name] = f;
        });
      }
    });
  }

  @Input()
  data: any[];

  @ViewChild(CreateModalComponent, { static: true })
  createModal: CreateModalComponent;
  @ViewChild(EditModalComponent, { static: true })
  editModal: EditModalComponent;
  @ViewChild(SwalComponent, { static: true })
  deleteSwal: SwalComponent;
  @Output()
  oncreate: EventEmitter<any> = new EventEmitter<any>();
  @Output()
  onupdate: EventEmitter<any> = new EventEmitter<any>();
  @Output()
  ondelete: EventEmitter<string> = new EventEmitter<string>();

  @Output()
  onrefresh: EventEmitter<any> = new EventEmitter<any>();

  get createTitle(): string { return `创建${this.modelDisplayName}`; }
  get editTitle(): string { return `更新${this.modelDisplayName}`; }

  @Output()
  onextraaction: EventEmitter<any> = new EventEmitter<any>();
  
  createFields: Rainbow.ViewModels.FieldDisplayVM[] = [];
  editFields: Rainbow.ViewModels.FieldDisplayVM[] = [];

  constructor(
    private enumService: EnumCacheService,
    private inputTypeService: InputTypeService
  ) { }

  ngOnInit() {
  }

  getInputControlType(field: Rainbow.ViewModels.FieldDisplayVM): string {
    return this.inputTypeService.getInputControlType(field);
  }

  openCreateModal(){
    this.createModal.openCreateModal(this.value);
  }

  closeCreateModal() {
    this.createModal.hide();
  }
  closeEditModal() {
    this.editModal.hide();
  }

  openEditModal(data: any) {
    this.editModal.openEditModal(data);
  }

  showDeleteSwal(data: any) {
    this.deleteItemId = data.Id;
    this.deleteSwal.show();
  }



  createItem(vm: any) {
    const files = this.createModal.getFiles();
    Object.assign(vm, files);
    this.oncreate.next(vm);
  }

  updateItem(vm: any) {
    this.onupdate.next(vm);
  }
  deleteItem() {
    this.ondelete.next(this.deleteItemId);
    this.deleteItemId = null;
  }

  refreshList() {
    this.onrefresh.next(null);
  }

  extraAction(data: any) {
    this.onextraaction.next(data);
  }
  selectAllChange(val: any) {
    let checked = val.target.checked;
    this.selectIdObj = {};
    if (checked) {
      this.data.forEach(element => {
        this.selectIdObj[element.Id] = true;
      });
    }
  }
}


