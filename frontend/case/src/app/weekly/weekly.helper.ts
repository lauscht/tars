import { Injectable } from '@angular/core';
import { DateTime, Duration } from 'luxon';

@Injectable({
  providedIn: 'root'
})
export class WeeklyHelper {

  constructor(public days=5)
  {
  }

  start(): DateTime {
    return this.startOfWeek(DateTime.local());
  }
  end(): DateTime {
    return this.endOfWeek(DateTime.local());
  }

  startOfWeek(day: DateTime) {
    return day.startOf('week');
  }
  endOfWeek(day: DateTime) {
    return day.set({weekday: this.days});
  }
}
