import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IdentityManagerLoginComponent } from './identity-manager-login.component';

describe('IdentityManagerLoginComponent', () => {
  let component: IdentityManagerLoginComponent;
  let fixture: ComponentFixture<IdentityManagerLoginComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IdentityManagerLoginComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IdentityManagerLoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
