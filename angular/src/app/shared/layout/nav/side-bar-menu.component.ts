import { PermissionCheckerService } from '@abp/auth/permission-checker.service';
import { Injector, ElementRef, Component, OnInit, AfterViewInit, ViewEncapsulation, Inject, HostBinding, ChangeDetectionStrategy, ChangeDetectorRef, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { AppMenu } from './app-menu';
import { AppNavigationService } from './app-navigation.service';
import { DOCUMENT } from '@angular/common';
import { NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs/operators';

import { LayoutRefService } from '@metronic/app/core/services/layout/layout-ref.service';
import { MenuAsideOffcanvasDirective } from '@metronic/app/core/directives/menu-aside-offcanvas.directive';
import { NavigationDto, PapelDto } from '@shared/service-proxies/service-proxies';
import { PapelService } from '@app/shared/services/papel.service';
import { NavegacaoService } from '@app/shared/services/navegacao.service';
import { AuthService } from '@app/shared/services/auth.service';

@Component({
    templateUrl: './side-bar-menu.component.html',
    selector: 'side-bar-menu',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    styleUrls: ['side-bar-menu.component.less']
})
export class SideBarMenuComponent extends AppComponentBase implements OnInit, AfterViewInit {

    menu: AppMenu = null;

    @HostBinding('class') classes = 'm-grid__item m-aside-left m-aside-left--skin-light'; //+ this.s(this.currentTheme.baseSettings.theme + '.' + 'App.UiManagement.Left.AsideSkin');
    @HostBinding('id') id = 'm_aside_left';

    @HostBinding('attr.mMenuAsideOffcanvas') mMenuAsideOffcanvas: MenuAsideOffcanvasDirective;

    currentRouteUrl = '';
    insideTm: any;
    outsideTm: any;
    public clickedItemMenu: NavigationDto;
    papelDto: PapelDto;

    constructor(
        injector: Injector,
        private authService: AuthService,
        private el: ElementRef,
        private router: Router,
        private layoutRefService: LayoutRefService,
        private _permission: PermissionCheckerService,
        private _appNavigationService: AppNavigationService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _papelService: PapelService,
        public navegacaoService: NavegacaoService,
        @Inject(DOCUMENT) private document: Document) {
        super(injector);
    }

    ngOnInit() {

        this._papelService.onTrocarDePapel.subscribe(papelDto => {
            this.papelDto = papelDto;
            this.carregarMenu(this.papelDto);
        });

        this.currentRouteUrl = this.router.url.split(/[?#]/)[0];

        this.router.events
            .pipe(filter(event => event instanceof NavigationEnd))
            .subscribe(event => this.currentRouteUrl = this.router.url.split(/[?#]/)[0]);
    }

    private carregarMenu(papel: PapelDto = null) {
        if (!papel.nome || !papel.nome.includes('S2 - ')) {
            papel = null;
        }

        abp.ui.setBusy();
        this._appNavigationService.getMenu(papel).then(x => {
            this.menu = x;
            this._changeDetectorRef.detectChanges();
            abp.ui.clearBusy();
        });
    }


    ngAfterViewInit(): void {
        setTimeout(() => {
            this.mMenuAsideOffcanvas = new MenuAsideOffcanvasDirective(this.el);
            this.mMenuAsideOffcanvas.ngAfterViewInit();

            this.layoutRefService.addElement('asideLeft', this.el.nativeElement);
        });
    }

    showMenuItem(menuItem): boolean {
        return this._appNavigationService.showMenuItem(menuItem);
    }

    isMenuItemIsActive(item): boolean {
        if (item.items.length) {
            return this.isMenuRootItemIsActive(item);
        }

        if (!item.route) {
            return false;
        }

        // dashboard
        if (item.route !== '/' && this.currentRouteUrl.startsWith(item.route)) {
            return true;
        }

        return this.currentRouteUrl.replace(/\/$/, '') === item.route.replace(/\/$/, '');
    }

    isMenuRootItemIsActive(item): boolean {
        let result = false;

        for (const subItem of item.items) {
            result = this.isMenuItemIsActive(subItem);
            if (result) {
                return true;
            }
        }

        return false;
    }

    saveItemClicked(itemMenu: NavigationDto): void {
        if (itemMenu.items && itemMenu.items.length == 0) {
            this.clickedItemMenu = itemMenu;
        }
        //this.carregarMenu(this.papelDto);
    }

    setStyleSelectedItem(itemMenu: NavigationDto): string {
        if (this.clickedItemMenu) {
            if (itemMenu.name === this.clickedItemMenu.name) {
                return 'color: #5b9bd1;';
            }
        }
        return '';
    }

    getMenuName(item: NavigationDto): string {
        if (abp.localization.isCurrentCulture('pt-BR')) {
            return item.displayNamePtBr;
        }
        return item.displayNameEnUs;
    }

    /**
	 * Use for fixed left aside menu, to show menu on mouseenter event.
	 * @param e Event
	 */
    mouseEnter(e: Event) {
        // check if the left aside menu is fixed
        if (this.document.body.classList.contains('m-aside-left--fixed')) {
            if (this.outsideTm) {
                clearTimeout(this.outsideTm);
                this.outsideTm = null;
            }

            this.insideTm = setTimeout(() => {
                // if the left aside menu is minimized
                if (this.document.body.classList.contains('m-aside-left--minimize') && mUtil.isInResponsiveRange('desktop')) {
                    // show the left aside menu
                    this.document.body.classList.remove('m-aside-left--minimize');
                    this.document.body.classList.add('m-aside-left--minimize-hover');
                }
            }, 300);
        }
    }

    /**
     * Use for fixed left aside menu, to show menu on mouseenter event.
     * @param e Event
     */
    mouseLeave(e: Event) {
        if (this.document.body.classList.contains('m-aside-left--fixed')) {
            if (this.insideTm) {
                clearTimeout(this.insideTm);
                this.insideTm = null;
            }

            this.outsideTm = setTimeout(() => {
                // if the left aside menu is expand
                if (this.document.body.classList.contains('m-aside-left--minimize-hover') && mUtil.isInResponsiveRange('desktop')) {
                    // hide back the left aside menu
                    this.document.body.classList.remove('m-aside-left--minimize-hover');
                    this.document.body.classList.add('m-aside-left--minimize');
                }
            }, 500);
        }
    }


    obterClasse(parentItem: NavigationDto, item: NavigationDto): String {
        if (parentItem) {
            return 'm-menu__link-text subMenuTextoLink';
        }
        return 'm-menu__link-text';
    }

    obterClasseSubMenu(parentItem: NavigationDto): string {
        if (parentItem && this.clickedItemMenu && parentItem.id === this.clickedItemMenu.id) {
            return 'm-menu__link m-menu__toggle itemMenuSelecionado';
        }
        return 'm-menu__link m-menu__toggle';
    }

    collapseAllMenu(): void {
        let papel: PapelDto = this.papelDto;
        abp.ui.setBusy();
        this._appNavigationService.getMenu(papel).then(x => {
            this.menu = x;
            this._changeDetectorRef.detectChanges();
            abp.ui.clearBusy();
        });
    }
}

