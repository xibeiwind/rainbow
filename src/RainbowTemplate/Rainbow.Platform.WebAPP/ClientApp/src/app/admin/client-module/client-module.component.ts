import { Component, OnInit, ViewChild } from '@angular/core';
import { EditableListViewComponent } from '../../EditableListViewComponent';
import { ViewModelDisplayService } from '../../services/ViewModelDisplayService';
import { BsModalService } from 'ngx-bootstrap';
import { FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ClientModuleService } from '../../services/ClientModuleService';
import { PagingDataListComponent } from '../../core/paging-data-list/paging-data-list.component';

@Component({
  selector: 'app-client-module',
  templateUrl: './client-module.component.html',
  styleUrls: ['./client-module.component.scss']
})
export class ClientModuleComponent
  extends EditableListViewComponent<Rainbow.ViewModels.ClientModules.ClientModuleVM>
  implements OnInit {

  @ViewChild('pagingDataList')
  pagingDataList: PagingDataListComponent;

  pagingData: Yunyong.Core.PagingList<Rainbow.ViewModels.ClientModules.ClientModuleVM>;
  queryOption: Rainbow.ViewModels.ClientModules.QueryClientModuleVM = {
    PageIndex: 1,
    PageSize: 10,
    OrderBys: []
  };

  constructor(
    private service: ClientModuleService,
    displayService: ViewModelDisplayService,
    modalService: BsModalService,
    formBuilder: FormBuilder,
    toastr: ToastrService
  ) {
    super(displayService, modalService, formBuilder, toastr);
    this.config = {
      modelType: 'ClientModuleVM',
      create: 'CreateClientModuleVM',
      update: 'UpdateClientModuleVM',
      detail: 'ClientModuleVM',
      query: 'QueryClientModuleVM',
    };
  }
  ngOnInit() {
    this._OnInit();
    this.pagingDataList.config = {
      canCreate: true,
      canEdit: true,
      canSelect: true,
      canDelete: true,
      pageSize: 10,
      maxSize: 5
    };
  }
  refreshList() {
    // this.service.GetListAsync().subscribe(res => {
    //   this.items = res;
    // });

    this.service.QueryAsync(this.queryOption).subscribe(res => {
      this.pagingData = res;
    });
  }

  querySubmit(query: Yunyong.Core.PagingQueryOption) {
    console.log(JSON.stringify(query));

    this.queryOption = { ...this.queryOption, ...query };
    this.refreshList();

  }
  createSubmit(data: any) {
    this.service.CreateAsync(data).subscribe(res => {
      if (res.Status === Yunyong.Core.AsyncTaskStatus.Success) {
        this.toastr.info(`${this.modelDisplayName}创建成功`);
        this.pagingDataList.closeCreateModal();
        this.refreshList();
      }
    }, err => {
      this.toastr.error(`${this.modelDisplayName}创建失败！`);
    });
  }
  editSubmit(data: any) {
    this.service.UpdateAsync(data).subscribe(res => {
      this.toastr.info(`${this.modelDisplayName}编辑成功!`);
        this.pagingDataList.closeEditModal();
      this.refreshList();

    }, error => {
      this.toastr.error('编辑失败!');
    });
  }
  deleteSubmit(itemId: string) {
    this.service.DeleteAsync({ Id: itemId }).subscribe(res => {
      if (res.Status === Yunyong.Core.AsyncTaskStatus.Success) {
        this.refreshList();
      }
    });
  }
  onFieldsUpdated(data: { name: string; fields: Rainbow.ViewModels.FieldDisplayVM[]; }) {
    if (data.name === 'Create') {
      this.pagingDataList.createFields = data.fields;
    }
    if (data.name === 'Display') {
      this.pagingDataList.listFields = data.fields;

    }
    if (data.name === 'Update') {
      this.pagingDataList.editFields = data.fields;
    }
    if (data.name === 'Query') {
      this.pagingDataList.queryFields = data.fields;
    }
  }}
