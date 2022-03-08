import { AppConsts } from '@shared/AppConsts';
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
  useExisting: forwardRef(() => FgvInputEmailComponent),
  multi: true
};

@Component({
  selector: 'fgv-input-email',
  templateUrl: './fgv-input-email.component.html',
  styleUrls: ['./fgv-input-email.component.css'],
  providers: [CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR]
})
export class FgvInputEmailComponent extends FgvBaseComponent {

  @Input() label: string;
  @Input() id: string;
  @Input() name: string;
  @Input() style: string;
  @Input() formGroup: FormGroup;
  @Input() formControlName: string;

  bsConfig = AppConsts.bsConfig;
}
