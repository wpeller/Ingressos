import { Injectable } from '@angular/core';
import { SessionServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppSessionService } from '@shared/common/session/app-session.service';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  constructor(private _sessionService : AppSessionService) { }

  public obterUserName() : string{
    if (!this._sessionService.user.siga2UserName) {
      return this._sessionService.user.userName;
    }
    return this._sessionService.user.siga2UserName;

  }


  public obterNomeUsuario(): string {
    if (!this._sessionService.user.siga2UserName) {
      return this._sessionService.user.name;
    }
    return this._sessionService.user.siga2UserName;
  }
  
}
