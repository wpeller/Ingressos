import { FgvBaseComponent } from '../FgvBaseComponent';
import { FormGroup, NG_VALUE_ACCESSOR } from '@angular/forms';
import { Component, forwardRef, Input } from '@angular/core';

export const CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR: any = {
	provide: NG_VALUE_ACCESSOR,
	// tslint:disable-next-line: no-use-before-declare
	useExisting: forwardRef(() => FgvInputEditorComponent),
	multi: true,
};

@Component({
	selector: 'fgv-input-editor',
	templateUrl: './fgv-input-editor.component.html',
	styleUrls: ['./fgv-input-editor.component.css'],
	providers: [CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR],
})
export class FgvInputEditorComponent extends FgvBaseComponent {

	@Input() label: string;
	@Input() id: string;
	@Input() name: string;
	@Input() style: string;
	@Input() maxlength: string;
	@Input() minlength: string;
	@Input() formGroup: FormGroup;
	@Input() readonly: boolean;
	@Input() required: boolean;
	@Input() simple: boolean;
}
