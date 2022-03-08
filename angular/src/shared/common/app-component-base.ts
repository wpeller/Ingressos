import { AbpMultiTenancyService } from '@abp/multi-tenancy/abp-multi-tenancy.service';
import { AppConsts } from '@shared/AppConsts';
import { AppSessionService } from '@shared/common/session/app-session.service';
import { AppUiCustomizationService } from '@shared/common/ui/app-ui-customization.service';
import { AppUrlService } from '@shared/common/nav/app-url.service';
import {
    AutorizacaoServicoServiceProxy,
    FuncionalidadesPermissoes,
    AutorizacaoInput,
    PapelDto,
    RecursoFuncionalidade,
    UiCustomizationSettingsDto
    } from '@shared/service-proxies/service-proxies';
import { FeatureCheckerService } from '@abp/features/feature-checker.service';
import { Injector, OnInit } from '@angular/core';
import { LocalizationService } from '@abp/localization/localization.service';
import { MessageService } from '@abp/message/message.service';
import { NavegacaoService } from '@app/shared/services/navegacao.service';
import { NotifyService } from '@abp/notify/notify.service';
import { PapelService } from '@app/shared/services/papel.service';
import { PermissionCheckerService } from '@abp/auth/permission-checker.service';
import { PrimengTableHelper } from 'shared/helpers/PrimengTableHelper';
import { SettingService } from '@abp/settings/setting.service';
import { UsuarioService } from '@app/shared/services/usuario.service';

export abstract class AppComponentBase implements OnInit {

    localizationSourceName = AppConsts.localization.defaultLocalizationSourceName;
    private permissao: FuncionalidadesPermissoes;
    localization: LocalizationService;
    permission: PermissionCheckerService;
    feature: FeatureCheckerService;
    notify: NotifyService;
    setting: SettingService;
    message: MessageService;
    multiTenancy: AbpMultiTenancyService;
    appSession: AppSessionService;
    primengTableHelper: PrimengTableHelper;
    ui: AppUiCustomizationService;
    appUrlService: AppUrlService;
    protected papelService: PapelService;
    private usuarioServico: UsuarioService;
    private autorizacaoServico: AutorizacaoServicoServiceProxy;
    protected navegacaoServico: NavegacaoService;
    constructor(injector: Injector) {
        this.localization = injector.get(LocalizationService);
        this.permission = injector.get(PermissionCheckerService);
        this.feature = injector.get(FeatureCheckerService);
        this.notify = injector.get(NotifyService);
        this.setting = injector.get(SettingService);
        this.message = injector.get(MessageService);
        this.multiTenancy = injector.get(AbpMultiTenancyService);
        this.appSession = injector.get(AppSessionService);
        this.ui = injector.get(AppUiCustomizationService);
        this.appUrlService = injector.get(AppUrlService);
        this.primengTableHelper = new PrimengTableHelper();
        this.papelService = injector.get(PapelService);
        this.usuarioServico = injector.get(UsuarioService);
        this.autorizacaoServico = injector.get(AutorizacaoServicoServiceProxy);
        this.navegacaoServico = injector.get(NavegacaoService);
    }

    ngOnInit() {
        // this.getPermissaoRecurso();
    }

    l(key: string, ...args: any[]): string {
        args.unshift(key);
        args.unshift(this.localizationSourceName);
        return this.ls.apply(this, args);
    }

    ls(sourcename: string, key: string, ...args: any[]): string {
        let localizedText = this.localization.localize(key, sourcename);

        if (!localizedText) {
            localizedText = key;
        }

        if (!args || !args.length) {
            return localizedText;
        }

        args.unshift(localizedText);
        return abp.utils.formatString.apply(this, args);
    }

    isGranted(permissionName: string): boolean {
        return this.permission.isGranted(permissionName);
    }

    isGrantedAny(...permissions: string[]): boolean {
        if (!permissions) {
            return false;
        }

        for (const permission of permissions) {
            if (this.isGranted(permission)) {
                return true;
            }
        }

        return false;
    }

    s(key: string): string {
        return abp.setting.get(key);
    }

    appRootUrl(): string {
        return this.appUrlService.appRootUrl;
    }

    get currentTheme(): UiCustomizationSettingsDto {
        return this.appSession.theme;
    }

    protected obterPapelAtual(): PapelDto {
        return this.papelService.obterPapelAtual();
    }

    protected obterUserName(): string {
        return this.usuarioServico.obterUserName();
    }

    permite(funcionalidade: string): Boolean {
        if (this.permissao && this.permissao.permissoes) {
            this.getPermissaoRecurso();
            return this.validaPermissao(funcionalidade);
        }
    }

    permissaoCarregada(): Boolean {
        return this.permissao !== null;
    }

    protected getPermissaoRecurso() {
        if (this.navegacaoServico.navegacaoAtual) {
            let itemMenuId = this.navegacaoServico.navegacaoAtual.id;
            let papelId = this.papelService.obterPapelAtual().id;
            let userName =  this.usuarioServico.obterUserName();
            let input = new AutorizacaoInput();
            input.itemMenuId = itemMenuId;
            input.papelId = papelId;
            input.codigoExterno = userName;
            this.autorizacaoServico.obterPermissoes(input).subscribe(p => {
                console.log(p);
                this.permissao = p;
            });
        }
    }

    private validaPermissao(funcionalidade: string): Boolean {
        let existe: RecursoFuncionalidade = this.permissao.permissoes.find(f => f.funcionalidade === funcionalidade)[0];
        return existe && existe.habilitado;
    }

}
