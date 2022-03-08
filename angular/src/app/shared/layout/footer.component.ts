import { Component, Injector, OnInit, ElementRef, ViewChild} from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { element } from '@angular/core/src/render3';
import { AppConsts } from '@shared/AppConsts';

@Component({
    templateUrl: './footer.component.html',
    selector: 'footer-bar',
    styleUrls: ['./footer.component.css']
})
export class FooterComponent extends AppComponentBase implements OnInit {

    releaseDate: string;
    public exibirRodape = false;
    public rodape;
    public applicationName = AppConsts.application.name;
    @ViewChild('rodape') content: ElementRef;
    constructor(
        injector: Injector
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.releaseDate = this.appSession.application.releaseDate.format('YYYYMMDD');
    }
    
    public scroll(element: any) {
        if (this.exibirRodape == false){
            this.exibirRodape = true;
            //setTimeout(() => {
             //   element.scrollIntoView({ behavior: 'smooth' });    
            //}, 500);
        }
        else
        {
            this.exibirRodape = false;
        }
      }

}
