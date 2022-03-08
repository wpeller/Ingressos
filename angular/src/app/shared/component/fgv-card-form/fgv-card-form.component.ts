import {
	Component,
	EventEmitter,
	Input,
	Output
	} from '@angular/core';

@Component({
	selector: 'fgv-card-form',
	templateUrl: './fgv-card-form.component.html',
	styleUrls: [ './fgv-card-form.component.css' ],
})
export class FgvCardFormComponent {

	@Input() exibirVoltar = true;
	@Input() exibirSalvar = true;
	
	@Output() voltar: EventEmitter<any> = new EventEmitter<any>();
	@Output() salvar: EventEmitter<any> = new EventEmitter<any>();

	constructor(
	) {
	}

	acaoVoltar(): void {
		this.voltar.emit();
	}

	acaoSalvar(): void {
		this.salvar.emit();
	}
}
