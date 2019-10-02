import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-item-detail',
  templateUrl: './item-detail.component.html',
  styleUrls: ['./item-detail.component.scss']
})
export class ItemDetailComponent implements OnInit {
  @Input()
  fields: Rainbow.ViewModels.FieldDisplayVM[];
  @Input()
  item: any;


  constructor(route: ActivatedRoute) { }

  ngOnInit() {
  }

}
