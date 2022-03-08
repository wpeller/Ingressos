import { Component, OnInit, Injector, Input } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import {DashboardServicoServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppSessionService } from '@shared/common/session/app-session.service';
import { PapelService } from '@app/shared/services/papel.service';
import { Router } from '@angular/router';
import { TreeNode } from 'primeng/primeng';

@Component({
  selector: 'app-sistemas-externos',
  templateUrl: './sistemas-externos.component.html',
  styleUrls: ['./sistemas-externos.component.css']
})
export class SistemasExternosComponent extends AppComponentBase implements OnInit {

  @Input("cols") colunas: any;
  sistemasExternos: TreeNode[] = []
  constructor(
    injector: Injector,
    private _servicoHome:DashboardServicoServiceProxy,
    private _sessionService: AppSessionService,
    private _papelService: PapelService,
    private _router: Router
  ) {
    super(injector);
  }

  ngOnInit() {
    this.sistemasExternos=[];
    this.sistemasExternos.push({label: "PORTAL ALUNO", collapsedIcon: "fas fa-minus",  data: "https://aluno.fgv.br", expanded: true})
    this.sistemasExternos.push({label: "PORTAL IDE", collapsedIcon: "fas fa-minus",  data: "https://educacao-executiva.fgv.br", expanded: true})
    this.sistemasExternos.push({label: "PORTAL DOS PROFESSORES", collapsedIcon: "fas fa-minus",  data: "https://professoreside.fgv.br", expanded: true})
    this.sistemasExternos.push({label: "SEC", collapsedIcon: "fas fa-minus",  data: "https://www15.fgv.br/sec/", expanded: true})
    this.sistemasExternos.push({label: "SGP", collapsedIcon: "fas fa-minus",  data: "https://sgp.fgv.br/mgm", expanded: true})
    this.sistemasExternos.push({label: "SALA VIRTUAL", collapsedIcon: "fas fa-minus", data: "https://www.fgv.br/salavirtual", expanded: true})

  }

  obterClass(): String {
    return 'col-md-' + this.colunas + ' col-12 float-left';
  }

  navigate(nome: string): void {
    if (nome === 'sgp') {
      this._router.navigate(['https://sgphomolog.fgv.br/mgm/']);
    } else if (nome === 'sala') {
      this._router.navigate(['https://sigaf.fgv.br/svphomo/SigaSVP/']);
    } else if (nome === 'network') {
      this._router.navigate(['https://sigaf.fgv.br/svphomo/SigaSVP/']);
    }
  }

}
