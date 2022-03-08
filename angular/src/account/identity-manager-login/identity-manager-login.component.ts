import { ActivatedRoute } from '@angular/router';
import { AppConsts } from '@shared/AppConsts';
import { AuthService } from '@app/shared/services/auth.service';
import { Component, OnInit } from '@angular/core';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { TokenService } from 'abp-ng2-module/dist/src/auth/token.service';
import { UtilsService } from 'abp-ng2-module/dist/src/utils/utils.service';

@Component({
  selector: 'app-identity-manager-login',
  templateUrl: './identity-manager-login.component.html',
  styleUrls: ['./identity-manager-login.component.css']
})
export class IdentityManagerLoginComponent implements OnInit {

  public freeze = true;
  constructor(
    private _activatedRoute: ActivatedRoute,
    private _authService: AuthService,
    private _tokenAuthService: TokenAuthServiceProxy,
    private _tokenService: TokenService,
    private _utilsService: UtilsService) { }

  ngOnInit() {
    //console.log('Entrei para validação');
    this._activatedRoute.queryParams.subscribe(params => {
      let token = params['token'];
      //console.log(token);
      this._tokenAuthService.identityManagerLogin(token).subscribe(x => {

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

        location.href = `${AppConsts.appBaseUrl}/app/dashboard`;
      }, error => {
          let url = `${AppConsts.urlIdentityManager}${AppConsts.idAplicacaoIdentityManager}`;
          url += `&returnurl=${AppConsts.appBaseUrl}/account/identitymanagerlogin?token=&invalid=true`;
          location.href = url;
        });
    });
  }
}
