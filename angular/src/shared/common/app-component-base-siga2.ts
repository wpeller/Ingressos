import { UsuarioServicoServiceProxy, PapelDto, ValidaUsuarioTemPermissaoDto } from '@shared/service-proxies/service-proxies';
import { AfterViewInit, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from './app-component-base';
import { AppSessionService } from './session/app-session.service';
import { Router } from '@angular/router';
import { UtilsService } from 'abp-ng2-module/dist/src/utils/utils.service';
import { AppConsts } from '@shared/AppConsts';

export abstract class AppComponentBaseSiga2 extends AppComponentBase implements OnInit, AfterViewInit {

    private admUsuarioServico: UsuarioServicoServiceProxy;
    private appSessionService: AppSessionService;
    private router: Router;
    private utilsService: UtilsService;

    papel: PapelDto;

    constructor(injector: Injector) {
        super(injector);
        this.admUsuarioServico = injector.get(UsuarioServicoServiceProxy);
        this.appSessionService = injector.get(AppSessionService);''
        this.router = injector.get(Router);
        this.utilsService = injector.get(UtilsService);
    }

    private getCurrentRouterUrl(): any {
        let currentRoute = this.router.url;
        return currentRoute;
    }

    private obterRotaCorrente(): string {
        let rota = this.getCurrentRouterUrl();
        if (rota.startsWith('/')) {
            rota = rota.substring(1);
        }
        if (rota.indexOf('editar') > 0) {
            let index = rota.indexOf('editar');
            rota = rota.substring(0, index - 1);
            rota = rota + '/consulta';
        }
        if (rota.indexOf('ajuda') > 0 && rota.indexOf('consulta') > 0) {
            let index = rota.indexOf('consulta');
            rota = rota.substring(0, index - 1);
            rota = rota + '/cadastro';
        }
        return rota;
    }

    ngOnInit(): void {
        super.ngOnInit();
        this.papel = this.papelService.obterPapelAtual();
        let ambiente = AppConsts.ambiente;
        ambiente = ambiente.toLowerCase();
        if (ambiente.indexOf('desenvolvimento') === -1) {
            let rota = this.getCurrentRouterUrl();
            if (rota.indexOf('trocaSenha') > 0) {
                return;
            }
            this.verificarAcesso();
        }
    }

    ngAfterViewInit(): void {
    }

    verificarAcesso() {
        this.papel = this.papelService.obterPapelAtual();
        if (this.papel && !this.papel.id) {
            this.papelService.onTrocarDePapel.subscribe(papel => {
                this.papel = papel;
                let rota = this.obterRotaCorrente();
                if (rota === '/') {
                    return;
                }
                let validar = new ValidaUsuarioTemPermissaoDto();
                validar.rota = rota;
                let usuarioLogado = this.appSessionService.user.siga2UserName;
                let papelIdCriptografado = this.utilsService.getCookieValue(`LastRoleUsed_${usuarioLogado}`);
                validar.papelId = +atob(papelIdCriptografado);
                validar.usuarioCodigoExterno = usuarioLogado;
                this.admUsuarioServico.validaUsuarioTemPermissao(validar).subscribe(result => {
                    if (result.sucesso) {
                        this.navegacaoServico.registrarAcesso();
                        return;
                    } else {
                        this.message.error('Acesso negado!');
                        setTimeout(() => { this.router.navigate(['/app/dashboard']); }, 600);
                    }
                });

            });
        } else {
            let rota = this.obterRotaCorrente();
            let validar = new ValidaUsuarioTemPermissaoDto();
            validar.rota = rota;
            let usuarioLogado = this.appSessionService.user.siga2UserName;
            let papelIdCriptografado = this.utilsService.getCookieValue(`LastRoleUsed_${usuarioLogado}`);
            validar.papelId = +atob(papelIdCriptografado);
            validar.usuarioCodigoExterno = usuarioLogado;
            this.admUsuarioServico.validaUsuarioTemPermissao(validar).subscribe(result => {
                if (result.sucesso) {
                    this.navegacaoServico.registrarAcesso();
                    return;
                } else {
                    this.message.error('Acesso negado!');
                    setTimeout(() => { this.router.navigate(['/app/dashboard']); }, 600);
                }
            });
        }
    }
}
