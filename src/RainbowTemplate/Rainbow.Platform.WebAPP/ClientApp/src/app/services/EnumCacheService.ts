import { Injectable, Inject } from '@angular/core';
import { EnumDisplayService } from './EnumDisplayService';

@Injectable()
export class EnumCacheService {
  enumObj = {};
  constructor(serevice: EnumDisplayService) {
    serevice.GetEnumDisplayList().subscribe(res => {
      res.Data.forEach(item => {
        this.enumObj[item.Name] = item;
      });
    });
  }
  public GetEnumDisplay(name: string)
    : Rainbow.ViewModels.EnumDisplayVM {
    if (this.enumObj.hasOwnProperty(name)) {
      return this.enumObj[name];
    } else {
      return undefined;
    }
  }
}
