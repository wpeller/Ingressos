import { Component, Input, ViewChild, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';

@Component({
  selector: 'fgv-modal',
  templateUrl: './fgv-modal.component.html',
  styleUrls: ['./fgv-modal.component.css']
})
export class FgvModalComponent {

  @ViewChild('Modal') modal: ModalDirective;

  @Input() title: string;
  
  @Output() confirmar: EventEmitter<any> = new EventEmitter<any>();
  @Output() cancelar: EventEmitter<any> = new EventEmitter<any>();

  active = true;

  public show(): void {
  	this.active = true;
		this.modal.show();
  }
  
	public hide(): void {
  	this.active = false;
		this.modal.hide();
	}
  
	public close(): void {
		this.hide();
	}
}
