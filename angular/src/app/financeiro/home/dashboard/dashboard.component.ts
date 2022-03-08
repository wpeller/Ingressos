import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
  Component,
  Injector,
  OnInit
} from '@angular/core';
import { MenuServicoServiceProxy, ObterItemMenuInput } from '@shared/service-proxies/service-proxies';
import { NavegacaoService } from '@app/shared/services/navegacao.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  animations: [appModuleAnimation()]
})
export class DashboardComponent extends AppComponentBase implements OnInit {
  constructor(
    injector: Injector,
    private _menuService: MenuServicoServiceProxy,
    public navegacaoService: NavegacaoService
  ) {
    super(injector);
  }

  ngOnInit() {
    //navegar para a home do HUB
    let filtro = new ObterItemMenuInput();
    filtro.nome = 'Home';
    this._menuService.obterItemMenu(filtro).subscribe(item => {
      //this.navegacaoService.navigateTo(item);
	  if (location.href.indexOf("localhost")===0){
          this.navegacaoService.navigateTo(item);
      }
    })
  }


}
