import { BsDropdownModule } from 'ngx-bootstrap';
import { DropdownModule } from 'primeng/primeng';
import { FgvTableActionsDeleteComponent } from './fgv-table-actions-delete.component';
import { NgModule } from '@angular/core';

@NgModule({
	imports: [ BsDropdownModule.forRoot(), DropdownModule ],
	declarations: [ FgvTableActionsDeleteComponent ],
	exports: [ FgvTableActionsDeleteComponent ],
	bootstrap: [ FgvTableActionsDeleteComponent ],
})
export class FgvTableActionsDeleteModule {}
