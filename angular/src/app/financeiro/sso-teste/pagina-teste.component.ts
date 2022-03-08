import { Component, OnInit, Injector } from '@angular/core';
import { AppComponentBaseSiga2 } from '@shared/common/app-component-base-siga2';
import { AppConsts } from '@shared/AppConsts';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
   selector: 'app-pagina-teste',
   templateUrl: './pagina-teste.component.html',
   animations: [appModuleAnimation()]
 })
 export class PaginaTesteComponent extends AppComponentBaseSiga2 implements OnInit {

  public modulo = '';
  constructor(injector: Injector ) {
    super(injector);
  }

  ngOnInit() {
    this.modulo = AppConsts.moduloAcesso;
  }
 }
