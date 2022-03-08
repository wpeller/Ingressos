import { AppMenuItem } from './app-menu-item';
import { NavigationDto } from '@shared/service-proxies/service-proxies';

export class AppMenu {
    name = '';
    displayName = '';
    items: NavigationDto[];

    constructor(name: string, displayName: string, items: NavigationDto[]) {
        this.name = name;
        this.displayName = displayName;
        this.items = items;
    }
}
