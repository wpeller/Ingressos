import { Injector, ElementRef, Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { LayoutRefService } from '@metronic/app/core/services/layout/layout-ref.service';
import { ThemesLayoutBaseComponent } from '@app/shared/layout/themes/themes-layout-base.component';
import { UrlHelper } from '@shared/helpers/UrlHelper';
import { AppConsts } from '@shared/AppConsts';
import { SideBarMenuComponent } from '../../nav/side-bar-menu.component';
import { Idle, DEFAULT_INTERRUPTSOURCES } from '@ng-idle/core';
import { Keepalive } from '@ng-idle/keepalive';


@Component({
    templateUrl: './default-layout.component.html',
    selector: 'default-layout',
    animations: [appModuleAnimation()],
    styleUrls: ['./default-layout.component.less']
})
export class DefaultLayoutComponent extends ThemesLayoutBaseComponent implements OnInit, AfterViewInit {

    @ViewChild('mHeader') mHeader: ElementRef;

    @ViewChild('sideBarMenu') sideBarMenu: SideBarMenuComponent;
    @ViewChild('defaultBrand') defaultBrand: DefaultLayoutComponent;
    
    public idleState: number = 0;
    public timedOut = false;
    public lastPing?: Date = null;
    public defaultIdle: number = 600;
    public progressCount: number;
    urlSiga = AppConsts.appBaseUrl;
    
    constructor(
        injector: Injector,
        private layoutRefService: LayoutRefService,
        private idle: Idle,
        private keepalive: Keepalive
    ) {
        super(injector);    
    }
    
    defaultLogo = AppConsts.appBaseUrl + '/assets/common/images/logo2.png';
    ngOnInit() {
        this.installationMode = UrlHelper.isInstallUrl(location.href);
    }

    ngAfterViewInit(): void {
        this.layoutRefService.addElement('header', this.mHeader.nativeElement);
        if (this.defaultBrand) {
            this.defaultBrand.sideBarMenu = this.sideBarMenu;
        }
    }
}
