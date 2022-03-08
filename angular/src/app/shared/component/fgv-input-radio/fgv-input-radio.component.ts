import { Component, forwardRef, Input } from '@angular/core';
import { FgvBaseComponent } from '../FgvBaseComponent';
import { GenericItem } from '../GenericItem';
import { NG_VALUE_ACCESSOR } from '@angular/forms';

export const CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR: any = {
  provide: NG_VALUE_ACCESSOR,
  // tslint:disable-next-line: no-use-before-declare
  useExisting: forwardRef(() => FgvInputRadioComponent),
  multi: true
};

@Component({
  selector: 'fgv-input-radio',
  templateUrl: './fgv-input-radio.component.html',
  styleUrls: ['./fgv-input-radio.component.css'],
  providers: [CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR]
})
export class FgvInputRadioComponent extends FgvBaseComponent {

  @Input() items: GenericItem[];

}
