import { CommonModule } from '@angular/common';
import { FgvPaginatorComponent } from './fgv-paginator.component';
import { NgModule } from '@angular/core';
import { PaginatorModule } from 'primeng/primeng';

@NgModule({
    imports: [
        CommonModule,
        PaginatorModule
    ],
    declarations: [ FgvPaginatorComponent ],
    exports: [  FgvPaginatorComponent ],
    bootstrap: [ FgvPaginatorComponent ]
})
export class FgvPaginatorModule { }
