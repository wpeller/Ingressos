import { CommonModule } from '@angular/common';
import { DatePipe } from '@angular/common';
import { DateValidation } from '@app/shared/common/date/date.validation';
import { FgvInputDateComponent } from './fgv-input-date.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { KeyFilterModule } from 'primeng/keyfilter';
import { NgModule } from '@angular/core';
import { NgxBootstrapDatePickerConfigService } from 'assets/ngx-bootstrap/ngx-bootstrap-datepicker-config.service';
import {
	BsDatepickerConfig,
	BsDatepickerModule,
	BsDaterangepickerConfig,
	BsLocaleService,
} from 'ngx-bootstrap';


@NgModule({
	imports: [
		BsDatepickerModule,
		CommonModule,
        FormsModule,
		KeyFilterModule,
        ReactiveFormsModule
	],
	declarations: [ FgvInputDateComponent ],
	exports: [ FgvInputDateComponent ],
	providers: [
		BsLocaleService,
		DatePipe,
		DateValidation,
		{ provide: BsDatepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerConfig },
		{ provide: BsDaterangepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDaterangepickerConfig },
		{ provide: BsLocaleService, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerLocale },
	],
})
export class FgvInputDateModule {}
