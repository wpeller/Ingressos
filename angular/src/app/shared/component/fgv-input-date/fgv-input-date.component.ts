import { AppConsts } from '@shared/AppConsts';
import { FgvBaseComponent } from '../FgvBaseComponent';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { Component, forwardRef, Input, OnInit } from '@angular/core';
import { BsLocaleService } from 'ngx-bootstrap';

export const CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR: any = {
	provide: NG_VALUE_ACCESSOR,
	// tslint:disable-next-line: no-use-before-declare
	useExisting: forwardRef(() => FgvInputDateComponent),
	multi: true,
};

@Component({
	selector: 'fgv-input-date',
	templateUrl: './fgv-input-date.component.html',
	styleUrls: [ './fgv-input-date.component.css' ],
	providers: [ CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR ],
})
export class FgvInputDateComponent extends FgvBaseComponent implements OnInit {

	bsConfig = AppConsts.bsConfig;
	format = 'dd/mm/yyyy';
	forDate: RegExp = /^[0-9\/]+$/;

	constructor(
		private localeService: BsLocaleService
	) {
		super();
	}

	ngOnInit() {
		this.localeService.use('pt-br');
		this.placeholder = '__/__/____';
	}
}
