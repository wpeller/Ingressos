import { ActivatedRoute, Router } from '@angular/router';
import { AppConsts } from '@shared/AppConsts';
import { AuthService } from '@app/shared/services/auth.service';
import { Component, OnInit } from '@angular/core';
import { TokenAuthServiceProxy, MenuServicoServiceProxy } from '@shared/service-proxies/service-proxies';
import { TokenService } from 'abp-ng2-module/dist/src/auth/token.service';
import { UtilsService } from 'abp-ng2-module/dist/src/utils/utils.service';
import { PapelService } from '@app/shared/services/papel.service';
import { UsuarioService } from '@app/shared/services/usuario.service';

@Component({
  selector: 'app-external-login',
  templateUrl: './external-login.component.html',
  styleUrls: ['./external-login.component.css']
})
export class ExternalLoginComponent implements OnInit {

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private _tokenAuthService: TokenAuthServiceProxy,
    private _tokenService: TokenService,
    private _utilsService: UtilsService,
    private _authService: AuthService,
    private _menuServico: MenuServicoServiceProxy,
    private _papelService: PapelService,
    private usuarioService: UsuarioService) { }

  ngOnInit() {
    this.activatedRoute.queryParams.subscribe(params => {
      let token = params['token'];
      this._tokenAuthService.externalLoginToken(token).toPromise().then(x => {
        let tokenExpireDate = (new Date(new Date().getTime() + 1000 * x.expireInSeconds));

        this._tokenService.setToken(
          x.accessToken,
          tokenExpireDate
        );

        this._utilsService.setCookieValue(
            AppConsts.authorization.encrptedAuthTokenName,
            x.encryptedAccessToken,
            tokenExpireDate,
            abp.appPath
        );
        abp.ui.setBusy();

        this._utilsService.setCookieValue(
          `LastRoleUsed_${x.data.usuarioLogado}`,
          btoa(x.data.idPapel.toString()),
          (new Date(new Date().getTime() + 60 * 60 * 24 * 1000)),
          abp.appPath);

          let papel = this._papelService.obterPapelAtual();

          if (papel) {
              this._menuServico
                  .gravarLogDeAcesso(
                    x.data.usuarioLogado,
                      papel.nome,
                      x.data.rota,
                      x.data.rota
                  )
                  .subscribe();
          } else {
              this._papelService.onTrocarDePapel.subscribe((p) => {
                  papel = p;
                  this._menuServico
                      .gravarLogDeAcesso(
                        x.data.usuarioLogado,
                          papel.nome,
                          x.data.rota,
                          x.data.rota
                      )
                      .subscribe();
              });
          }

          location.href = `${AppConsts.appBaseUrl}/${x.data.rota}`;

        //let papel = this.papelService.obterPapelPorId(x.data.idPapel);
        //this.papelService.trocarPapel(papel);
        //this.router.navigate([x.data.rota]).then();
      });
    });
  }
}
