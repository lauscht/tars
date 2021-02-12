import { DateTime } from 'luxon';
import { WeeklyHelper } from './weekly.helper';
import { LessonService } from './../lesson/lesson.service';
import { Component, OnInit } from '@angular/core';
import { _ } from "lodash-es";
import { Lesson } from '../lesson/lesson.entity';
import { FlyInOutAnimation } from '../fly-in.animation';

@Component({
  selector: 'app-weekly',
  templateUrl: './weekly.component.html',
  styleUrls: ['./weekly.component.css'],
  animations: [FlyInOutAnimation]
})
export class WeeklyComponent implements OnInit {

  public days: DateTime[];
  public lessons;
  public lesson: Lesson;
  public day;
  public end;

  editLessonState = 'out';
  constructor(
    private lessonService: LessonService,
  ) {
    const weekly = new WeeklyHelper();
    const start = weekly.start();
    this.day = start;
    this.end = start.plus({days:weekly.days});
    this.days = [];
    for(var c = 0; c < weekly.days; c++){
      this.days.push(start.plus({days: c}));
    }
   }

   getLessonsOfTheDay(day){
     const weekday = day.weekday;
     return this.lessonService.getLessons().filter((l) =>
     l.start.weekday === weekday);
   }

  ngOnInit(): void {

  }

  goToPreviousWeek() {
    this.day = this.day.minus({days: 7});
  }
  goToNextWeek() {
    this.day = this.day.plus({days: 7});
  }

  add() {
    this.editLessonState = this.editLessonState === 'in' ? 'out' : 'in';
  }
  show(lesson: Lesson) {
    this.lesson = lesson;
    this.editLessonState = 'in';
  }

}
