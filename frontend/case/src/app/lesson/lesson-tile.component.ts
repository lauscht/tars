import { Component, Input, OnInit } from '@angular/core';
import { ColorService } from '../shared/color.service';
import { Lesson } from './lesson.entity';

@Component({
  selector: 'app-lesson-tile',
  templateUrl: './lesson-tile.component.html',
  styleUrls: ['./lesson-tile.component.css']
})
export class LessonTileComponent implements OnInit {

  @Input()
  lesson: Lesson;

  colorCss: string;

  constructor( private colorService: ColorService) { }

  ngOnInit(): void {

    this.colorCss = this.colorService.getColor(this.lesson.start);

  }

}
