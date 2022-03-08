import { Component, OnInit, Injector, Input } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
import { PapelService } from '@app/shared/services/papel.service';
import { AppSessionService } from '@shared/common/session/app-session.service';
import { DashboardServicoServiceProxy, 
  PapelDto, AcessoRecursoDto, 
  NavigationDto, MenuServicoServiceProxy, ObterItemDeMenuPorIdInput } from '@shared/service-proxies/service-proxies';
import { NavegacaoService } from '@app/shared/services/navegacao.service';
import { NG_PROJECT_AS_ATTR_NAME } from '@angular/core/src/render3/interfaces/projection';
import { AppConsts } from '@shared/AppConsts';

@Component({
  selector: 'app-mais-visitados',
  templateUrl: './mais-visitados.component.html',
  styleUrls: ['./mais-visitados.component.css']
})
export class MaisVisitadosComponent extends AppComponentBase implements OnInit {
  acessos: AcessoRecursoDto[] = [];
  private papelAtual: PapelDto;
  carregandoMaisVisitados: Boolean;
  @Input("cols") colunas: any;

  constructor(
    injector: Injector,
    private _servicoHome: DashboardServicoServiceProxy,
    private _sessionService: AppSessionService,
    private _papelService: PapelService,
    private navegacaoService: NavegacaoService,
    private menuServico: MenuServicoServiceProxy
  ) {
    super(injector);
  }

  ngOnInit() {
    this._papelService.onTrocarDePapel.subscribe(papel => {
      this.papelAtual = papel;
      this.acessos=[];
      this.carregandoMaisVisitados = true;
      this.obterMaisVisitados();
    });
  }

  private obterMaisVisitados() {
    this._servicoHome.obterMaisVisitados(this._sessionService.user.siga2UserName, this.papelAtual)
    .pipe(finalize(() => this.carregandoMaisVisitados = false))
    .subscribe(p => {
      this.acessos = p;
    });

  }
  
  obterClass(): String {
    return 'col-md-' + this.colunas + ' col-12 float-left';
  }


  navega(acesso: AcessoRecursoDto): void { 
    let input = new ObterItemDeMenuPorIdInput();
    input.idItemMenu = acesso.id;
    abp.ui.setBusy();
    this.menuServico.obterItemDeMenuPorId(input)
    .pipe(finalize(() => abp.ui.clearBusy()))
    .subscribe(p => {  
      let navigate = new NavigationDto();
      navigate.moduloAcesso= p.moduloAcesso;
      navigate.id = p.id;
      navigate.rota = p.rota;
      this.navegacaoService.navigateTo(navigate);
    });   
    
  }

  
}
