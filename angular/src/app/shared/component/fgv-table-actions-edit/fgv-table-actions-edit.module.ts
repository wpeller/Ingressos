import { BsDropdownModule } from 'ngx-bootstrap';
import { DropdownModule } from 'primeng/primeng';
import { FgvTableActionsEditComponent } from './fgv-table-actions-edit.component';
import { NgModule } from '@angular/core';

@NgModule({
	imports: [ BsDropdownModule.forRoot(), DropdownModule ],
	declarations: [ FgvTableActionsEditComponent ],
	exports: [ FgvTableActionsEditComponent ],
	bootstrap: [ FgvTableActionsEditComponent ],
})
export class FgvTableActionsEditModule {}
