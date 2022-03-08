import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { FgvBaseComponent } from '../FgvBaseComponent';
import {
    Component,
    forwardRef,
    Input,
    Output,
    EventEmitter,
} from '@angular/core';

export const CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR: any = {
    provide: NG_VALUE_ACCESSOR,
    // tslint:disable-next-line: no-use-before-declare
    useExisting: forwardRef(() => FgvInputAutocompleteComponent),
    multi: true
};

@Component({
    selector: 'fgv-input-autocomplete',
    templateUrl: './fgv-input-autocomplete.component.html',
    styleUrls: ['./fgv-input-autocomplete.component.css'],
    providers: [CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR]
})
export class FgvInputAutocompleteComponent extends FgvBaseComponent {

    @Input() dropdown: boolean;
    @Input() field: string;
    @Input() forceSelection: boolean;
    @Input() minLength: string;
    @Input() suggestions: any[];

    @Output() completeMethod: EventEmitter<any> = new EventEmitter<any>();
    @Output() onSelect: EventEmitter<any> = new EventEmitter<any>();
    @Output() onUnSelect: EventEmitter<any> = new EventEmitter<any>();
    @Output() onDropdownClick: EventEmitter<any> = new EventEmitter<any>();

    completeMethodHandler(event) {
        this.completeMethod.emit(event);
    }

    onSelectHandler(event) {
        this.onSelect.emit(event);
    }

    onUnSelectHandler(event) {
        this.onUnSelect.emit(event);
    }

    onDropdownClickHandler(event) {
        this.onDropdownClick.emit(event);
    }
}
