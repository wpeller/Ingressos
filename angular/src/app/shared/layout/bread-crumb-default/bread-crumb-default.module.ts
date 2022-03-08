import { BreadCrumbDefaultComponent } from './bread-crumb-default.component';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { UtilsModule } from '@shared/utils/utils.module';
import { AppCommonModule } from '@app/shared/common/app-common.module';

@NgModule({
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    UtilsModule,
    AppCommonModule
  ],
  declarations: [
    BreadCrumbDefaultComponent
  ], exports: [
    BreadCrumbDefaultComponent
  ]
})
export class BreadCrumbModule { }
