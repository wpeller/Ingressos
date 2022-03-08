import { Component, OnInit, Injector, Input } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { PapelDto, AvisoDto, DashboardServicoServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppSessionService } from '@shared/common/session/app-session.service';
import { PapelService } from '@app/shared/services/papel.service';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-avisos-home',
  templateUrl: './avisos-home.component.html',
  styleUrls: ['./avisos-home.component.css']
})
export class AvisosHomeComponent extends AppComponentBase implements OnInit {
  avisos: AvisoDto[] = [];  
  private papelAtual: PapelDto;
  @Input("cols") colunas: any;
  carregandoAviso: Boolean = true; 

  constructor(
    injector: Injector,
    private _servicoHome: DashboardServicoServiceProxy,
    private _sessionService: AppSessionService,
    private _papelService: PapelService
  ) {
    super(injector);
  }

  ngOnInit() {
    this._papelService.onTrocarDePapel.subscribe(papel => {
      this.papelAtual = papel;
      this.avisos = [];
      this.carregandoAviso=true;
      this.obterAvisos();
    });
  }

  obterAvisos(): void{
    this._servicoHome.obterAvisosDoPapel(this.papelAtual)
    .pipe(finalize(() => this.carregandoAviso = false))
    .subscribe(p => {
      this.avisos = p;
    });
  }

  obterClass(): String {
    return 'col-md-' + this.colunas + ' col-12 float-right';
  }

  ver(item: AvisoDto){
    let html = "<div style='text-align: justify'>" + item.texto + "</div>";
    abp.message.info(html, item.titulo, true);
  }

}
