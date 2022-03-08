import {
	Component,
	EventEmitter,
	forwardRef,
	Input,
	Output,
	TemplateRef
	} from '@angular/core';
import { FgvBaseComponent } from '../FgvBaseComponent';
import { NG_VALUE_ACCESSOR } from '@angular/forms';

export const CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR: any = {
	provide: NG_VALUE_ACCESSOR,
	// tslint:disable-next-line: no-use-before-declare
	useExisting: forwardRef(() => FgvInputPickListComponent),
	multi: true,
};

@Component({
	selector: 'fgv-input-picklist',
	templateUrl: './fgv-input-picklist.component.html',
	styleUrls: [ './fgv-input-picklist.component.css' ],
})
export class FgvInputPickListComponent extends FgvBaseComponent {
	@Input() sourceHeader: string;
	@Input() targetHeader: string;
	@Input() source: any[] = [];
	@Input() target: any[] = [];
	@Input() showSourceControls: boolean;
	@Input() showTargetControls: boolean;
	@Input() filterBy: string;

	@Output() onMoveToTarget: EventEmitter<any> = new EventEmitter<any>();
	@Output() onMoveToSource: EventEmitter<any> = new EventEmitter<any>();
	@Output() onMoveAllToTarget: EventEmitter<any> = new EventEmitter<any>();
	@Output() onMoveAllToSource: EventEmitter<any> = new EventEmitter<any>();
	@Output() onSourceReorder: EventEmitter<any> = new EventEmitter<any>();
	@Output() onTargetReorder: EventEmitter<any> = new EventEmitter<any>();
	@Output() onSourceSelect: EventEmitter<any> = new EventEmitter<any>();
	@Output() onTargetSelect: EventEmitter<any> = new EventEmitter<any>();

	@Input()
  	itemTemplate: TemplateRef<any>;

	onMoveToTargetHandle(event?: any) {
		this.onMoveToTarget.emit(event);
	}
	onMoveToSourceHandle(event?: any) {
		this.onMoveToSource.emit(event);
	}

	onMoveAllToTargetHandle(event?: any) {
		this.onMoveAllToTarget.emit(event);
	}
	onMoveAllToSourceHandle(event?: any) {
		this.onMoveAllToSource.emit(event);
	}

	onSourceReorderHandle(event?: any) {
		this.onSourceReorder.emit(event);
	}
	onTargetReorderHandle(event?: any) {
		this.onTargetReorder.emit(event);
	}

	onSourceSelectHandle(event?: any) {
		this.onSourceSelect.emit(event);
	}
	onTargetSelectHandle(event?: any) {
		this.onSourceSelect.emit(event);
	}
}
