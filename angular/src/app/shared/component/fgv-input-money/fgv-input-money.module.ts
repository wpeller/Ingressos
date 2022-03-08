import { CommonModule } from '@angular/common';
import { FgvInputMoneyComponent } from './fgv-input-money.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { KeyFilterModule } from 'primeng/primeng';
import { NgModule } from '@angular/core';

export const customCurrencyMaskConfig = {
	align: 'center',
	allowNegative: true,
	allowZero: true,
	decimal: ',',
	precision: 2,
	prefix: 'R$ ',
	suffix: '',
	thousands: '.',
	nullable: true,
};

@NgModule({
	imports: [
		CommonModule,
		FormsModule,
		KeyFilterModule,
		ReactiveFormsModule
	],
	declarations: [ FgvInputMoneyComponent ],
	exports: [ FgvInputMoneyComponent ],
	bootstrap: [ FgvInputMoneyComponent ],
})
export class FgvInputMoneyModule {}
