import { CommonModule } from '@angular/common';
import { FgvInputPasswordComponent } from './fgv-input-password.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { PasswordModule } from 'primeng/primeng';
import {
    BsDropdownModule,
} from 'ngx-bootstrap';

@NgModule({
    imports: [
        BsDropdownModule,
        CommonModule,
        FormsModule,
        PasswordModule,
        ReactiveFormsModule
    ],
	declarations: [ FgvInputPasswordComponent ],
	exports: [ FgvInputPasswordComponent ],
	bootstrap: [ FgvInputPasswordComponent ],
})
export class FgvInputPasswordModule {}
