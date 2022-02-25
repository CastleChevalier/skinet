import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.scss']
})
export class PagerComponent implements OnInit {
@Input() totalCount: number; //input is something we receive from a parent component
@Input() pageSize: number;
@Output() pageChanged = new EventEmitter<number>(); //way a child component can output to a parent
  constructor() { }

  ngOnInit(): void {
  }

  onPagerChange(event: any) {
    this.pageChanged.emit(event.page);
  }
}
