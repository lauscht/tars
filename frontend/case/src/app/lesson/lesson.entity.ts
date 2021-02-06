import { Duration, DateTime } from 'luxon';

export class Course {
  constructor(
    public name: string,
    public subject: string
  ) {

  }
}

export class ScheduledLesson {
  constructor(
    public course: Course,
    public room: string,
    public start: DateTime,
    public duration: Duration,
    public byWeekly = false,
  ) {

  }

  get end(){
    return this.start.plus(this.duration);
  }
}

export class Lesson {
  constructor(
    public course: Course,
    public room: string,
    public start: DateTime,
    public duration: Duration,
    public content?: string,
    public homework?: string
  ) {
  }

  get hasContent() {
    return this.content !== null;
  }
  get end(){
    return this.start.plus(this.duration);
  }
}
