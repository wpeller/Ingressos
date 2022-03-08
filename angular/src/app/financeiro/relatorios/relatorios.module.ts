import { AlunoServicoServiceProxy, CursoServicoServiceProxy, UnidadeServicoServiceProxy, TurmaServicoServiceProxy, PlanoComExcessoDeParcelamentoServiceProxy } from './../../../shared/service-proxies/service-proxies';

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RelatoriosRoutingModule } from './relatorios-routing.module';
import { FgvModalModule } from '@app/shared/component/fgv-modal/fgv-modal.module';
import { BreadCrumbModule } from '@app/shared/layout/bread-crumb-default/bread-crumb-default.module';
import { UtilsModule } from '@shared/utils/utils.module';
import { FgvCardSearchModule } from '@app/shared/component/fgv-card-search/fgv-card-search.module';
import { ModalConsultaAlunoModule, ModalConsultaTurmaModule, ModalConsultaUnidadeModule } from 'ngx-siga2-modais';
import { FgvInputSelectModule } from '@app/shared/component/fgv-input-select/fgv-input-select.module';
import { FgvInputMultiselectModule } from '@app/shared/component/fgv-input-multiselect/fgv-input-multiselect.module';
import { FgvButtonsBuscarModule } from '@app/shared/component/fgv-buttons-buscar/fgv-buttons-buscar.module';
import { CalendarModule, CheckboxModule, ChipsModule, InputTextareaModule, PaginatorModule, ProgressSpinnerModule, RadioButtonModule } from 'primeng/primeng';
import { FgvPaginatorModule } from '@app/shared/component/fgv-paginator/fgv-paginator.module';
import { TableModule } from 'primeng/table';
import { PlanosComExcessoDeParcelamentoComponent } from './planos-com-excesso-de-parcelamento/planos-com-excesso-de-parcelamento.component';
import { FgvInputDateModule, FgvInputTextModule } from 'ngx-siga2-componentes';
import { AppCommonModule } from '@app/shared/common/app-common.module';
import { BsDatepickerModule, BsDropdownModule, ButtonsModule, CarouselModule, CollapseModule } from 'ngx-bootstrap';
import { HttpModule } from '@angular/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ServiceProxyModule } from '@shared/service-proxies/service-proxy.module';
import { FgvComponentModule } from '@app/shared/component/FgvComponentModule/fgv-components.module';
import { FgvCardSectionModule } from '@app/shared/component/fgv-card-section/fgv-card-section.module';

@NgModule({
  declarations: [PlanosComExcessoDeParcelamentoComponent],
  imports: [
    CommonModule,
    RelatoriosRoutingModule,
    BreadCrumbModule,
    UtilsModule,
    TableModule,
    AppCommonModule,
    BsDatepickerModule.forRoot(),
    BsDropdownModule.forRoot(),
    ButtonsModule.forRoot(),
    CarouselModule.forRoot(),
    CollapseModule.forRoot(),
    FormsModule,
    ReactiveFormsModule,
    ServiceProxyModule,
    PaginatorModule,
    ChipsModule,
    RadioButtonModule,
    CheckboxModule,
    InputTextareaModule,
    ModalConsultaAlunoModule,
    ModalConsultaTurmaModule,
    ModalConsultaUnidadeModule,
    FgvCardSearchModule,
    FgvInputSelectModule,
    FgvButtonsBuscarModule,
    FgvModalModule,
    FgvComponentModule,
    FgvCardSectionModule,
    FgvInputTextModule,
    ProgressSpinnerModule,
    FgvPaginatorModule,
    CalendarModule
  ],

  providers: [
      CursoServicoServiceProxy,
      UnidadeServicoServiceProxy,
      AlunoServicoServiceProxy,
      TurmaServicoServiceProxy,
      PlanoComExcessoDeParcelamentoServiceProxy
  ]

})
export class RelatoriosModule { }
