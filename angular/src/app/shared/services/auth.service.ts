import { AppSessionService } from '@shared/common/session/app-session.service';
import { Injectable } from '@angular/core';
import { MenuServicoServiceProxy, UsuarioServicoServiceProxy, ValidaUsuarioTemPermissaoDto, RetornoOutputDto } from '@shared/service-proxies/service-proxies';
import { mergeMap as _observableMergeMap } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { PapelService } from './papel.service';
import { Router } from '@angular/router';
import { UtilsService } from 'abp-ng2-module/dist/src/utils/utils.service';

@Injectable()
export class AuthService {

    constructor(
        private usuarioServico: UsuarioServicoServiceProxy,
        private appSessionService: AppSessionService,
        private papelService: PapelService,
        private menuServico: MenuServicoServiceProxy,
        private utilsService: UtilsService,
        private router: Router) {
    }

    isUserAuthenticated(): boolean {
        return this.appSessionService.user !== undefined;
    }

    private getCurrentRouterUrl(): any {
        let currentRoute = this.router.url;
        return currentRoute;
    }

    private obterRotaCorrente(): string {
        let rota = this.getCurrentRouterUrl();
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

    isRouteAllowed(): Observable<RetornoOutputDto> {
        let lastPart = '';
        let rota = this.obterRotaCorrente();
        if (rota === '/') {
            let retorno = new RetornoOutputDto();
            retorno.sucesso = true;
            return of(retorno);
        }
        let parts = rota.split('/');
        if (parts && parts.length > 0) {
            lastPart = parts[parts.length - 1];
            if (lastPart === 'app' || lastPart === 'dashboard') {
                let retorno = new RetornoOutputDto();
                retorno.sucesso = true;
                return of(retorno);
            } else {
                let validar = new ValidaUsuarioTemPermissaoDto();
                validar.rota = rota;
                let usuarioLogado = this.appSessionService.user.siga2UserName;
                let papelIdCriptografado = this.utilsService.getCookieValue(`LastRoleUsed_${usuarioLogado}`);
                validar.papelId = +atob(papelIdCriptografado);
                validar.usuarioCodigoExterno = usuarioLogado;
                return this.usuarioServico.validaUsuarioTemPermissao(validar);
            }
        }
    }
}
