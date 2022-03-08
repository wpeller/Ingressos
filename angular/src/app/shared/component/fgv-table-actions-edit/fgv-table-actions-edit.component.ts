import { Component, Output, EventEmitter, Input } from '@angular/core';

@Component({
  selector: 'fgv-table-actions-edit',
  templateUrl: './fgv-table-actions-edit.component.html',
  styleUrls: ['./fgv-table-actions-edit.component.css']
})
export class FgvTableActionsEditComponent {

  @Output() editar: EventEmitter<any> = new EventEmitter<any>();

  editarHandler(event): void {
    this.editar.emit(event);
  }
}
