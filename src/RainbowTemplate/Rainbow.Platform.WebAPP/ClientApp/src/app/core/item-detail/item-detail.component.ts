import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-item-detail',
  templateUrl: './item-detail.component.html',
  styleUrls: ['./item-detail.component.scss']
})
export class ItemDetailComponent implements OnInit {
  fields: Rainbow.ViewModels.FieldDisplayVM[];
  item: any;


  constructor(route: ActivatedRoute) { }

  ngOnInit() {
  }

}
