import { WeeklyHelper } from '../weekly/weekly.helper';
import { Injectable } from '@angular/core';
import { Course, Lesson } from './lesson.entity';
import { DateTime, Duration } from 'luxon';


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
    const courseA = new Course("9a", "es");
    const courseB = new Course("7b", "bk");

    const monday = weekly.start();
    const wednesday = monday.plus({days: 2});
    const friday = monday.plus({days: 4});

    this.lessons = [
      new Lesson(courseA, roomK35,
        monday.set({ hour: 9, minute: 30 }), duration, "Some detailed monday content"
      ),
      new Lesson(courseB, room412,
        monday, duration, "Some other monday content"
      ),
      new Lesson(courseA, roomK35,
      wednesday, duration, null
      ),
      new Lesson(courseB, room412,
        friday, duration, "Some friday content"
      )
    ];
  }

  getLessons(){
    return this.lessons;
  }
}
