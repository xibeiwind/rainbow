import { FormBuilder } from '@angular/forms';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { EditableModelListConfig } from './EditableModelListConfig';
import { ViewModelDisplayService } from './services/ViewModelDisplayService';
export abstract class EditableListViewComponent<ModelVM> {
  config: EditableModelListConfig;
  fieldGroup = {};
  modelDisplayName: string;
  items: ModelVM[];
  editItemId: string;



  constructor(
    protected displayService: ViewModelDisplayService,
    protected modalService: BsModalService,
    protected formBuilder: FormBuilder,
    protected toastr: ToastrService) {
  }
  protected _OnInit() {
    this.displayService.GetVMDisplay({ Name: this.config.modelType }).subscribe(res => {
      if (res.Status === Yunyong.Core.AsyncTaskStatus.Success) {
        this.modelDisplayName = res.Data.DisplayName;
      }
    });

    if (this.config.create !== undefined) {
      this.getFieldsAsync(Rainbow.Common.Enums.VMType.Create, 'Create', this.config.create);
    }
    if (this.config.update !== undefined) {
      this.getFieldsAsync(Rainbow.Common.Enums.VMType.Update, 'Update', this.config.update);
    }
    this.getFieldsAsync(Rainbow.Common.Enums.VMType.ListDisplay, 'List', this.config.list);
    this.getFieldsAsync(Rainbow.Common.Enums.VMType.Query, 'Query', this.config.query);

    this.refreshList();
  }

  abstract refreshList();

  abstract createSubmit(data: any);

  abstract editSubmit(data: any);
  abstract deleteSubmit(itemId: string);

  abstract onFieldsUpdated(data: { name: string, fields: Rainbow.ViewModels.FieldDisplayVM[] });

  private getFieldsAsync(type: Rainbow.Common.Enums.VMType, fieldName: string, typeName: string) {
    this.displayService.GetVMDisplay({ Name: typeName }).subscribe(res => {
      if (res.Status === Yunyong.Core.AsyncTaskStatus.Success) {
        this.fieldGroup[fieldName] = {
          type: type,
          fields: res.Data.Fields
        };
        this.onFieldsUpdated({
          name: fieldName,
          fields: res.Data.Fields
        });
      }
    });
  }
}


