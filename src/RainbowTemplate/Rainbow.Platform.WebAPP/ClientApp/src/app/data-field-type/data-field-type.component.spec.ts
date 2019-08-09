import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DataFieldTypeComponent } from './data-field-type.component';

describe('DataFieldTypeComponent', () => {
  let component: DataFieldTypeComponent;
  let fixture: ComponentFixture<DataFieldTypeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DataFieldTypeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DataFieldTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
