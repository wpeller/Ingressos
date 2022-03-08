import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlanosComExcessoDeParcelamentoComponent } from './planos-com-excesso-de-parcelamento.component';

describe('PlanosComExcessoDeParcelamentoComponent', () => {
  let component: PlanosComExcessoDeParcelamentoComponent;
  let fixture: ComponentFixture<PlanosComExcessoDeParcelamentoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlanosComExcessoDeParcelamentoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlanosComExcessoDeParcelamentoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
