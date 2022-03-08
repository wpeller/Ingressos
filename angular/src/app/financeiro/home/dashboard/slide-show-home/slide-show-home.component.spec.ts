import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SlideShowHomeComponent } from './slide-show-home.component';

describe('SlideShowHomeComponent', () => {
  let component: SlideShowHomeComponent;
  let fixture: ComponentFixture<SlideShowHomeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SlideShowHomeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SlideShowHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
