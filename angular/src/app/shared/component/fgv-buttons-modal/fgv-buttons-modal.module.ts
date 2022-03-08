import { CommonModule } from '@angular/common';
import { FgvButtonsModalComponent } from './fgv-buttons-modal.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { BsDropdownModule, ModalModule, TabsModule, TooltipModule } from 'ngx-bootstrap';
import {
	FileUploadModule as PrimeNgFileUploadModule,
	InputSwitchModule,
	PaginatorModule,
	PickListModule,
	ProgressBarModule,
	InputTextModule,
} from 'primeng/primeng';

@NgModule({
	imports: [
		BsDropdownModule,
		CommonModule,
		FormsModule,
		InputSwitchModule,
		InputTextModule,
		ModalModule,
		PaginatorModule,
		PickListModule,
		PrimeNgFileUploadModule,
		ProgressBarModule,
		ReactiveFormsModule,
		TabsModule,
		TooltipModule,
	],
	declarations: [ FgvButtonsModalComponent ],
	exports: [ FgvButtonsModalComponent ],
})
export class FgvButtonsModalModule {}
