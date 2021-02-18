import { Component, DoCheck, Input, OnInit, Output } from '@angular/core';
import { ColorInfo } from '../shared/color.info';
import { ColorService } from '../shared/color.service';
import { Lesson } from './lesson.entity';

@Component({
  selector: 'app-lesson-tile',
  templateUrl: './lesson-tile.component.html',
  styleUrls: ['./lesson-tile.component.css']
})
export class LessonTileComponent implements OnInit, DoCheck  {

  @Input()
  lesson: Lesson;

  @Output()
  colorInfo: ColorInfo;

  boarderCss: string;

  constructor(private colorService: ColorService) { }

  ngDoCheck(): void {
   this.updateBoarder();
  }

  ngOnInit(): void {

    this.colorInfo = this.colorService.getColor(this.lesson.start);
    this.updateBoarder();

  }

  updateBoarder() {
    this.boarderCss = this.lesson.hasContent ? 'border-solid' : 'border-dashed';
  }

}
