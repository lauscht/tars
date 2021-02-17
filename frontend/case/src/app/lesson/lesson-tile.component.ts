import { Component, Input, OnInit } from '@angular/core';
import { ColorInfo } from '../shared/color.info';
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

  color: ColorInfo;
  boarderCss: string;

  constructor( private colorService: ColorService) { }

  ngOnInit(): void {

    this.color = this.colorService.getColor(this.lesson.start);
    this.boarderCss = this.lesson.hasContent ? 'border-solid':  'border-dashed';

  }

}
