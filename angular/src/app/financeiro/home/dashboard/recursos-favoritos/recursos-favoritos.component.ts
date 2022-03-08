import { Component, OnInit, Injector, Input } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { PapelDto,  NavigationDto, DashboardServicoServiceProxy, MenuServicoServiceProxy} from '@shared/service-proxies/service-proxies';
import { Router } from '@angular/router';
import { SlideShowServiceService } from '../slide-show-service.service';
import { AppSessionService } from '@shared/common/session/app-session.service';
import { PapelService } from '@app/shared/services/papel.service';
import { finalize } from 'rxjs/operators';
import { NavegacaoService } from '@app/shared/services/navegacao.service';
import { UsuarioService } from '@app/shared/services/usuario.service';

@Component({
  selector: 'app-recursos-favoritos',
  templateUrl: './recursos-favoritos.component.html',
  styleUrls: ['./recursos-favoritos.component.css']
})
export class RecursosFavoritosComponent extends AppComponentBase implements OnInit {
  public papelAtual: PapelDto;
  favoritos: NavigationDto[] = [];
  carregandoFavoritos: Boolean=true;
  @Input('cols') colunas: any;

  constructor(
    injector: Injector,
    private _servicoHome: DashboardServicoServiceProxy,
    private _sessionService: AppSessionService,
    private _papelService: PapelService,
    private navegacaoService: NavegacaoService,
    private menuServico: MenuServicoServiceProxy,
    private usuarioService: UsuarioService
  ) {
    super(injector);
  }

  ngOnInit() {
    this._papelService.onTrocarDePapel.subscribe(papel => {
      this.papelAtual = papel;
      this.carregandoFavoritos = true;
      this.favoritos=[];
      this.obterRecursosFavoritos();
    });
  }

  obterRecursosFavoritos() {
     this._servicoHome.obterRecursosFavoritos(this._sessionService.user.siga2UserName, this.papelAtual)
      .pipe(finalize(() => this.carregandoFavoritos = false))
      .subscribe(p => {
        this.favoritos = p;
      });
  }
  
  obterClass(): String {
    return 'col-md-' + this.colunas + ' col-12 float-right';
  }

  navega(item: NavigationDto): void { 
    this.navegacaoService.navigateTo(item);    
  }

  remove(item: NavigationDto): void { 
        this.message.confirm('Deseja excluir o ' + item.name + ' da sua lista de favoritos?', (p) => {
          if(p){
            this.menuServico.removerFavorito(
            this.usuarioService.obterUserName(), 
            this._papelService.obterPapelAtual().id, 
            item.id)
            .subscribe( () => {
              abp.message.success("Registro excluído com sucesso!");
              this.obterRecursosFavoritos();
            })
          }
      });
  }

}
