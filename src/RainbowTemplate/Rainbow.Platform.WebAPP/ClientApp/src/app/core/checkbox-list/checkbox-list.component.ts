import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-checkbox-list',
  templateUrl: './checkbox-list.component.html',
  styleUrls: ['./checkbox-list.component.scss']
})
export class CheckboxListComponent implements OnInit {

  items: { name: string, checked: boolean }[] = [];

  @Input()
  public get values(): string[] {
    return this.items.map(a => a.name);
  }
  public set values(value: string[]) {

    this.items = value.map(a => ({ name: a, checked: false }));
  }

  @Input()
  public set selectedValues(value: string[]) {
    this.items.forEach(item => {
      if (value.length > 0) {
        item.checked = value.includes(item.name);
      } else {
        item.checked = false;
      }
    });
  }


  @Output()
  selectedValuesChange: EventEmitter<string[]> = new EventEmitter<string[]>();


  constructor() { }

  ngOnInit() {
  }
  onSelectChanged() {
    this.selectedValuesChange.next(this.items.filter(a => a.checked).map(a => a.name));
  }
}
