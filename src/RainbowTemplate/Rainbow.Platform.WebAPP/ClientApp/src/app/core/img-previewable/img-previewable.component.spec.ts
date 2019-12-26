import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ImgPreviewableComponent } from './img-previewable.component';

describe('ImgPreviewableComponent', () => {
  let component: ImgPreviewableComponent;
  let fixture: ComponentFixture<ImgPreviewableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ImgPreviewableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ImgPreviewableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
