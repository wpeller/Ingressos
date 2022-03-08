import {
    Component,
    Input,
    Output,
    EventEmitter,
} from '@angular/core';

@Component({
    selector: 'fgv-button-incluir',
    templateUrl: './fgv-button-incluir.component.html',
    styleUrls: ['./fgv-button-incluir.component.css']
})
export class FgvButtonIncluirComponent {

    @Input() value: string = 'Incluir';
    @Input() disabled: boolean;
    @Output() incluir: EventEmitter<any> = new EventEmitter<any>();

    incluirHandler(): void {
        this.incluir.emit();
    }
}
