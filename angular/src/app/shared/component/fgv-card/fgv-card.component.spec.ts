/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { FgvCardComponent } from './fgv-card.component';

describe('FgvCardComponent', () => {
  let component: FgvCardComponent;
  let fixture: ComponentFixture<FgvCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FgvCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FgvCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
