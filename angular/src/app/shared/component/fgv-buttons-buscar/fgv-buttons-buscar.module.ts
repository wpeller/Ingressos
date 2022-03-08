import { CommonModule } from '@angular/common';
import { FgvButtonsBuscarComponent } from './fgv-buttons-buscar.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';

@NgModule({
	imports: [
		CommonModule,
		FormsModule,
		ReactiveFormsModule,
	],
	declarations: [ FgvButtonsBuscarComponent ],
	exports: [ FgvButtonsBuscarComponent ],
	bootstrap: [ FgvButtonsBuscarComponent ]
})
export class FgvButtonsBuscarModule {}
