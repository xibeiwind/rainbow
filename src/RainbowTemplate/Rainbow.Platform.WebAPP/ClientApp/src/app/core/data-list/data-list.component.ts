import { Component, OnInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';
import { SwalComponent } from '@sweetalert2/ngx-sweetalert2';

import { EditModalComponent } from '../edit-modal/edit-modal.component';
import { CreateModalComponent } from '../create-modal/create-modal.component';
import { EnumDisplayService } from '../../services/EnumDisplayService';

@Component({
  selector: 'app-data-list',
  templateUrl: './data-list.component.html',
  styleUrls: ['./data-list.component.scss']
})
export class DataListComponent implements OnInit {

  private _fields: Rainbow.ViewModels.FieldDisplayVM[];

  @Input()
  get fields(): Rainbow.ViewModels.FieldDisplayVM[] {
    return this._fields;
  }

  set fields(data: Rainbow.ViewModels.FieldDisplayVM[]) {
    this._fields = data;
    data.forEach(field => {
      if (field.IsEnum) {
        this.enumService.GetEnumDisplay(field.FieldType).subscribe(res => {
          this.enumObj[field.Name] = {};
          res.Data.Fields.forEach(f => {
            this.enumObj[field.Name][f.Value] = f;
          });
        });
      }
    });
  }

  @Input()
  canCreate: boolean;
  @Input()
  canEdit: boolean;
  @Input()
  canDelete: boolean;

  @Input()
  items: any[];

  @Input()
  enableSelect: boolean;
  @Input()
  currentItem: any;
  private selectIdObj: any = {};

  @Input()
  modelDisplayName: string;

  @Output()
  oncreate: EventEmitter<any> = new EventEmitter<any>();
  @Output()
  onupdate: EventEmitter<any> = new EventEmitter<any>();
  @Output()
  ondelete: EventEmitter<string> = new EventEmitter<string>();

  @Output()
  onrefresh: EventEmitter<any> = new EventEmitter<any>();

  @ViewChild('createModal')
  createModal: CreateModalComponent;
  @ViewChild('editModal')
  editModal: EditModalComponent;
  @ViewChild('deleteSwal')
  deleteSwal: SwalComponent;
  editFields: Rainbow.ViewModels.FieldDisplayVM[];
  private deleteItemId: string;
  get createTitle(): string { return `创建${this.modelDisplayName}`; }
  get editTitle(): string { return `更新${this.modelDisplayName}`; }
  createFields: Rainbow.ViewModels.FieldDisplayVM[];
  enumObj = {};


  constructor(private enumService: EnumDisplayService) { }

  ngOnInit() {
  }

  createItem(item: any) {
    this.oncreate.next(item);
  }

  closeCreateModal() {
    this.createModal.hide();
  }

  openEditModal(item: any) {
    // this.editTitle = `更新${this.modelDisplayName}`;

    this.editModal.openEditModal(item);
  }

  updteItem(value: any) {
    this.onupdate.next(value);
  }

  closeEditModal() {
    this.editModal.hide();
  }

  showDeleteSwal(item: any) {
    this.deleteItemId = item.Id;
    this.deleteSwal.show();
  }
  deleteItem() {
    this.ondelete.next(this.deleteItemId);
    this.deleteItemId = null;
  }

  getSelectedIds(): string[] {
    const ids = [];
    for (const key in this.selectIdObj) {
      if (this.selectIdObj.hasOwnProperty(key)) {
        const value = this.selectIdObj[key];
        if (value === true) {
          ids.push(key);
        }
      }
    }
    return ids;
  }

  refreshList() {
    this.onrefresh.next();
  }
}
