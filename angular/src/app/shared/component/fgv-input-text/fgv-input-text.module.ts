import { CommonModule } from '@angular/common';
import { FgvInputTextComponent } from './fgv-input-text.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule
    ],
	declarations: [ FgvInputTextComponent ],
	exports: [ FgvInputTextComponent ],
	bootstrap: [ FgvInputTextComponent ],
})
export class FgvInputTextModule {}
