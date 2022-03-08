import { AppComponentBase } from 'shared/common/app-component-base';
import { AppConsts } from '@shared/AppConsts';
import { Component, Injector, OnInit } from '@angular/core';
import { AppSessionService } from '@shared/common/session/app-session.service';

@Component({
    templateUrl: './dashboard.component.html'
})
export class DashboardComponent extends AppComponentBase implements OnInit {

    public constructor(
        injector: Injector,
        private _sessionService: AppSessionService
    ) {
        super(injector);
    }

    ngOnInit() {
        // if (this._sessionService.user.siga2UserName) {
        //     location.href = `${AppConsts.appBaseUrl}/app/${AppConsts.moduloAcesso}/home/dashboard`;
        // } else {
        //     location.href = `${AppConsts.appBaseUrl}/app/main/dashboard`;
        // }
    }
}
