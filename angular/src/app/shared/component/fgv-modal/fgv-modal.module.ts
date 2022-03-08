import { CommonModule } from '@angular/common';
import { FgvModalComponent } from './fgv-modal.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap';
import { NgModule } from '@angular/core';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ModalModule,
    ReactiveFormsModule
  ],
  declarations: [ FgvModalComponent ],
	exports: [ FgvModalComponent ],
	bootstrap: [ FgvModalComponent ]
})
export class FgvModalModule { }
