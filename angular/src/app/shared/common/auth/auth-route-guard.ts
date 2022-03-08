import {
    ActivatedRouteSnapshot,
    CanActivate,
    Router,
    RouterStateSnapshot
    } from '@angular/router';
import { AuthService } from '@app/shared/services/auth.service';
import { Data, Route } from '@node_modules/@angular/router/src/config';
import { Injectable } from '@angular/core';
import { NavegacaoService } from '@app/shared/services/navegacao.service';
import { Observable } from 'rxjs';
import { UrlHelper } from '@shared/helpers/UrlHelper';

@Injectable()
export class AppRouteGuard implements CanActivate {

    constructor(
        private authService: AuthService,
        private navegacaoService: NavegacaoService,
        private router: Router
    ) { }

    canActivateInternal(data: Data, state: RouterStateSnapshot): boolean {
        if (state && UrlHelper.isInstallUrl(state.url)) {
            return true;
        }

        let rota = state.url;
        if (rota.indexOf('editar') > 0) {
            let index = rota.indexOf('editar');
            rota = rota.substring(0, index - 1);
            rota = rota + '/consulta';
        }
        if (rota.indexOf('dashboard') === -1) {
            this.navegacaoService.getNavigationItem(rota)
                .subscribe(navigation => {
                    if (navigation) {
                        this.navegacaoService.navegacaoAtual = navigation;
                        abp.event.trigger('app.auth.canActivate', navigation);
                    }
                });
        }

        let isUserAuthenticated = this.authService.isUserAuthenticated();
        if (isUserAuthenticated) {
            return true;
        } else {
            this.router.navigate(['/account/login']);
        }
    }

    canActivate(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<boolean> | boolean {
        return this.canActivateInternal(route, state);
    }

    canLoad(route: Route): Observable<boolean> | Promise<boolean> | boolean {
        let isUserAuthenticated = this.authService.isUserAuthenticated();
        if (isUserAuthenticated) {
            return true;
        } else {
            this.router.navigate(['/account/login']);
        }
    }
}
