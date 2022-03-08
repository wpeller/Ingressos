import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { DashboardComponent } from "./home/dashboard/dashboard.component";
import { PaginaTesteComponent } from "./sso-teste/pagina-teste.component";

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    { path: 'home/dashboard', component: DashboardComponent },
                    { path: 'SSO/teste', component: PaginaTesteComponent},

                    {
                        path: 'relatorios',
                        loadChildren: 'app/financeiro/relatorios/relatorios.module#RelatoriosModule', //Lazy load admin module
                        data: { preload: true }
                    }
                ],

            }
        ])
    ],
    exports: [
        RouterModule
    ]
})

export class FinanceiroRoutingModule { }
