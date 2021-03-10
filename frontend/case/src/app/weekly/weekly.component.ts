import { DateTime } from 'luxon';
import { WeeklyHelper } from './weekly.helper';
import { LessonService } from './../lesson/lesson.service';
import { Component, OnInit } from '@angular/core';
import { Lesson } from '../lesson/lesson.entity';
import { FlyInOutAnimation } from '../fly-in.animation';
import { ColorService } from '../shared/color.service';
import { Course } from '../course/course.entity';
import { DoCheck } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'weekly',
  templateUrl: './weekly.component.html',
  styleUrls: ['./weekly.component.css'],
  animations: [FlyInOutAnimation]
})
export class WeeklyComponent implements OnInit, DoCheck {

  public days: DateTime[];
  public selected: Lesson;
  public previous: Lesson[];
  public future: Lesson[];
  public selectedContent: string;
  public selectedHomework: string;
  public editContent: boolean;
  public editHomework: boolean;
  public day;
  public end;


  editLessonState = 'out';
  constructor(
    private lessonService: LessonService,
    private colorService: ColorService,
  ) {
    this.day = DateTime.local();
  }

  getLessonsOfTheDay(day) {
    const weekday = day.weekday;
    return this.lessonService.getLessons().filter((l) =>
      l.start.weekday === weekday);
  }

  getColorOfTheDay(day) {
    const weekday = day.weekday;
    return this.colorService.WeekColorMap.get(weekday);
  }

  ngOnInit(): void {
    this.updateDays();
  }

  ngDoCheck(): void {

    if (!this.selected && this.days && this.days.length > 0) {
      const lessons = this.getLessonsOfTheDay(this.days[0]);

      if (lessons.length > 0) {
        this.show(lessons[0]);
      }

    }

  }

  updateDays() {
    const weekly = new WeeklyHelper();
    const start = weekly.startOfWeek(this.day);
    this.end = start.plus({ days: weekly.days });
    const days = [];
    for (var c = 0; c < weekly.days; c++) {
      days.push(start.plus({ days: c }));
    }
    this.days = days;
  }

  add() {
    this.editLessonState = this.editLessonState === 'in' ? 'out' : 'in';
  }
  show(lesson: Lesson) {

    if (lesson != this.selected) {
      this.editContent = (lesson.content === null);
      this.editHomework = (lesson.homework == null);
      this.selectedContent = lesson.content;
      this.selectedHomework = lesson.homework;
    }

    this.selected = lesson;
    this.future = this.lessonService.getFuture(lesson);
    this.previous = this.lessonService.getPrevious(lesson);
    this.editLessonState = 'in';

  }

  save() {
    this.selected.content = this.selectedContent;
    this.selected.homework = this.selectedHomework;
    //Looks like MatSnackBar is incompatible! Todo check why!
    //this.snackBar.open('Changes saved.');
  }

  cancel() {
    //this.snackBar.open('Changes canceled.');
    this.selectedContent = this.selected.content;
    this.selectedHomework = this.selected.homework;
  }

}
