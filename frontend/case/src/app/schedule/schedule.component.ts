import { Schedule } from './schedule.entity';
import { DateTime, Duration } from 'luxon';
import { WeeklyHelper } from './../weekly/weekly.helper';
import { Component, OnInit } from '@angular/core';
import { Lesson } from '../lesson/lesson.entity';

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.css']
})
export class ScheduleComponent implements OnInit {

  public schedule: Schedule;

  constructor(
  ) {
    const helper = new WeeklyHelper();
    this.schedule = new Schedule();
    this.schedule.start = helper.start().minus({days: 14});
    this.schedule.end = this.schedule.start.plus({days: 28});

    const duration = Duration.fromISO('PT45M');
    this.schedule.hours = [
      { start: { hour:  7, minute: 30 }, duration },
      { start: { hour:  7, minute: 55 }, duration },
      { start: { hour:  8, minute: 45 }, duration },
      { start: { hour:  9, minute: 35 }, duration },
      { start: { hour: 10, minute: 40 }, duration },
      { start: { hour: 11, minute: 30 }, duration },
      { start: { hour: 12, minute: 20 }, duration },
      { start: { hour: 13, minute: 25 }, duration },
    ]
  }

  ngOnInit(): void {
  }

}
