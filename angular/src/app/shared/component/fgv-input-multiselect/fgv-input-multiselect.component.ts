import {
  Component,
  forwardRef,
  Input,
  OnInit
  } from '@angular/core';
import { FgvBaseComponent } from '../FgvBaseComponent';
import { FormGroup, NG_VALUE_ACCESSOR } from '@angular/forms';
import { SelectItem } from 'primeng/api';

export const CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR: any = {
  provide: NG_VALUE_ACCESSOR,
  // tslint:disable-next-line: no-use-before-declare
  useExisting: forwardRef(() => FgvInputMultiselectComponent),
  multi: true
};

@Component({
  selector: 'fgv-input-multiselect',
  templateUrl: './fgv-input-multiselect.component.html',
  styleUrls: ['./fgv-input-multiselect.component.css']
})
export class FgvInputMultiselectComponent extends FgvBaseComponent implements OnInit {

  //@Input() filterPlaceHolder: string;
  //@Input() defaultLabel = 'Selecione';
  //@Input() optionLabel: string;

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
