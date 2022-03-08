import { CommonModule } from '@angular/common';
import { DatePipe } from '@angular/common';
import { DateValidation } from '@app/shared/common/date/date.validation';
import { DynamicPipe } from '@app/shared/pipes/dynamic-pipe';
import { FgvButtonIncluirModule } from './../fgv-button-incluir/fgv-button-incluir.module';
import { FgvButtonsBuscarModule } from './../fgv-buttons-buscar/fgv-buttons-buscar.module';
import { FgvButtonsModalModule } from '../fgv-buttons-modal/fgv-buttons-modal.module';
import { FgvButtonsSalvarModule } from './../fgv-buttons-salvar/fgv-buttons-salvar.module';
import { FgvCardFormModule } from '../fgv-card-form/fgv-card-form.module';
import { FgvCardModule } from './../fgv-card/fgv-card.module';
import { FgvCardSearchModule } from '../fgv-card-search/fgv-card-search.module';
import { FgvCardSectionModule } from '../fgv-card-section/fgv-card-section.module';
import { FgvInputAutocompleteModule } from '../fgv-input-autocomplete/fgv-input-autocomplete.module';
import { FgvInputCapitalModule } from '../fgv-input-capital/fgv-input-capital.module';
import { FgvInputDateModule } from '../fgv-input-date/fgv-input-date.module';
import { FgvInputEditorModule } from '../fgv-input-editor/fgv-input-editor.module';
import { FgvInputEmailModule } from './../fgv-input-email/fgv-input-email.module';
import { FgvInputMaskModule } from './../fgv-input-mask/fgv-input-mask.module';
import { FgvInputMoneyModule } from '../fgv-input-money/fgv-input-money.module';
import { FgvInputMultiselectModule } from '../fgv-input-multiselect/fgv-input-multiselect.module';
import { FgvInputNumberModule } from './../fgv-input-number/fgv-input-number.module';
import { FgvInputPasswordModule } from '../fgv-input-password/fgv-input-password.module';
import { FgvInputPhoneModule } from './../fgv-input-phone/fgv-input-phone.module';
import { FgvInputPickListModule } from '../fgv-input-picklist/fgv-input-picklist.module';
import { FgvInputRadioModule } from './../fgv-input-radio/fgv-input-radio.module';
import { FgvInputSelectModule } from './../fgv-input-select/fgv-input-select.module';
import { FgvInputSwitchModule } from './../fgv-input-switch/fgv-input-switch.module';
import { FgvInputTextModule } from './../fgv-input-text/fgv-input-text.module';
import { FgvModalModule } from '../fgv-modal/fgv-modal.module';
import { FgvPaginatorModule } from './../fgv-paginator/fgv-paginator.module';
import { FgvTableActionsDeleteModule } from '../fgv-table-actions-delete/fgv-table-actions-delete.module';
import { FgvTableActionsEditModule } from '../fgv-table-actions-edit/fgv-table-actions-edit.module';
import { FgvTableActionsModule } from './../fgv-table-actions/fgv-table-actions.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputMaskModule } from 'primeng/inputmask';
import { KeyFilterModule } from 'primeng/keyfilter';
import { NgModule } from '@angular/core';
import { NgxBootstrapDatePickerConfigService } from 'assets/ngx-bootstrap/ngx-bootstrap-datepicker-config.service';
import {
    BsDatepickerConfig,
    BsDatepickerModule,
    BsDaterangepickerConfig,
    BsDropdownModule,
    BsLocaleService,
    ModalModule,
    TabsModule,
    TooltipModule,
} from 'ngx-bootstrap';
import {
    FileUploadModule as PrimeNgFileUploadModule,
    InputSwitchModule,
    PaginatorModule,
    PickListModule,
    ProgressBarModule,
    InputTextModule,
} from 'primeng/primeng';

@NgModule({
    imports: [
        BsDatepickerModule,
        BsDropdownModule,
        CommonModule,
        CommonModule,
        FormsModule,
        InputMaskModule,
        InputSwitchModule,
        InputTextModule,
        KeyFilterModule,
        ModalModule,
        PaginatorModule,
        PickListModule,
        PrimeNgFileUploadModule,
        ProgressBarModule,
        ReactiveFormsModule,
        TabsModule,
        TooltipModule,
    ],
    declarations: [DynamicPipe],
    exports: [
        DynamicPipe,
        FgvButtonIncluirModule,
        FgvButtonsBuscarModule,
        FgvButtonsModalModule,
        FgvButtonsSalvarModule,
        FgvCardModule,
        FgvCardFormModule,
        FgvCardSearchModule,
        FgvCardSectionModule,
        FgvInputAutocompleteModule,
        FgvInputCapitalModule,
        FgvInputDateModule,
        FgvInputEditorModule,
        FgvInputEmailModule,
        FgvInputMaskModule,
        FgvInputMoneyModule,
        FgvInputMultiselectModule,
        FgvInputNumberModule,
        FgvInputPasswordModule,
        FgvInputPhoneModule,
        FgvInputPickListModule,
        FgvInputRadioModule,
        FgvInputSelectModule,
        FgvInputSwitchModule,
        FgvInputTextModule,
        FgvModalModule,
        FgvPaginatorModule,
        FgvTableActionsModule,
        FgvTableActionsDeleteModule,
        FgvTableActionsEditModule
    ],
    providers: [
        DatePipe,
        DateValidation,
        { provide: BsDatepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerConfig },
        { provide: BsDaterangepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDaterangepickerConfig },
        { provide: BsLocaleService, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerLocale },
    ],
})
export class FgvComponentModule { }
