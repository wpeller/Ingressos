import { CommonModule } from '@angular/common';
import { FgvCardSearchComponent } from './fgv-card-search.component';
import { NgModule } from '@angular/core';
import { UtilsModule } from '@shared/utils/utils.module';

@NgModule({
	imports: [ CommonModule, UtilsModule ],
	declarations: [ FgvCardSearchComponent ],
	exports: [ FgvCardSearchComponent ],
})
export class FgvCardSearchModule {}
