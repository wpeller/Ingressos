import { FgvBaseComponent } from '../FgvBaseComponent';
import {
  Component,
  forwardRef,
  Input,
  PipeTransform,
} from '@angular/core';
import { NG_VALUE_ACCESSOR, FormGroup } from '@angular/forms';
import { CurrencyPipe } from '@angular/common';

export const CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR: any = {
  provide: NG_VALUE_ACCESSOR,
  // tslint:disable-next-line: no-use-before-declare
  useExisting: forwardRef(() => FgvInputMoneyComponent),
  multi: true
};

@Component({
  selector: 'fgv-input-money',
  templateUrl: './fgv-input-money.component.html',
  styleUrls: ['./fgv-input-money.component.css'],
  providers: [CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR]
})
export class FgvInputMoneyComponent extends FgvBaseComponent {

  forMoney: RegExp = /^[0-9\.,]+$/;

  @Input() min: string;
  @Input() max: string;
  @Input() minlength: string;
  @Input() maxlength: string;
  @Input() size: string;

	format() {
    if ((this.value !== '') && (this.value !== '%') && (this.value !== "'") && (this.value !== '.')) {
      if (isNaN(parseInt(this.value.substring(this.value.length - 1, this.value.length), 10))) {
        return;
      } else {
        this.value = this.value.replace(',', '');
        if (this.value.length === 0) {
          this.value = ',' + this.value;
        } else if (this.value.length === 1) {
          this.value = '0,' + this.value;
        } else {
          if (this.value.length >= 4) {
            this.value = this.value.substring(0, this.value.length - 2) + ',' + this.value.substring(this.value.length - 2, this.value.length);
            if (this.value.substring(0, 1) === '0') {
              this.value = this.value.substring(1, this.value.length);
            }
          } else {
            this.value = this.value.substring(0, this.value.length - 2) + ',' + this.value.substring(this.value.length - 2, this.value.length);
          }
        }
      }
    } else if ((this.value !== '%') && (this.value !== "'") && (this.value !== '.') && (this.value !== 9)) {
      if (this.value.length !== 0) {
        //this.value = this.value.replace('.', '');
        this.value = this.value.replace(',', '');
        if (this.value.length === 0) {
          this.value = '';
        }
        if (this.value.length === 1) {
          this.value = '';
        }
        if (this.value.length === 2) {
          if (this.value.substring(0, 1) === '0') {
            this.value = this.value.substring(1, this.value.length);
          }
          this.value = '0,' + this.value;
        } else {
          this.value = this.value.substring(0, this.value.length - 2) + ',' + this.value.substring(this.value.length - 2, this.value.length);
        }
      }
    }
  }
}
