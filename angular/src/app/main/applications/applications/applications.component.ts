import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Http } from '@angular/http';
import { ApplicationServiceProxy, ApplicationDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditApplicationModalComponent } from './create-or-edit-application-modal.component';
import { ViewApplicationModalComponent } from './view-application-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './applications.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class ApplicationsComponent extends AppComponentBase {

    @ViewChild('createOrEditApplicationModal') createOrEditApplicationModal: CreateOrEditApplicationModalComponent;
    @ViewChild('viewApplicationModalComponent') viewApplicationModal: ViewApplicationModalComponent;
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    maxSecondsToExpireFilter : number;
		maxSecondsToExpireFilterEmpty : number;
		minSecondsToExpireFilter : number;
		minSecondsToExpireFilterEmpty : number;




    constructor(
        injector: Injector,
        private _applicationsServiceProxy: ApplicationServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getApplications(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._applicationsServiceProxy.getAll(
            this.filterText,
            this.maxSecondsToExpireFilter == null ? this.maxSecondsToExpireFilterEmpty: this.maxSecondsToExpireFilter,
            this.minSecondsToExpireFilter == null ? this.minSecondsToExpireFilterEmpty: this.minSecondsToExpireFilter,
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getSkipCount(this.paginator, event),
            this.primengTableHelper.getMaxResultCount(this.paginator, event)
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    createApplication(): void {
        this.createOrEditApplicationModal.show();
    }

    deleteApplication(application: ApplicationDto): void {
        this.message.confirm(
            '',
            (isConfirmed) => {
                if (isConfirmed) {
                    this._applicationsServiceProxy.delete(application.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }
}
