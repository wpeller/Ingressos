import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MaisVisitadosComponent } from './mais-visitados.component';

describe('MaisVisitadosComponent', () => {
  let component: MaisVisitadosComponent;
  let fixture: ComponentFixture<MaisVisitadosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MaisVisitadosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MaisVisitadosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
