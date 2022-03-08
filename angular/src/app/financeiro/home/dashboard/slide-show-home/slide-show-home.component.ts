import { Component, OnInit, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { SlideShowServiceService } from '../slide-show-service.service';

@Component({
  selector: 'app-slide-show-home',
  templateUrl: './slide-show-home.component.html',
  styleUrls: ['./slide-show-home.component.css']
})
export class SlideShowHomeComponent extends AppComponentBase implements OnInit {
 
  constructor(
    injector: Injector,
    private _galeriaService: SlideShowServiceService
  ) {
    super(injector);
  }

  galerias: any[];

  ngOnInit() {
    this.galerias = this._galeriaService.getImagens();
  }
  

}
