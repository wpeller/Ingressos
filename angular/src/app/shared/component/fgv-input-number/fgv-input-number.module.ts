import { CommonModule } from '@angular/common';
import { FgvInputNumberComponent } from './fgv-input-number.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { KeyFilterModule } from 'primeng/keyfilter';
import { NgModule } from '@angular/core';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        KeyFilterModule,
        ReactiveFormsModule
    ],
    declarations: [
        FgvInputNumberComponent
    ],
    exports: [
        FgvInputNumberComponent
    ],
    bootstrap: [
        FgvInputNumberComponent
    ]
})
export class FgvInputNumberModule { }
