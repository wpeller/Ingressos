import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AvisosHomeComponent } from './home/dashboard/avisos-home/avisos-home.component';
import { CategoriaDocumentosHomeComponent } from './home/dashboard/categoria-documentos-home/categoria-documentos-home.component';
import { MaisVisitadosComponent } from './home/dashboard/mais-visitados/mais-visitados.component';
import { RecursosFavoritosComponent } from './home/dashboard/recursos-favoritos/recursos-favoritos.component';
import { SistemasExternosComponent } from './home/dashboard/sistemas-externos/sistemas-externos.component';
import { SlideShowHomeComponent } from './home/dashboard/slide-show-home/slide-show-home.component';
import { DashboardComponent } from './home/dashboard/dashboard.component';
import { BreadCrumbModule } from '@app/shared/layout/bread-crumb-default/bread-crumb-default.module';
import { AppCommonModule } from '@app/shared/common/app-common.module';
import { AutoCompleteModule, ContextMenuModule, DragDropModule, DropdownModule, EditorModule, FileUploadModule, InputMaskModule, InputSwitchModule, KeyFilterModule, OverlayPanelModule, PaginatorModule, PanelModule, PickListModule, TreeModule, PasswordModule } from 'primeng/primeng';
import { BsDatepickerModule, BsDropdownModule, ButtonsModule, CarouselModule, CollapseModule, ModalModule, PopoverModule, TabsModule, TooltipModule } from 'ngx-bootstrap';
import { CountoModule } from 'angular2-counto';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { ImageCropperModule } from 'ngx-image-cropper';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { TableModule } from 'primeng/table';
import { TextMaskModule } from 'angular2-text-mask';
import { ServiceProxyModule } from '@shared/service-proxies/service-proxy.module';
import { UtilsModule } from '@shared/utils/utils.module';
import { DashboardServicoServiceProxy } from '@shared/service-proxies/service-proxies';
import { FinanceiroRoutingModule } from './financeiro.routing.module';
import { PaginaTesteComponent } from './sso-teste/pagina-teste.component';
import { FgvCardSearchModule } from '@app/shared/component/fgv-card-search/fgv-card-search.module';

@NgModule({
  declarations: [
    AvisosHomeComponent,
    CategoriaDocumentosHomeComponent,
    MaisVisitadosComponent,
    RecursosFavoritosComponent,
    SistemasExternosComponent,
    SlideShowHomeComponent,
    DashboardComponent,
    PaginaTesteComponent
  ],
  imports: [
    AppCommonModule,
    AutoCompleteModule,
    BreadCrumbModule,
    BsDatepickerModule.forRoot(),
    BsDropdownModule.forRoot(),
    ButtonsModule.forRoot(),
    CarouselModule.forRoot(),
    CollapseModule.forRoot(),
    CommonModule,
    ContextMenuModule,
    CountoModule,
    DragDropModule,
    DropdownModule,
    EditorModule,
    FileUploadModule,
    FormsModule,
    HttpModule,
    ImageCropperModule,
    InputMaskModule,
    InputSwitchModule,
    KeyFilterModule,
    ModalModule.forRoot(),
    NgxChartsModule,
    OverlayPanelModule,
    PaginatorModule,
    PanelModule,
    PickListModule,
    PopoverModule.forRoot(),
    ReactiveFormsModule,
    TableModule,
    TabsModule.forRoot(),
    TextMaskModule,
    TooltipModule.forRoot(),
    TreeModule,
    FinanceiroRoutingModule,
    ServiceProxyModule,
    UtilsModule,
    PasswordModule,
    FgvCardSearchModule
  ],
  exports:[
    AvisosHomeComponent,
    CategoriaDocumentosHomeComponent,
    MaisVisitadosComponent,
    RecursosFavoritosComponent,
    SistemasExternosComponent,
    SlideShowHomeComponent,
    DashboardComponent
  ],
  providers: [
    DashboardServicoServiceProxy
  ]
})
export class FinanceiroModule { }
