import { Injectable } from '@angular/core';

@Injectable()
export class DateValidation {
    /**
     * Validador de data.
     * @param {string} email
     * @returns {boolean}
     */
    //https://stackoverflow.com/questions/46589865/date-and-currency-validation-in-angular-4
    dateValidatorInPtBr(value: string): any {
        if (value !== undefined && value !== '' && value != null) {
            if (value.length < 10 && value.length > 10) {
                return false;
            }
           let day = null;
           let month = null;
           let year = null;
            if (value.indexOf('/') > -1) {
                let res = value.split('/');
                if (res.length > 1) {
                    day = res[0];
                    month = res[1];
                    year = res[2];
                }
            }
            if (isNaN(day) || isNaN(month) || isNaN(year)) { //invalid input data
                return false;
            }
            day = Number(day);
            month = Number(month);
            year = Number(year);
            if (month < 1 || month > 12) { //months from 1 to 12 only
                return false;
            }
            if (day < 1 || day > 31) { //days from 1 to 21 only
                return false;
            }
            if ((month === 4 || month === 6 || month === 9 || month === 11) && day === 31) { //months 4, 6, 9, 11 has only 30 days
                return false;
            }
            if (month === 2) { // check for february 29th
                let isleap = (year % 4 === 0 && (year % 100 !== 0 || year % 400 === 0));
                if (day > 29 || (day === 29 && !isleap)) {
                    return false;
                }
            }
        }
        return true;
    }
}
