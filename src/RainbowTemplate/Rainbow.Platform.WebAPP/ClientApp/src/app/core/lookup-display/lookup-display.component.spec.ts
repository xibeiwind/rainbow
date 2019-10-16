import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LookupDisplayComponent } from './lookup-display.component';

describe('LookupDisplayComponent', () => {
  let component: LookupDisplayComponent;
  let fixture: ComponentFixture<LookupDisplayComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LookupDisplayComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LookupDisplayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
