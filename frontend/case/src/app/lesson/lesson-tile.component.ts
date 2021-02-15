import { Component, Input, OnInit } from '@angular/core';
import { Lesson } from './lesson.entity';

@Component({
  selector: 'app-lesson-tile',
  templateUrl: './lesson-tile.component.html',
  styleUrls: ['./lesson-tile.component.css']
})
export class LessonTileComponent implements OnInit {

  @Input()
  lesson: Lesson;

  constructor() { }

  ngOnInit(): void {
  }

}
