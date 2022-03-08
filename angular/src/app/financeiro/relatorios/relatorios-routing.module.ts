
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PlanosComExcessoDeParcelamentoComponent } from './planos-com-excesso-de-parcelamento/planos-com-excesso-de-parcelamento.component';

const routes: Routes = [
    {
        path: 'plano-com-excesso-de-parcelamento',
        component: PlanosComExcessoDeParcelamentoComponent
    }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RelatoriosRoutingModule { }
