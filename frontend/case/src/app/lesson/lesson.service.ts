import { WeeklyHelper } from '../weekly/weekly.helper';
import { Injectable } from '@angular/core';
import { Lesson } from './lesson.entity';
import { DateTime, Duration } from 'luxon';
import { Course } from '../course/course.entity';
import * as cs from '../course/course.entities.mock';

@Injectable({
  providedIn: 'root'
})
export class LessonService {

  lessons: Lesson[];

  constructor(
  ) {
    const weekly = new WeeklyHelper();
    const roomK35 = "K35";
    const room412 = "412";
    const duration = Duration.fromISO('PT45M');
    const duration2 = Duration.fromISO('PT90M');

    const hours = [
      { hour:  7, minute: 30},
      { hour:  7, minute: 55 },
      { hour:  8, minute: 45 },
      { hour:  9, minute: 35 },
      { hour: 10, minute: 40 },
      { hour: 11, minute: 30 },
      { hour: 12, minute: 20 },
      { hour: 13, minute: 25 },
    ]

    const monday = weekly.start();
    const tuesday = monday.plus({days: 1});
    const wednesday = monday.plus({days: 2});
    const thursday = monday.plus({days: 3});
    const friday = monday.plus({days: 4});

    this.lessons = [
      new Lesson(cs.course9a, roomK35,
        monday.set(hours[1]), duration2, "Some detailed monday content"
      ),
      new Lesson(cs.course7ab, room412,
        monday.set(hours[3]), duration, `Some detailed monday content
-  fun game
- mini presentations 1, 2 + 3
- Voc intro
- cachipún
- HV Global`,
        `p.37/5+3+9
Voc wdh
Lernen für KA - Fragen notieren`
      ),
      new Lesson(cs.course8a, roomK35, monday.set(hours[4]), duration,
        "Some detailed monday content"),
      new Lesson(cs.courseKs1, roomK35, monday.set(hours[5]), duration2, null),

      new Lesson(cs.course9d, room412, tuesday.set(hours[3]), duration, null),
      new Lesson(cs.course8ab, room412, tuesday.set(hours[4]), duration2, null),
      new Lesson(cs.courseKs2, room412, tuesday.set(hours[6]), duration2, null),

      new Lesson(cs.courseKs1, room412, wednesday.set(hours[3]), duration, null),
      new Lesson(cs.course10d, room412, wednesday.set(hours[4]), duration2, null),
      new Lesson(cs.course8a, room412, wednesday.set(hours[6]), duration2, null),

      new Lesson(cs.course7ab, room412, thursday.set(hours[3]), duration, null),
      new Lesson(cs.course9d, room412, thursday.set(hours[4]), duration2, null),

      new Lesson(cs.course7ab, room412, friday.set(hours[1]), duration2, null),
      new Lesson(cs.course8ab, room412, friday.set(hours[4]), duration, null),
      new Lesson(cs.course10d, room412, friday.set(hours[5]), duration, null),
      new Lesson(cs.courseKs2, room412, friday.set(hours[6]), duration, null),

    ];
  }

  getLessons(){
    return this.lessons;
  }

  getLessonByCourse(course: Course) {
      return this.lessons.filter((l) => l.course.id == course.id);
  }

  getPrevious(l: Lesson): Lesson[] {
    return this.lessons.filter((v) => v.course === l.course && v.start < l.start);
  }

  getFuture(l: Lesson): Lesson[] {
    return this.lessons.filter((v) => v.course === l.course && v.start > l.start);
  }
}
