import {
  trigger,
  state,
  style,
  animate,
  transition,
} from '@angular/animations';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'flyout',
  templateUrl: './flyout.component.html',
  styleUrls: ['./flyout.component.css'],
  animations: [
    trigger('openClose', [
      state('open', style({
        top: '70vh',
      })),
      state('closed', style({
        top: '100vh',
      })),
      transition('open => closed', [
        animate('0.3s')
      ]),
      transition('closed => open', [
        animate('0.3s')
      ]),
    ]),
  ],
})
export class FlyoutComponent implements OnInit {

  @Input()
  visible: boolean = false;

  constructor() { }

  ngOnInit(): void {
  }

}
