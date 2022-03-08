import { CommonModule } from '@angular/common';
import { FgvButtonIncluirComponent } from './fgv-button-incluir.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
    ],
    declarations: [FgvButtonIncluirComponent],
    exports: [FgvButtonIncluirComponent],
    bootstrap: [FgvButtonIncluirComponent],
})
export class FgvButtonIncluirModule { }
