import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { ApplicationServiceProxy, CreateOrEditApplicationDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditApplicationModal',
    templateUrl: './create-or-edit-application-modal.component.html'
})
export class CreateOrEditApplicationModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal') modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    application: CreateOrEditApplicationDto = new CreateOrEditApplicationDto();



    constructor(
        injector: Injector,
        private _applicationsServiceProxy: ApplicationServiceProxy
    ) {
        super(injector);
    }

    show(applicationId?: string): void {

        if (!applicationId) {
            this.application = new CreateOrEditApplicationDto();
            this.application.id = applicationId;

            this.active = true;
            this.modal.show();
        } else {
            this._applicationsServiceProxy.getApplicationForEdit(applicationId).subscribe(result => {
                this.application = result.application;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;
			
            this._applicationsServiceProxy.createOrEdit(this.application)
             .pipe(finalize(() => { this.saving = false; }))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }







    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
