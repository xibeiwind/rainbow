import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ControllerProjectComponent } from './controller-project.component';

describe('ControllerProjectComponent', () => {
  let component: ControllerProjectComponent;
  let fixture: ComponentFixture<ControllerProjectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ControllerProjectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ControllerProjectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
