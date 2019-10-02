import { ViewModelDisplayService } from './services/ViewModelDisplayService';
import { ViewChild } from '@angular/core';
import { ItemDetailComponent } from './core/item-detail/item-detail.component';

export abstract class DetailViewComponent<ModelVM> {
    fields: Rainbow.ViewModels.FieldDisplayVM[];
    modelDisplayName: string;
    detailModelType: string;
    item: ModelVM;
    @ViewChild(ItemDetailComponent, { static: true })
    detailView: ItemDetailComponent;

    constructor(protected displayService: ViewModelDisplayService) {
    }
    protected _OnInit() {
        this.displayService.GetVMDisplay({ Name: this.detailModelType }).subscribe(res => {
            if (res.Status === Yunyong.Core.AsyncTaskStatus.Success) {
                this.modelDisplayName = res.Data.DisplayName;
                this.fields = res.Data.Fields;
                this.detailView.fields = this.fields;
                // this.detailView.item = this.item;
            }
        });
    }
}
