import { AutoCompleteModule } from 'primeng/primeng';
import { CommonModule } from '@angular/common';
import { FgvInputAutocompleteComponent } from './fgv-input-autocomplete.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';

@NgModule({
	imports: [
		AutoCompleteModule,
		CommonModule,
		FormsModule,
		ReactiveFormsModule
	],
	declarations: [ FgvInputAutocompleteComponent ],
	exports: [ FgvInputAutocompleteComponent ],
	bootstrap: [ FgvInputAutocompleteComponent ],
})
export class FgvInputAutocompleteModule {}
