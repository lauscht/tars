import { DateTime, Interval } from 'luxon';

export class Schedule{

  description: string;

  start: DateTime;
  end: DateTime;

  days: number;

  hours: any[];
}
