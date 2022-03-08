import { FgvBaseComponent } from '../FgvBaseComponent';
import {
  Component,
  forwardRef,
  Input,
  } from '@angular/core';
import { NG_VALUE_ACCESSOR, FormGroup } from '@angular/forms';

export const CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR: any = {
  provide: NG_VALUE_ACCESSOR,
  // tslint:disable-next-line: no-use-before-declare
  useExisting: forwardRef(() => FgvInputNumberComponent),
  multi: true
};

@Component({
  selector: 'fgv-input-number',
  templateUrl: './fgv-input-number.component.html',
  styleUrls: ['./fgv-input-number.component.css'],
  providers: [CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR]
})
export class FgvInputNumberComponent extends FgvBaseComponent {

  @Input() label: string;
  @Input() id: string;
  @Input() name: string;
  @Input() style: string;
  @Input() minlength: string;
  @Input() maxlength: string;
  @Input() min: string;
  @Input() max: string;
  @Input() formGroup: FormGroup;
  @Input() formControlName: string;

}
