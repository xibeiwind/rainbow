import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectImageFileComponent } from './select-image-file.component';

describe('SelectImageFileComponent', () => {
  let component: SelectImageFileComponent;
  let fixture: ComponentFixture<SelectImageFileComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SelectImageFileComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SelectImageFileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
