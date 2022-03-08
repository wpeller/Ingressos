import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetApplicationForView, ApplicationDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewApplicationModal',
    templateUrl: './view-application-modal.component.html'
})
export class ViewApplicationModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal') modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetApplicationForView;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetApplicationForView();
        this.item.application = new ApplicationDto();
    }

    show(item: GetApplicationForView): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
