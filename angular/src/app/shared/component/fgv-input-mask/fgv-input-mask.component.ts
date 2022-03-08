import { FgvBaseComponent } from '../FgvBaseComponent';
import { FormGroup, NG_VALUE_ACCESSOR } from '@angular/forms';
import {
  Component,
  forwardRef,
  Input,
  Output,
  EventEmitter,
  } from '@angular/core';

export const CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR: any = {
  provide: NG_VALUE_ACCESSOR,
  // tslint:disable-next-line: no-use-before-declare
  useExisting: forwardRef(() => FgvInputMaskComponent),
  multi: true
};

@Component({
  selector: 'fgv-input-mask',
  templateUrl: './fgv-input-mask.component.html',
  styleUrls: ['./fgv-input-mask.component.css'],
  providers: [CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR]
})
export class FgvInputMaskComponent extends FgvBaseComponent {

  @Input() mask: string;

  @Output() onfocus: EventEmitter<any> = new EventEmitter<any>();
  @Output() onblur: EventEmitter<any> = new EventEmitter<any>();
  @Output() oncomplete: EventEmitter<any> = new EventEmitter<any>();

  onfocusHandler(event): void {
    this.onfocus.emit(event);
  }

  onblurHandler(event): void {
    this.onblur.emit(event);
  }

  oncompleteHandler(event): void {
    this.oncomplete.emit(event);
  }
}
