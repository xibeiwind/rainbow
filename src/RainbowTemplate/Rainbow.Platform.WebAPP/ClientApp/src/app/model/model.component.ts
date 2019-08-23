import { Component, OnInit } from '@angular/core';
import { ModelService } from '../services/ModelService';
import { ToastrService } from 'ngx-toastr';
import { ViewModelDisplayService } from '../services/ViewModelDisplayService';

@Component({
  selector: 'app-model',
  templateUrl: './model.component.html',
  styleUrls: ['./model.component.scss']
})
export class ModelComponent implements OnInit {
  models: Rainbow.ViewModels.Models.ModelTypeVM[];
  currentModel: Rainbow.ViewModels.Models.ModelTypeVM;
  folderName: string;
  createVMs: Rainbow.ViewModels.Models.CreateViewModelApplyVM[] = [];
  enableDelete: boolean;
  generateService: boolean;
  generateController: boolean;
  generateNgModuleComponent: boolean;
  updateTsServices: boolean;
  vmTypes: { name: string, value: Rainbow.Common.Enums.VMType }[] = [
    { name: '创建', value: Rainbow.Common.Enums.VMType.Create },
    { name: '更新', value: Rainbow.Common.Enums.VMType.Update },
    { name: '显示', value: Rainbow.Common.Enums.VMType.Display },
    { name: '查询', value: Rainbow.Common.Enums.VMType.Query },
  ];
  currentDisplayViewModels: Rainbow.ViewModels.ViewModelDisplayVM[];
  ngModuleName: string = 'Admin';
  generateVM: boolean;
  isNgModelListComponent: boolean;

  constructor(private service: ModelService,
    private displayService: ViewModelDisplayService,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.service.GetModelTypes().subscribe(res => {
      this.models = res;
    });
  }

  selectModel(model: Rainbow.ViewModels.Models.ModelTypeVM) {
    if (this.currentModel !== model) {
      this.currentModel = model;
      this.folderName = `${model.Name}s`;
      this.createVMs = [];
      this.enableDelete = false;

      this.displayService.GetModelVMDisplays({ Name: model.Name }).subscribe(res => {
        this.currentDisplayViewModels = res.Data.ViewModels;
        res.Data.ViewModels.filter(a => a.Type !== Rainbow.Common.Enums.VMType.Delete).forEach(item => {
          const vm: Rainbow.ViewModels.Models.CreateViewModelApplyVM = {
            Type: item.Type,
            DisplayName: this.getDisplayName(item),
            Name: item.Name,
            ActionName: this.getActionName(item),
            Fields: item.Fields.map(f => f.Name),
          };
          vm['field'] = {};
          item.Fields.forEach(f => {
            vm['field'][f.Name] = true;
          });
          this.changeVmType(vm);

          this.createVMs.push(vm);
        });
      });
    }
  }
  getDisplayName(item: Rainbow.ViewModels.ViewModelDisplayVM): string {
    return item.DisplayName;
  }
  getActionName(item: Rainbow.ViewModels.ViewModelDisplayVM): string {
    switch (item.Type) {
      case Rainbow.Common.Enums.VMType.Create:
        return 'Create';
      case Rainbow.Common.Enums.VMType.Update:
        return 'Update';
      case Rainbow.Common.Enums.VMType.Delete:
        return 'Delete';
      case Rainbow.Common.Enums.VMType.Display:
        return '';
      case Rainbow.Common.Enums.VMType.Query:
        return 'Query';

      default:
        return '';
    }

  }

  createNewVM(typeStr: string = 'Create') {
    const typeObj = {
      'Create': Rainbow.Common.Enums.VMType.Create,
      'Update': Rainbow.Common.Enums.VMType.Update,
      'Display': Rainbow.Common.Enums.VMType.Display,
      'Query': Rainbow.Common.Enums.VMType.Query,
    };
    let vm: Rainbow.ViewModels.Models.CreateViewModelApplyVM = {
      Type: typeObj[typeStr],
      DisplayName: '',
      Name: '',
      ActionName: '',
      Fields: [],
    };
    vm['field'] = {};
    this.changeVmType(vm);
    this.createVMs.push(vm);
  }

  changeVmType(vm: Rainbow.ViewModels.Models.CreateViewModelApplyVM) {
    const nameObj = {};
    nameObj[Rainbow.Common.Enums.VMType.Create] = 'Create';
    nameObj[Rainbow.Common.Enums.VMType.Update] = 'Update';
    nameObj[Rainbow.Common.Enums.VMType.Display] = '';
    nameObj[Rainbow.Common.Enums.VMType.Query] = 'Query';
    const displayNameObj = {};
    displayNameObj[Rainbow.Common.Enums.VMType.Create] = '创建';
    displayNameObj[Rainbow.Common.Enums.VMType.Update] = '更新';
    displayNameObj[Rainbow.Common.Enums.VMType.Display] = '显示';
    displayNameObj[Rainbow.Common.Enums.VMType.Query] = '查询';

    vm.DisplayName = `${displayNameObj[vm.Type]}${this.currentModel.DisplayName}`;
    vm.Name = `${nameObj[vm.Type]}${this.currentModel.Name}VM`;

    vm.ActionName = nameObj[vm.Type];
  }

  deleteVM(vm: Rainbow.ViewModels.Models.CreateViewModelApplyVM) {
    this.createVMs = this.createVMs.filter(a => a !== vm);
  }

  submitCreateVMs() {
    this.createVMs.forEach((a) => {
      a.Fields = [];
      for (const key of Object.keys(a['field'])) {
        if (a['field'].hasOwnProperty(key)) {
          a.Fields.push(key);
        }
      }
    });

    const vm: Rainbow.ViewModels.Models.CreateModelSuitApplyVM = {
      ModelName: this.currentModel.Name,
      ModelFullName: this.currentModel.FullName,
      FolderName: this.folderName,
      EnableDelete: this.enableDelete,
      GenerateVM: this.generateVM,
      NgModuleName: this.ngModuleName,
      GenerateService: this.generateService,
      GenerateController: this.generateController,
      GenerateNgModuleComponent: this.generateNgModuleComponent,
      IsNgModelListComponent: this.isNgModelListComponent,
      UpdateTsServices: this.updateTsServices,
      Items: this.createVMs.map((a) => {
        return {
          ActionName: a.ActionName,
          Name: a.Name,
          DisplayName: a.DisplayName,
          Fields: a.Fields,
          Type: a.Type
        };
      }),
    };

    console.log(JSON.stringify(vm));
    this.service.CreateUpdateFiles(vm).subscribe(res => {
      console.log(res);
      this.toastr.success('创建成功');
      this.currentModel = null;
    }, err => {
      console.log(JSON.stringify(err));
      this.toastr.error('出错了');
    });
  }

  regenerateTsCode() {
    this.service.RegenerateTsCode().subscribe(res => {
      this.toastr.success('代码刷新完毕！');
    });
  }

}
