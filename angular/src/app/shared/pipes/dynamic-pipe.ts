import { Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from '@angular/common';
import * as moment from 'moment';

@Pipe({
    name: 'dynamicPipe',
})
export class DynamicPipe implements PipeTransform {
    constructor(private datePipe: DatePipe) { }

    transform(value: any): any {
        let teste = new Date(value);
        if (moment(value).isValid()) {
            return this.datePipe.transform(value, 'dd/MM/yyyy');
        }

        return value;
    }
}
