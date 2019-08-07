import { Component, OnInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';
import { SwalComponent } from '@sweetalert2/ngx-sweetalert2';

import { EditModalComponent } from '../edit-modal/edit-modal.component';
import { CreateModalComponent } from '../create-modal/create-modal.component';

@Component({
  selector: 'app-data-list',
  templateUrl: './data-list.component.html',
  styleUrls: ['./data-list.component.scss']
})
export class DataListComponent implements OnInit {

  @Input()
  fields: Rainbow.ViewModels.FieldDisplayVM[];
  @Input()
  canEdit: boolean;
  @Input()
  canDelete: boolean;

  @Input()
  items: any[];

  @Input()
  enableSelect: boolean;
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
  createTitle: string = `创建${this.modelDisplayName}`;
  editTitle: string = `更新${this.modelDisplayName}`;
  createFields: Rainbow.ViewModels.FieldDisplayVM[];



  constructor() { }

  ngOnInit() {
  }

  createItem(item: any) {
    this.oncreate.next(item);
  }

  closeCreateModal() {
    this.createModal.hide();
  }

  openEditModal(item: any) {
    this.editTitle = `更新${this.modelDisplayName}`;

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
