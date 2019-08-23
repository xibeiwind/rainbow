import { Component, OnInit, ViewChild } from '@angular/core';
import { EditableListViewComponent } from '../../EditableListViewComponent';
import { ViewModelDisplayService } from '../../services/ViewModelDisplayService';
import { BsModalService } from 'ngx-bootstrap';
import { FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { RoleInfoService } from '../../services/RoleInfoService';
import { DataListComponent } from '../../core/data-list/data-list.component';

@Component({
  selector: 'app-role-info',
  templateUrl: './role-info.component.html',
  styleUrls: ['./role-info.component.scss']
})
export class RoleInfoComponent
  extends EditableListViewComponent<Rainbow.ViewModels.RoleInfos.RoleInfoVM>
  implements OnInit {
  @ViewChild('dataList')
  dataList: DataListComponent;

  constructor(
    private service: RoleInfoService,
    displayService: ViewModelDisplayService,
    modalService: BsModalService,
    formBuilder: FormBuilder,
    toastr: ToastrService
  ) {
    super(displayService, modalService, formBuilder, toastr);
    this.config = {
      modelType: 'RoleInfoVM',
      create: 'CreateRoleInfoVM',
      update: 'UpdateRoleInfoVM',
      detail: 'RoleInfoVM',
      query: 'QueryRoleInfoVM',
    }
  }
  ngOnInit() {
    this._OnInit();
  }
  refreshList() {
    this.service.GetListAsync().subscribe(res => {
      this.items = res;
    });
  }
  createSubmit(data: any) {
    this.service.CreateAsync(data).subscribe(res => {
      if (res.Status === Yunyong.Core.AsyncTaskStatus.Success) {
        this.toastr.info(`${this.modelDisplayName}创建成功`);
        this.refreshList();
        this.dataList.closeCreateModal();
      }
    }, err => {
      this.toastr.error(`${this.modelDisplayName}创建失败！`);
    });
  }
  editSubmit(data: any) {
    this.service.UpdateAsync(data).subscribe(res => {
      this.toastr.info(`${this.modelDisplayName}编辑成功!`);
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
      this.dataList.createFields = data.fields;
    }
    if (data.name === 'Display') {
      this.dataList.fields = data.fields;
      this.dataList.items = this.items;
    }
    if (data.name === 'Update') {
      this.dataList.editFields = data.fields;
    }
  }

}
