import { Duration, DateTime } from 'luxon';
import { Course } from '../course/course.entity';


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
    public content: string,
    public homework?: string
  ) {
  }

  get hasContent() {
    return this.content;
  }
  get end(){
    return this.start.plus(this.duration);
  }
}
