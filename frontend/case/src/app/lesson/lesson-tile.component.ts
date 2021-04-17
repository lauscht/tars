import { ViewportScroller } from '@angular/common';
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

  @Input()
  isSelected: boolean;

  @Output()
  colorInfo: ColorInfo;

  borderCss: string;  

  constructor(
    private viewportScroller: ViewportScroller,
    private colorService: ColorService) { }

  ngDoCheck(): void {
   this.updateBorder();
  }

  ngOnInit(): void {

    this.colorInfo = this.colorService.getColor(this.lesson.start);
    this.updateBorder();

  }

  updateBorder() {
    this.borderCss = this.lesson.hasContent ? 'border-solid' : 'border-dashed';
  }

  scrollToEdit(){
    this.viewportScroller.scrollToAnchor('#edit');
  }

}
