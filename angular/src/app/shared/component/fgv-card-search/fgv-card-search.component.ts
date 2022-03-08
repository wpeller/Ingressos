import { Component, Output, EventEmitter, Input } from '@angular/core';

@Component({
    selector: 'fgv-card-search',
    templateUrl: './fgv-card-search.component.html',
    styleUrls: ['./fgv-card-search.component.css'],
})
export class FgvCardSearchComponent {

    @Input() exibirNovo = true;

    @Output() buscar: EventEmitter<any> = new EventEmitter();
    @Output() limpar: EventEmitter<any> = new EventEmitter();
    @Output() novoRegistro: EventEmitter<any> = new EventEmitter();

    acaoBuscar() {
        this.buscar.emit();
    }

    acaoLimpar() {
        this.limpar.emit();
    }

    acaoNovoRegistro() {
        this.novoRegistro.emit();
    }
}
