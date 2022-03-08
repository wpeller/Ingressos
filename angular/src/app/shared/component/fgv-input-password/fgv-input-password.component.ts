import { FgvBaseComponent } from '../FgvBaseComponent';
import { FormGroup, NG_VALUE_ACCESSOR } from '@angular/forms';
import {
  Component,
  forwardRef,
  Input,
  } from '@angular/core';

export const CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR: any = {
  provide: NG_VALUE_ACCESSOR,
  // tslint:disable-next-line: no-use-before-declare
  useExisting: forwardRef(() => FgvInputPasswordComponent),
  multi: true
};

@Component({
  selector: 'fgv-input-password',
  templateUrl: './fgv-input-password.component.html',
  styleUrls: ['./fgv-input-password.component.css'],
  providers: [CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR]
})
export class FgvInputPasswordComponent extends FgvBaseComponent {

  @Input() label: string;
  @Input() id: string;
  @Input() name: string;
  @Input() style: string;
  @Input() maxlength: string;
  @Input() minlength: string;
  @Input() formGroup: FormGroup;
  @Input() formControlName: string;
  @Input() placeholder = '';
  @Input() feedback: boolean;

  @Input() promptLabel: string;
  @Input() weakLabel: string;
  @Input() mediumLabel: string;
  @Input() strongLabel: string;
}
