import { EventEmitter, Injectable, OnInit } from '@angular/core';
import { ObterPapeisDoUsuarioParaMenuInput, PapelDto,PapelServicoServiceProxy, NavigationServiceProxy } from '@shared/service-proxies/service-proxies';
import { UsuarioService } from './usuario.service';
import { UtilsService } from 'abp-ng2-module/dist/src/utils/utils.service';
import { AppSessionService } from '@shared/common/session/app-session.service';

@Injectable({
  providedIn: 'root'
})
export class PapelService implements OnInit {

  constructor(private _papelServiceProxy:PapelServicoServiceProxy,
    private _usuarioService: UsuarioService,
    private sessionService: AppSessionService,
    private _utilsService: UtilsService
  ) { }

  public onTrocarDePapel: EventEmitter<PapelDto> = new EventEmitter();
  public iniciarPapelService: EventEmitter<any> = new EventEmitter();
  private papelAtual: PapelDto = new PapelDto();
  private papeis: PapelDto[] = [];

  ngOnInit(): void {

  }

  public consultarPapeis(): Promise<PapelDto[]> {
    let input = new ObterPapeisDoUsuarioParaMenuInput();
    input.codigoExternoUsuario = this._usuarioService.obterUserName();
    return this._papelServiceProxy.obterPapeisDoUsuarioParaMenu(input).toPromise()
      .then(x => {
        this.papeis = x;
        return this.papeis;
      });
  }

  public obterPapeis(): Promise<PapelDto[]> {
    abp.ui.setBusy();
    let input = new ObterPapeisDoUsuarioParaMenuInput();
    input.codigoExternoUsuario = this._usuarioService.obterUserName();
    return this._papelServiceProxy.obterPapeisDoUsuarioParaMenu(input).toPromise()
      .then(x => {
        this.papeis = x;
        this.trocarPapel(this.obterUltimoPapelOuPrimeiroDaLista(this.papeis));
        abp.ui.clearBusy();
        return this.papeis;
      });
  }

  private obterUltimoPapelOuPrimeiroDaLista(papeis: PapelDto[]): PapelDto {

    if (!this.papeis || this.papeis.length === 0) {
      return new PapelDto();
    }

    let papelEmCookieCriptografado = this._utilsService.getCookieValue(`LastRoleUsed_${this._usuarioService.obterUserName()}`);

    //não pode mostrar o papel do SIGA 1 no primeiro acesso ou no log-On
    /*if (!papelEmCookieCriptografado || papelEmCookieCriptografado === '') {
    //  return this.papeis.find(x => x.nome.includes("S2 - "));
    }*/

    let papelEmCookie = +atob(papelEmCookieCriptografado);

    let papel = papeis.find(x => x.id === papelEmCookie);

    if (!papel || !papel.nome.includes("S2 - ")) {
      {
        papel = papeis.find(x => x.nome.includes("S2 - "));
        //return this.papeis[0];
      }
    }

    if (papel === undefined) {
      return this.papeis[0];
    }

    return papel;
  }

  public obterPapelAtual(): PapelDto {
    return this.papelAtual;
  }

  public trocarPapel(novoPapel: PapelDto): PapelDto {
    if (novoPapel.id == null) {
      this.onTrocarDePapel.emit(new PapelDto());
      return null;
    }

    let papelAnterior = this.papelAtual;
  //  console.log('Papel anterior', papelAnterior);
    this.papelAtual = novoPapel;
  //  console.log('Papel atual ', this.papelAtual);

    if (papelAnterior.id == null) {
      this.iniciarPapelService.emit(null);
    }

    this.onTrocarDePapel.emit(this.papelAtual);
    this._utilsService.setCookieValue(
      `LastRoleUsed_${this._usuarioService.obterUserName()}`,
      btoa(novoPapel.id.toString()),
      (new Date(new Date().getTime() + 60 * 60 * 24 * 1000)),
      abp.appPath);
    return this.papelAtual;
  }

  public obterUserName(): string {
    if (!this.sessionService.user.siga2UserName) {
      return this.sessionService.user.userName;
    }
    return this.sessionService.user.siga2UserName;
  }

}
