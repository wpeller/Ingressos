import { Component, Input, Output, EventEmitter } from '@angular/core';
@Component({
	selector: 'fgv-buttons-buscar',
	templateUrl: './fgv-buttons-buscar.component.html',
	styleUrls: [ './fgv-buttons-buscar.component.css' ],
})
export class FgvButtonsBuscarComponent {
	@Input() disabledBuscar = false;

	@Input() exibirBusca = true;
	@Input() exibirNovo = true;
	@Input() exibirLimpar = true;

	@Output() buscar: EventEmitter<any> = new EventEmitter<any>();
	@Output() limpar: EventEmitter<any> = new EventEmitter<any>();
	@Output() novo: EventEmitter<any> = new EventEmitter<any>();

	buscarHandler(): void {
		this.buscar.emit();
	}

	limparHandler(): void {
		this.limpar.emit();
	}

	novoHandler(): void {
		this.novo.emit();
	}
}
