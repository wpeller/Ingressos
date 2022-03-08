import { CommonModule } from '@angular/common';
import { EditorModule, InputTextModule } from 'primeng/primeng';
import { FgvInputEditorComponent } from './fgv-input-editor.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';

@NgModule({
  imports: [
    CommonModule,
    EditorModule,
    FormsModule,
    InputTextModule,
    ReactiveFormsModule
  ],
  declarations: [FgvInputEditorComponent],
	exports: [ FgvInputEditorComponent ],
	bootstrap: [ FgvInputEditorComponent ],
})
export class FgvInputEditorModule { }
