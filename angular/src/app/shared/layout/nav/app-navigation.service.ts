import { PermissionCheckerService } from '@abp/auth/permission-checker.service';
import { AppSessionService } from '@shared/common/session/app-session.service';
import { Injectable, EventEmitter } from '@angular/core';
import { AppMenu } from './app-menu';
import { NavigationServiceProxy, NavigationDto, ObterItensDeMenuPorUsuarioEPapelInput, PapelDto, MenuServicoServiceProxy } from '@shared/service-proxies/service-proxies';
import { UsuarioService } from '@app/shared/services/usuario.service';

@Injectable()
export class AppNavigationService {

    constructor(
        private _permissionCheckerService: PermissionCheckerService,
        private _appSessionService: AppSessionService,
        private _navigationServiceProxy: NavigationServiceProxy,
        private _siga2Menu : MenuServicoServiceProxy,
        private _usuarioService : UsuarioService
    ) {
    }

    private menu = new AppMenu('MainMenu', 'MainMenu', [
        this.createNavigation('Administration', '', 'flaticon-interface-8', '', [
            this.createNavigation('Applications', 'Pages.Applications', 'flaticon-more', '/app/main/applications/applications'),
            this.createNavigation('OrganizationUnits', 'Pages.Administration.OrganizationUnits', 'flaticon-map', '/app/admin/organization-units'),
            this.createNavigation('Roles', 'Pages.Administration.Roles', 'flaticon-suitcase', '/app/admin/roles'),
            this.createNavigation('Users', 'Pages.Administration.Users', 'flaticon-users', '/app/admin/users'),
            this.createNavigation('Languages', 'Pages.Administration.Languages', 'flaticon-tabs', '/app/admin/languages'),
            this.createNavigation('AuditLogs', 'Pages.Administration.AuditLogs', 'flaticon-folder-1', '/app/admin/auditLogs'),
            this.createNavigation('Maintenance', 'Pages.Administration.Host.Maintenance', 'flaticon-lock', '/app/admin/maintenance'),
            this.createNavigation('Settings', 'Pages.Administration.Tenant.Settings', 'flaticon-settings', '/app/admin/tenantSettings')
        ])
    ]);

    public onMenuCarregado : EventEmitter<NavigationDto[]> = new EventEmitter();

    createNavigation (
        name: string,
        permissionName: string,
        icon: string,
        route: string,
        items?: NavigationDto[],
        external?: boolean,
        moduleId? : string,
        modulePath?: string,
        fullUrlPath?:string,
        visible: boolean = true
    ): NavigationDto
    {
        var obj = new NavigationDto();
        obj.name = name;
        obj.permissionName = permissionName;
        obj.icon = icon;
        obj.route = route;
        obj.items = [];
        if(items) {
            obj.items = items;
        }
        obj.external = external;
        obj.moduleId = moduleId;
        obj.modulePath = modulePath;
        obj.fullUrlPath = fullUrlPath;
        obj.visible = visible;
        return obj;
    }

    getMenu(papel : PapelDto = null): Promise<AppMenu> {
        return Promise.all([
                        this.getLocalMenu(),
                        this.getSiga2Menu(papel)])
                    .then(x => {
                        var m = new AppMenu("","",[]);
                        m.items = x[0].concat(x[1]);
                        this.onMenuCarregado.emit(m.items);
                        return m;
                    });
    }

    private getLocalMenu() : Promise<NavigationDto[]>{
        let m = new NavigationDto();
        m.items = this.menu.items;
        return this._navigationServiceProxy.sendAndSynchronize(m)
            .toPromise()
            .then(x =>
                x.items);
    }

    public getSiga2Menu(papel : PapelDto = null): Promise<NavigationDto[]>{
        if (!papel || papel.id == null) {
            return Promise.resolve([]);
        }

        var input = new ObterItensDeMenuPorUsuarioEPapelInput();
        input.idPapel = papel.id;
        input.codigoExternoUsuario = this._usuarioService.obterUserName();
        return this._siga2Menu.obterItensDeMenuPorUsuarioEPapel(input).toPromise();
    }

    checkChildMenuItemPermission(menuItem): boolean {
        for (let i = 0; i < menuItem.items.length; i++) {
            let subMenuItem = menuItem.items[i];

            if (subMenuItem.permissionName) {
                return this._permissionCheckerService.isGranted(subMenuItem.permissionName);
            } else if (subMenuItem.items && subMenuItem.items.length) {
                return this.checkChildMenuItemPermission(subMenuItem);
            }
            return true;
        }

        return false;
    }

    showMenuItem(menuItem: NavigationDto): boolean {
        if (menuItem && menuItem.permissionName && menuItem.permissionName === 'Pages.Administration.Tenant.SubscriptionManagement' && this._appSessionService.tenant && !this._appSessionService.tenant.edition) {
            return false;
        }

        let hideMenuItem = !menuItem.visible;

        if (menuItem.permissionName && !this._appSessionService.user) {
            hideMenuItem = true;
        }

        if (menuItem.permissionName && !this._permissionCheckerService.isGranted(menuItem.permissionName)) {
            hideMenuItem = true;
        }

        if (!hideMenuItem && menuItem.items && menuItem.items.length) {
            return this.checkChildMenuItemPermission(menuItem);
        }

        return !hideMenuItem;
    }
}
