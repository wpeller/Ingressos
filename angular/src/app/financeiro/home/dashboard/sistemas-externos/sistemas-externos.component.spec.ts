import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SistemasExternosComponent } from './sistemas-externos.component';

describe('SistemasExternosComponent', () => {
  let component: SistemasExternosComponent;
  let fixture: ComponentFixture<SistemasExternosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SistemasExternosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SistemasExternosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
