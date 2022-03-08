import { Component, Input, Output, EventEmitter } from '@angular/core';
@Component({
  selector: 'fgv-buttons-modal',
  templateUrl: './fgv-buttons-modal.component.html',
  styleUrls: ['./fgv-buttons-modal.component.css'],
})
export class FgvButtonsModalComponent {

  @Output() confirmar: EventEmitter<any> = new EventEmitter<any>();
  @Output() cancelar: EventEmitter<any> = new EventEmitter<any>();

  confirmarHandler(): void {
    this.confirmar.emit();
  }

  cancelarHandler(): void {
    this.cancelar.emit();
  }
}
