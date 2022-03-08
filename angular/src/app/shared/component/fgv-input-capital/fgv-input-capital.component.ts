import { Component, forwardRef, Input } from '@angular/core';
import { FgvBaseComponent } from '../FgvBaseComponent';
import { FormGroup, NG_VALUE_ACCESSOR } from '@angular/forms';

export const CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR: any = {
  provide: NG_VALUE_ACCESSOR,
  // tslint:disable-next-line: no-use-before-declare
  useExisting: forwardRef(() => FgvInputCapitalComponent),
  multi: true
};

@Component({
  selector: 'fgv-input-capital',
  templateUrl: './fgv-input-capital.component.html',
  styleUrls: ['./fgv-input-capital.component.css'],
  providers: [CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR]
})
export class FgvInputCapitalComponent extends FgvBaseComponent {

  @Input() label: string;
  @Input() id: string;
  @Input() name: string;
  @Input() maxlength: string;
  @Input() minlength: string;
  @Input() formGroup: FormGroup;
  @Input() formControlName: string;

}
