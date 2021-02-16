import { LessonService } from './lesson.service';
import { Component, Input, OnInit } from '@angular/core';
import { Lesson } from './lesson.entity';

@Component({
  selector: 'lesson',
  templateUrl: './lesson.component.html',
  styleUrls: ['./lesson.component.css']
})
export class LessonComponent implements OnInit {

  private _lesson: Lesson;
  get lesson(): Lesson {
    return this._lesson;
  }
  @Input()
  set lesson(l: Lesson){
    this._lesson = l;
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
