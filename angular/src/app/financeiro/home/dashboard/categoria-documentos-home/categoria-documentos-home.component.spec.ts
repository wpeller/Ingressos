import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoriaDocumentosHomeComponent } from './categoria-documentos-home.component';

describe('CategoriaDocumentosHomeComponent', () => {
  let component: CategoriaDocumentosHomeComponent;
  let fixture: ComponentFixture<CategoriaDocumentosHomeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CategoriaDocumentosHomeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CategoriaDocumentosHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
