import { WeeklyHelper } from '../weekly/weekly.helper';
import { Injectable } from '@angular/core';
import { Lesson } from './lesson.entity';
import { DateTime, Duration } from 'luxon';
import { Course } from '../course/course.entity';


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

    const course9a = new Course(0, "9a", "es");
    const course7ab = new Course(1, "7ab", "bk");
    const course8a = new Course(2, "8a", "E");
    const courseKs1 = new Course(3, "KS1", "e2");
    const course9d = new Course(4, "9d", "spap");
    const course8ab = new Course(5, "8ab", "spap");
    const courseKs2 = new Course(6, "KS2", "e1");
    const course10d = new Course(7, "10d", "spap");

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
      new Lesson(course9a, roomK35,
        monday.set(hours[1]), duration2, "Some detailed monday content"
      ),
      new Lesson(course7ab, room412,
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
      new Lesson(course8a, roomK35, monday.set(hours[4]), duration,
        "Some detailed monday content"),
      new Lesson(courseKs1, roomK35, monday.set(hours[5]), duration2, null),

      new Lesson(course9d, room412, tuesday.set(hours[3]), duration, null),
      new Lesson(course8ab, room412, tuesday.set(hours[4]), duration2, null),
      new Lesson(courseKs2, room412, tuesday.set(hours[6]), duration2, null),

      new Lesson(courseKs1, room412, wednesday.set(hours[3]), duration, null),
      new Lesson(course10d, room412, wednesday.set(hours[4]), duration2, null),
      new Lesson(course8a, room412, wednesday.set(hours[6]), duration2, null),

      new Lesson(course7ab, room412, thursday.set(hours[3]), duration, null),
      new Lesson(course9d, room412, thursday.set(hours[4]), duration2, null),

      new Lesson(course7ab, room412, friday.set(hours[1]), duration2, null),
      new Lesson(course8ab, room412, friday.set(hours[4]), duration, null),
      new Lesson(course10d, room412, friday.set(hours[5]), duration, null),
      new Lesson(courseKs2, room412, friday.set(hours[6]), duration, null),

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

  save(l:Lesson){
    console.log("Not implemented. So Feed me with code. ;-)");
  }
}
