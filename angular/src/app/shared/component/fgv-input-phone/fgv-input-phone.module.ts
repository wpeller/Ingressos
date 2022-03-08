import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FgvInputPhoneComponent } from './fgv-input-phone.component';
import { BsDropdownModule, ModalModule, TabsModule, TooltipModule } from 'ngx-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
	FileUploadModule as PrimeNgFileUploadModule,
	InputSwitchModule,
	PaginatorModule,
	PickListModule,
	ProgressBarModule,
	InputTextModule,
	InputMaskModule,
	KeyFilterModule,
} from 'primeng/primeng';

@NgModule({
	imports: [
		BsDropdownModule,
		CommonModule,
		CommonModule,
		FormsModule,
		InputMaskModule,
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
	declarations: [ FgvInputPhoneComponent ],
	exports: [ FgvInputPhoneComponent ],
})
export class FgvInputPhoneModule {}
