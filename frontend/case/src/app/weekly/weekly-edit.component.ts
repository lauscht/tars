import { Input, OnChanges, SimpleChanges } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Lesson } from '../lesson/lesson.entity';
import { LessonService } from '../lesson/lesson.service';
import { ColorInfo } from '../shared/color.info';
import { ColorService } from '../shared/color.service';

@Component({
  selector: 'weekly-edit',
  templateUrl: './weekly-edit.component.html',
  styleUrls: ['./weekly-edit.component.css']
})
export class WeeklyEditComponent implements OnInit, OnChanges {

  @Input()
  public selected: Lesson;

  public previous: Lesson[];
  public future: Lesson[];
  public selectedContent: string;
  public selectedHomework: string;
  public editContent: boolean;
  public editHomework: boolean;
  public color: ColorInfo;

  constructor(
    private lessonService: LessonService,
    private colorService: ColorService,    
    private snackBar: MatSnackBar
  ) { }



  ngOnInit(): void {
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['selected']) {
      this.show(changes['selected'].currentValue);
    }
  }

  show(lesson: Lesson) {

    this.editContent = (lesson.content === null);
    this.editHomework = (lesson.homework == null);
    this.selectedContent = lesson.content;
    this.selectedHomework = lesson.homework;

    this.selected = lesson;
    this.future = this.lessonService.getFuture(lesson);
    this.previous = this.lessonService.getPrevious(lesson);
    this.color = this.colorService.getColor(this.selected.start);

  }

  save() {
    this.selected.content = this.selectedContent;
    this.selected.homework = this.selectedHomework;
    this.lessonService.save(this.selected);
    this.snackBar.open('Changes saved.');
    this.editContent = false;
    this.editHomework= false;
  }

  cancel() {    
    this.selectedContent = this.selected.content;
    this.selectedHomework = this.selected.homework;

    this.snackBar.open('Changes are canceled.');
  }

}
