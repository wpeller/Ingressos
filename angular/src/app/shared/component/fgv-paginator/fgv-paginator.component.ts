import {
  Component,
  EventEmitter,
  Input,
  Output,
  OnInit,
  TemplateRef,
  ViewChild
  } from '@angular/core';
import { Paginator, SelectItem } from 'primeng/primeng';
import { PrimengTableHelper } from '@shared/helpers/PrimengTableHelper';

@Component({
  selector: 'fgv-paginator',
  templateUrl: './fgv-paginator.component.html',
  styleUrls: ['./fgv-paginator.component.css']
})
export class FgvPaginatorComponent {

  @ViewChild('paginator') paginator: Paginator;

  @Input() primengTableHelper: PrimengTableHelper;
  @Output() onPageChange: EventEmitter<any> = new EventEmitter<any>();

  pageLinkSize: number;
  style: any;
  styleClass: string;
  alwaysShow: boolean;
  templateLeft: TemplateRef<any>;
  templateRight: TemplateRef<any>;
  dropdownAppendTo: any;
  pageLinks: number[];
  _totalRecords: number;
  _first: number;
  _rows: number;
  _rowsPerPageOptions: number[];
  rowsPerPageItems: SelectItem[];
  paginatorState: any;
  totalRecords: number;
  first: number;
  rows: number;
  rowsPerPageOptions: number[];

  onPageChangeHandler(event) {
    this.onPageChange.emit(event);
  }

  changePage(p: number): void {
    this.paginator.changePage(p);
    return;
  }
}
