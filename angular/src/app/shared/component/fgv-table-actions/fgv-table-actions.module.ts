import { BsDropdownModule } from 'ngx-bootstrap';
import { DropdownModule } from 'primeng/primeng';
import { FgvTableActionsComponent } from './fgv-table-actions.component';
import { NgModule } from '@angular/core';

@NgModule({
	imports: [ BsDropdownModule.forRoot(), DropdownModule ],
	declarations: [ FgvTableActionsComponent ],
	exports: [ FgvTableActionsComponent ],
	bootstrap: [ FgvTableActionsComponent ],
})
export class FgvTableActionsModule {}
