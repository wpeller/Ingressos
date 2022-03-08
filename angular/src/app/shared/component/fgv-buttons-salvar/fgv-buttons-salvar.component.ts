import {
  Component,
  Input,
  Output,
  EventEmitter,
  } from '@angular/core';

@Component({
  selector: 'fgv-buttons-salvar',
  templateUrl: './fgv-buttons-salvar.component.html',
  styleUrls: ['./fgv-buttons-salvar.component.css']
})
export class FgvButtonsSalvarComponent {

  @Input() disabled: boolean;
  @Output() voltar: EventEmitter<any> = new EventEmitter<any>();
  @Output() salvar: EventEmitter<any> = new EventEmitter<any>();

  voltarHandler(): void {
    this.voltar.emit();
  }

  salvarHandler(): void {
    this.salvar.emit();
  }
}
