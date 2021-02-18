import { DateTime } from 'luxon';
import { WeeklyHelper } from './weekly.helper';
import { LessonService } from './../lesson/lesson.service';
import { Component, OnInit } from '@angular/core';
import { Lesson } from '../lesson/lesson.entity';
import { FlyInOutAnimation } from '../fly-in.animation';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'weekly',
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
  public dayForm;

  editLessonState = 'out';
  constructor(
    private lessonService: LessonService,
  ) {
    this.day = DateTime.local();
   }

   getLessonsOfTheDay(day){
     const weekday = day.weekday;
     return this.lessonService.getLessons().filter((l) =>
     l.start.weekday === weekday);
   }

  ngOnInit(): void {
    this.updateDays();
  }

  updateDays() {
    const weekly = new WeeklyHelper();
    const start = weekly.startOfWeek(this.day);
    this.end = start.plus({days:weekly.days});
    const days = [];
    for(var c = 0; c < weekly.days; c++){
      days.push(start.plus({days: c}));
    }
    this.days = days;
  }

  add() {
    this.editLessonState = this.editLessonState === 'in' ? 'out' : 'in';
  }
  show(lesson: Lesson) {
    this.lesson = lesson;
    this.editLessonState = 'in';
  }

}
