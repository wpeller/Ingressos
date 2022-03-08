import { CommonModule } from '@angular/common';
import { FgvInputCapitalComponent } from './fgv-input-capital.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';

@NgModule({
	imports: [
		CommonModule,
		FormsModule,
		ReactiveFormsModule,

	],
	declarations: [FgvInputCapitalComponent],
	exports: [FgvInputCapitalComponent],
	bootstrap: [FgvInputCapitalComponent]

})
export class FgvInputCapitalModule { }
