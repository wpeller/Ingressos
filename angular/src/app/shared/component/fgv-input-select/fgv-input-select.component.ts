import { FgvBaseComponent } from '../FgvBaseComponent';
import {
  Component,
  forwardRef,
  Input,
  OnInit,
  } from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { SelectItem } from 'primeng/api';

export const CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR: any = {
  provide: NG_VALUE_ACCESSOR,
  // tslint:disable-next-line: no-use-before-declare
  useExisting: forwardRef(() => FgvInputSelectComponent),
  multi: true
};

@Component({
  selector: 'fgv-input-select',
  templateUrl: './fgv-input-select.component.html',
  styleUrls: ['./fgv-input-select.component.css'],
  providers: [CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR]
})
export class FgvInputSelectComponent extends FgvBaseComponent implements OnInit {

  @Input() options: SelectItem[] = [];
  @Input() rows: string;
  @Input() isConsulta = false;
  @Input() isMasculino = true;
  @Input() exibirSelecione = true;
  @Input() textoSelecione = 'SELECIONE';

  ngOnInit(): void {
    if (this.isConsulta) {
      this.textoSelecione = 'TODOS';
      if (!this.isMasculino) {
        this.textoSelecione = 'TODAS';
      }
    } else {
      this.textoSelecione = 'SELECIONE';
    }
  }
}
