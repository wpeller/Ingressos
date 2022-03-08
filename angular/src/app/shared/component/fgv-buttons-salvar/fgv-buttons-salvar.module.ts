import { CommonModule } from '@angular/common';
import { FgvButtonsSalvarComponent } from './fgv-buttons-salvar.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';

@NgModule({
	imports: [
		CommonModule,
		FormsModule,
		ReactiveFormsModule,
	],
	declarations: [ FgvButtonsSalvarComponent ],
	exports: [ FgvButtonsSalvarComponent ],
	bootstrap: [ FgvButtonsSalvarComponent ],
})
export class FgvButtonsSalvarModule {}
