import { CommonModule } from '@angular/common';
import { FgvInputRadioComponent } from './fgv-input-radio.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputMaskModule } from 'primeng/inputmask';
import { KeyFilterModule } from 'primeng/keyfilter';
import { NgModule } from '@angular/core';
import { BsDropdownModule, ModalModule, TabsModule, TooltipModule } from 'ngx-bootstrap';
import {
	FileUploadModule as PrimeNgFileUploadModule,
	InputSwitchModule,
	InputTextModule,
	PaginatorModule,
	PickListModule,
	ProgressBarModule,
	RadioButtonModule,
} from 'primeng/primeng';

@NgModule({
	imports: [
		BsDropdownModule,
		CommonModule,
		CommonModule,
		FormsModule,
		InputMaskModule,
		RadioButtonModule,
		InputSwitchModule,
		InputTextModule,
		KeyFilterModule,
		ModalModule,
		PaginatorModule,
		PickListModule,
		PrimeNgFileUploadModule,
		ProgressBarModule,
		ReactiveFormsModule,
		TabsModule,
		TooltipModule,
	],
	declarations: [ FgvInputRadioComponent ],
	exports: [ FgvInputRadioComponent ],
	bootstrap: [ FgvInputRadioComponent ],
})
export class FgvInputRadioModule {}
