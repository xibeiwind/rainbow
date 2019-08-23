import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PagingDataListComponent } from './paging-data-list.component';

describe('PagingDataListComponent', () => {
  let component: PagingDataListComponent;
  let fixture: ComponentFixture<PagingDataListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PagingDataListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PagingDataListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
