import { Component, Input, OnInit } from '@angular/core';
import { Lesson } from './lesson.entity';

@Component({
  selector: 'app-lesson-edit',
  templateUrl: './lesson-edit.component.html',
  styleUrls: ['./lesson-edit.component.css'],
})
export class LessonEditComponent implements OnInit {

  @Input() currentLesson: Lesson;

  constructor() { }

  ngOnInit(): void {
  }
}
