import { Component, Output, EventEmitter, Input } from '@angular/core';

@Component({
  selector: 'fgv-table-actions-delete',
  templateUrl: './fgv-table-actions-delete.component.html',
  styleUrls: ['./fgv-table-actions-delete.component.css']
})
export class FgvTableActionsDeleteComponent {

  @Output() excluir: EventEmitter<any> = new EventEmitter<any>();

  excluirHandler(event): void {
    this.excluir.emit(event);
  }
}
