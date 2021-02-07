import { LessonService } from './lesson.service';
import { Component, Input, OnInit } from '@angular/core';
import { Lesson } from './lesson.entity';

@Component({
  selector: 'app-lesson',
  templateUrl: './lesson.component.html',
  styleUrls: ['./lesson.component.css']
})
export class LessonComponent implements OnInit {

  private currentLesson: Lesson;
  get lesson(): Lesson {
    return this.currentLesson;
  }
  @Input()
  set lesson(l: Lesson){
    this.currentLesson = l;
    this.previous = this.lessonService.getPrevious(this.lesson);
    this.future = this.lessonService.getFuture(this.lesson);
  }
  public previous: Lesson[];
  public future: Lesson[];

  constructor(
    private lessonService: LessonService
  ) { }

  ngOnInit(): void {
    console.log(this.lesson);
  }




}
