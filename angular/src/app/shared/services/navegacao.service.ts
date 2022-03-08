import { AppConsts } from '@shared/AppConsts';
import {
  ApplicationServiceProxy,
  ApplicationTokenData,
  MenuServicoServiceProxy,
  NavigationDto,
  ObterItemMenuInput,
  RegistrarAcessoRecursoInput
  } from '@shared/service-proxies/service-proxies';
import { finalize } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { MessageService } from 'abp-ng2-module/dist/src/message/message.service';
import { Observable } from 'rxjs';
import { PapelService } from './papel.service';
import { Router } from '@angular/router';
import { UsuarioService } from './usuario.service';

@Injectable({
  providedIn: 'root'
})
export class NavegacaoService {

  papel: number;
  public navegacaoAtual: NavigationDto;

  constructor(
    private _router: Router,
    private usuarioService: UsuarioService,
    private _applicationService: ApplicationServiceProxy,
    private _messageService: MessageService,
    private _papelService: PapelService,
    private _menuServico: MenuServicoServiceProxy) { }

  navigateTo(item: NavigationDto): void {
    if (item.moduloAcesso !== AppConsts.moduloAcesso) {
      this.ssoSend(item);
      return;
    }
    let papel = this._papelService.obterPapelAtual();
        if (papel) {
            this._menuServico
                .gravarLogDeAcesso(
                    this.usuarioService.obterUserName(),
                    papel.nome,
                    item.name,
                    item.rota
                )
                .subscribe();
        } else {
            this._papelService.onTrocarDePapel.subscribe((p) => {
                papel = p;
                this._menuServico
                    .gravarLogDeAcesso(
                        this.usuarioService.obterUserName(),
                        papel.nome,
                        item.name,
                        item.rota
                    )
                    .subscribe();
            });
        }

    this.navegacaoAtual = item;
    this._router.navigate([item.rota]);
  }

  registrarAcesso() {
    abp.ui.setBusy();
    let input = new RegistrarAcessoRecursoInput();
    input.codigoExternoUsuario = this.usuarioService.obterUserName();
    input.idPapel = this._papelService.obterPapelAtual().id;
    input.idItemMenu = this.navegacaoAtual.id;

    this._menuServico.registrarAcessoRecurso(input)
      .pipe(finalize(() => abp.ui.clearBusy() ))
      .subscribe();
  }

  navigate(rota: string): void {
    let rotaOriginal = rota;
    if (rota.indexOf('editar') > 0) {
      let index = rota.indexOf('editar');
      rota = rota.substring(0, index - 1);
      rota = rota + '/consulta';
    }
    if (rota.indexOf('dashboard') === -1) {
      this.getNavigationItem(rota)
      .subscribe(navigation => {
        if (navigation) {
        let navigationItem;
          if (navigation.rota === rota) {
            navigationItem = navigation;
            navigationItem.rota = rotaOriginal;
            this.navigateTo(navigationItem);
            return;
          }
          if (!navigationItem) {
            this._messageService.error('Rota não-encontrada!');
          }
        }
      });
    }
  }

  public getNavigationItem(rota: string): Observable<NavigationDto> {
    if (rota.startsWith('/')) {
      rota = rota.substring(1);
    }
    let input = new ObterItemMenuInput();
    input.id = undefined;
    input.titulo = undefined;
    input.nome = undefined;
    input.rota = rota;
     return this._menuServico.obterItemMenu(input);
  }

  private ssoSend(item: NavigationDto): void {
    let sendSso: ApplicationTokenData = new ApplicationTokenData();
    sendSso.idPapel = this._papelService.obterPapelAtual().id;
    sendSso.modulo = item.moduloAcesso;
    sendSso.idRecurso = item.id;
    sendSso.usuarioLogado = this.usuarioService.obterUserName();
    sendSso.rota = item.rota;

    this._applicationService.createToken(sendSso)
      .subscribe(p => {
        let url = p.url + "?token=" + p.token;
        location.href = url;
      });
  }

}

