import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BreadCrumbDefaultComponent } from './bread-crumb-default.component';

describe('BreadCrumbDefaultComponent', () => {
  let component: BreadCrumbDefaultComponent;
  let fixture: ComponentFixture<BreadCrumbDefaultComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BreadCrumbDefaultComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BreadCrumbDefaultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
