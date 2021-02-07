import { DateTime } from 'luxon';
import { Component, Input, OnInit } from '@angular/core';
import { Lesson } from './lesson.entity';

@Component({
  selector: 'app-lesson',
  templateUrl: './lesson.component.html',
  styleUrls: ['./lesson.component.css'],
})
export class LessonComponent implements OnInit {

  @Input() lesson: Lesson;

  constructor() { }

  ngOnInit(): void {
  }
}
