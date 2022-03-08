import { CommonModule } from '@angular/common';
import { FgvInputMultiselectComponent } from './fgv-input-multiselect.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MultiSelectModule } from 'primeng/multiselect';
import { NgModule } from '@angular/core';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        MultiSelectModule,
        ReactiveFormsModule
    ],
    declarations: [FgvInputMultiselectComponent],
    exports: [FgvInputMultiselectComponent],
    bootstrap: [FgvInputMultiselectComponent]
})
export class FgvInputMultiselectModule { }
