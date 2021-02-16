import { DateTime } from 'luxon';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';


@Component({
  selector: 'app-week-input',
  templateUrl: './week-input.component.html',
  styleUrls: ['./week-input.component.css']
})
export class WeekInputComponent implements OnInit {

  public edit: boolean = false;
  @Input() date: DateTime;

  @Output() dateChanged = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }

  goToPreviousWeek() {
    this.date = this.date.minus({days: 7});
    this.dateChanged.emit();
  }
  goToNextWeek() {
    this.date = this.date.plus({days: 7});
    this.dateChanged.emit();
  }

}
