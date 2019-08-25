import { Component, OnInit, Input, ViewChild, Output, EventEmitter } from '@angular/core';
import { EnumDisplayService } from 'src/app/services/EnumDisplayService';
import { CreateModalComponent } from '../create-modal/create-modal.component';
import { EditModalComponent } from '../edit-modal/edit-modal.component';
import { SwalComponent } from '@sweetalert2/ngx-sweetalert2';
import { PagingDataListConfig } from './PagingDataListConfig';

@Component({
  selector: 'app-paging-data-list',
  templateUrl: './paging-data-list.component.html',
  styleUrls: ['./paging-data-list.component.scss']
})
export class PagingDataListComponent implements OnInit {
  private _fields: Rainbow.ViewModels.FieldDisplayVM[];
  private _pagingData: Yunyong.Core.PagingList<any> = {
    Data: [],
    PageIndex: 1,
    PageSize: 10,
    TotalCount: 0,
    TotalPage: 0
  };
  enumObj = {};
  queryVM: Yunyong.Core.QueryOption = { OrderBys: [] };
  deleteItemId: string;

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
          this.enumObj[field.Name][f.Value] = f;
        });
      }
    });
  }

  @Input()
  get pagingData(): Yunyong.Core.PagingList<any> {
    return this._pagingData;
  }
  set pagingData(value: Yunyong.Core.PagingList<any>) {
    this._pagingData = value || this._pagingData;
  }

  @ViewChild('createModal')
  createModal: CreateModalComponent;
  @ViewChild('editModal')
  editModal: EditModalComponent;
  @ViewChild('deleteSwal')
  deleteSwal: SwalComponent;
  @Input()
  modelDisplayName: string;

  @Output()
  oncreate: EventEmitter<any> = new EventEmitter<any>();
  @Output()
  onupdate: EventEmitter<any> = new EventEmitter<any>();
  @Output()
  ondelete: EventEmitter<string> = new EventEmitter<string>();

  @Output()
  onquery: EventEmitter<Yunyong.Core.PagingQueryOption> = new EventEmitter<Yunyong.Core.PagingQueryOption>();

  get createTitle(): string { return `创建${this.modelDisplayName}`; }
  get editTitle(): string { return `更新${this.modelDisplayName}`; }

  createFields: Rainbow.ViewModels.FieldDisplayVM[] = [];
  editFields: Rainbow.ViewModels.FieldDisplayVM[] = [];

  queryFields: Rainbow.ViewModels.FieldDisplayVM[] = [];






  config: PagingDataListConfig;
  // currentPage: number = 1;
  selectIdObj = {};


  paging: Yunyong.Core.PagingQueryOption;

  constructor(private enumService: EnumDisplayService) { }

  ngOnInit() {
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
    const vm: Yunyong.Core.PagingQueryOption = {
      ...this.queryVM,
      PageSize: this.config.pageSize,
      PageIndex: this.pagingData.PageIndex,
    };
    this.onquery.next(vm);
  }

  pageChanged(pageIndex) {
    this.refreshList();
  }



  querySubmit(queryVM: Yunyong.Core.QueryOption) {
    this.queryVM = queryVM;
    this.refreshList();
  }

}



