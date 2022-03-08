import { Injector, OnInit } from "@angular/core";
import { FormBuilder, FormGroup } from "@angular/forms";
import { PrimengTableHelper } from "@shared/helpers/PrimengTableHelper";
import { AppComponentBaseSiga2 } from "./app-component-base-siga2";

export abstract class AppComponentConsultaBaseSiga2 extends AppComponentBaseSiga2 implements OnInit{
    _builder: FormBuilder;
    contemDadosParaBusca: boolean;
    formConsulta: FormGroup;
    primengTableHelper: PrimengTableHelper;
    existeBusca: boolean;

    constructor(injector: Injector){
        super(injector);
        this._builder = injector.get(FormBuilder);
        this.primengTableHelper = new PrimengTableHelper();
    }

    ngOnInit(): void{
        this.contemDadosParaBusca = false;
        this.geraFormulario();
    }

    abstract geraFormulario(): void;
    resetForm():  void{
        this.geraFormulario();
    }

    public limpar(): void {
        this.resetForm();
        this.contemDadosParaBusca = false;
        this.primengTableHelper.totalRecordsCount = 0;
        this.primengTableHelper.records = [];
    }


}
