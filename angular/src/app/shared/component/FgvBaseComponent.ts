import * as _ from 'lodash';
import { ControlValueAccessor, FormGroup } from '@angular/forms';
import { EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FgvValidation } from './FgvValidation';

export abstract class FgvBaseComponent implements ControlValueAccessor, OnInit {
    get value(): any {
        return this.innerValue;
    }

    set value(v: any) {
        this.writeValue(v);
    }
    protected innerValue: any;
    protected erros: string[] = [];

    @Input() public label: string;
    @Input() public id: string;
    @Input() public name: string;
    @Input() public style: string;
    @Input() public class: string;
    @Input() public classInput: string;
    @Input() public formGroup: FormGroup;
    @Input() public formControlName: string;
    @Input() public required: boolean;
    @Input() public readonly: boolean;
    @Input() public placeholder = '';

    @Output() protected change: EventEmitter<any> = new EventEmitter<any>();
    @Output() protected focusout: EventEmitter<any> = new EventEmitter<any>();

    ngOnInit() { }

    changeHandler(event): void {
        this.change.emit(event);
    }

    focusoutHandler(event): void {
        this.focusout.emit(event);
    }

    writeValue(obj: any): void {
        this.innerValue = obj;
    }

    registerOnChange(fn: Function): void { }

    registerOnTouched(fn: Function): void { }

    setDisabledState(isDisabled: boolean) { }

    public get informacaoCampo() {
        return this.formGroup.get(this.formControlName);
    }

    public get verificaEstado() {
        return this.informacaoCampo.invalid && (this.informacaoCampo.dirty || this.informacaoCampo.touched);
    }

    public get errorMessage() {
        this.erros = [];
        for (const propertyName in this.informacaoCampo.errors) {
            if (
                this.formGroup.get(this.formControlName).errors.hasOwnProperty(propertyName) &&
                this.formGroup.get(this.formControlName).touched
            ) {
                let erro = FgvValidation.getErrorMsg(
                    this.label,
                    propertyName,
                    this.informacaoCampo.errors[propertyName]
                );
                this.erros.push(erro);
            }
        }
        return this.erros;
    }
}
