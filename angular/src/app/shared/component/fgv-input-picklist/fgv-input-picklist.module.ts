import {
	BsDropdownModule,
	ModalModule,
	TabsModule,
	TooltipModule
	} from 'ngx-bootstrap';
import { CommonModule } from '@angular/common';
import { FgvInputPickListComponent } from './fgv-input-picklist.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputMaskModule } from 'primeng/inputmask';
import { KeyFilterModule } from 'primeng/keyfilter';
import { NgModule } from '@angular/core';
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
	declarations: [ FgvInputPickListComponent ],
	exports: [ FgvInputPickListComponent ],
	bootstrap: [ FgvInputPickListComponent ]
})
export class FgvInputPickListModule {}
