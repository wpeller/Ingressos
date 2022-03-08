import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RecursosFavoritosComponent } from './recursos-favoritos.component';

describe('RecursosFavoritosComponent', () => {
  let component: RecursosFavoritosComponent;
  let fixture: ComponentFixture<RecursosFavoritosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RecursosFavoritosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RecursosFavoritosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
