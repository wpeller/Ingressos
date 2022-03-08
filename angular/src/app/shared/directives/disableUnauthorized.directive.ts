import { AuthService } from '../services/auth.service';
import {
    ElementRef,
    OnInit
    } from '@angular/core';

export class DisableUnauthorizedDirective implements OnInit {

    constructor(private el: ElementRef, private authService: AuthService) { }

    ngOnInit() {
        this.el.nativeElement.disabled = false;
        if (!this.authService.isRouteAllowed()) {
            this.el.nativeElement.disabled = true;
        }
    }
}
