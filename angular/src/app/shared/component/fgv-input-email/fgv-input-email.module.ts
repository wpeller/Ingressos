import { CommonModule } from '@angular/common';
import { FgvInputEmailComponent } from './fgv-input-email.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputMaskModule } from 'primeng/inputmask';
import { KeyFilterModule } from 'primeng/keyfilter';
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
	declarations: [ FgvInputEmailComponent ],
	exports: [ FgvInputEmailComponent ],
	bootstrap: [ FgvInputEmailComponent ],
})
export class FgvInputEmailModule {}
