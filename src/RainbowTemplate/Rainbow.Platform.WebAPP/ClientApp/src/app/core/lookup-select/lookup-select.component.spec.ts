import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LookupSelectComponent } from './lookup-select.component';

describe('LookupSelectComponent', () => {
  let component: LookupSelectComponent;
  let fixture: ComponentFixture<LookupSelectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LookupSelectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LookupSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
