import { SelectItem } from 'primeng/components/common/api';

export class GenericItem implements SelectItem {
    constructor(public label, public value) { }
}
