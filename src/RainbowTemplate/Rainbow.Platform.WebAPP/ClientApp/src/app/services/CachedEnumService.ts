import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EnumDisplayService } from './EnumDisplayService';
@Injectable()
export class CachedEnumService {
  enumObj = {};
  constructor(private enumService: EnumDisplayService) { }

  public GetEnumDisplay(name: String): object {
    return null;
  }
}

