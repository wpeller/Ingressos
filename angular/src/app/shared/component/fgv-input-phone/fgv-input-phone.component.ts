import { Component, OnInit, forwardRef, Input } from '@angular/core';

import { FgvBaseComponent } from '../FgvBaseComponent';
import { NG_VALUE_ACCESSOR } from '@angular/forms';

export const CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR: any = {
    provide: NG_VALUE_ACCESSOR,
    // tslint:disable-next-line: no-use-before-declare
    useExisting: forwardRef(() => FgvInputPhoneComponent),
    multi: true,
};

@Component({
    selector: 'fgv-input-phone',
    templateUrl: './fgv-input-phone.component.html',
    styleUrls: ['./fgv-input-phone.component.css'],
    providers: [CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR],
})
export class FgvInputPhoneComponent extends FgvBaseComponent {

}
