import { Component, OnInit, Input } from '@angular/core';


@Component({
  selector: 'app-toggle-switch',
  templateUrl: './toggle-switch.component.html',
  styleUrls: ['./toggle-switch.component.scss']
})
export class ToggleSwitchComponent implements OnInit {

  @Input()
  onLabel: string = 'On';
  @Input()
  offLabel: string = 'Off';
  @Input()
  knobLabel: string = '\u00a0';

  @Input()
  disabled: boolean;
  @Input()
  value: boolean;


  constructor() { }

  ngOnInit() {
  }

  toggleSwitch() {
    if (!this.disabled) {
      this.value = !this.value;
    }
  }
}

