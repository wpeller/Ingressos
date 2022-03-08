import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { Router, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs/operators';

@Component({
    selector: 'app-root',
    template:  `<router-outlet></router-outlet>`
})
export class RootComponent implements OnInit, OnDestroy {
    subscription: Subscription;
    constructor(private router: Router) {
    }
    ngOnInit(): void {
        this.subscription = this.router.events.pipe(
            filter(event => event instanceof NavigationEnd)
        ).subscribe(() => window.scrollTo(0, 0));
    }

    ngOnDestroy(): void {
        this.subscription.unsubscribe();
    }
}
