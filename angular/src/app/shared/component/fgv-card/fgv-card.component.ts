import { Component, OnInit, Input } from '@angular/core';

@Component({
    selector: 'fgv-card',
    templateUrl: './fgv-card.component.html',
    styleUrls: ['./fgv-card.component.css'],
})
export class FgvCardComponent {

    @Input() title: string;

}
