import { Injector, Component, ViewEncapsulation, Inject, OnInit, Output, Input } from '@angular/core';

import { AppConsts } from '@shared/AppConsts';
import { AppComponentBase } from '@shared/common/app-component-base';

import { DOCUMENT } from '@angular/common';
import { Router } from '@angular/router';
import { SideBarMenuComponent } from '../../nav/side-bar-menu.component';

@Component({
    templateUrl: './default-brand.component.html',
    selector: 'default-brand',
    encapsulation: ViewEncapsulation.None
})
export class DefaultBrandComponent extends AppComponentBase implements OnInit {

    public menu: string;
    esconderMenu: Object;

    public sideBarMenu: SideBarMenuComponent;
    urlSiga = AppConsts.appBaseUrl

    //defaultLogo = AppConsts.appBaseUrl + '/assets/common/images/logo2.png';
    defaultLogo = AppConsts.appBaseUrl + '/assets/common/images/logo-siga.png';
    remoteServiceBaseUrl: string = AppConsts.remoteServiceBaseUrl;

    constructor(
        injector: Injector,
        @Inject(DOCUMENT) private document: Document,
        private _router: Router
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.menu = 'Esconder menu';
    }

    InteratividadeIconeMenu(): void {

        this.menu = (this.menu === 'Esconder menu' ? 'Mostrar menu' : 'Esconder menu')
    }

    clickTopbarToggle(): void {
        this.document.body.classList.toggle('m-topbar--on');
    }

    clickLeftAsideHideToggle(): void {
        this.document.body.classList.toggle('m-aside-left--hide');
    }

    receberEvento(recebido) {
        alert('Evento acionado com sucesso!');
    }

    goHome(): void {
        if (this.sideBarMenu) {
            this.sideBarMenu.collapseAllMenu();
        }
        this._router.navigate(['/']);
    }
}
