import { ViewportScroller } from '@angular/common';
import { EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
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

  private _selectedLesson: Lesson;

  get selected() {
    return this._selectedLesson;
  }

  @Input()
  set selected(value: Lesson) {

    if (value == this.selected)
      return;

    if (this.canBeSaved()) {
      const currentState = this._selectedLesson;
      const currentContent = this.selectedContent;
      const currentHomework = this.selectedHomework;
      const refSnackbar = this.snackBar.open("Do you want to save your changes?", "save", { duration: 0 });

      refSnackbar.onAction().subscribe(x => {
        this.saveState(currentContent, currentHomework, currentState);
        this._selectedLesson = value;
      });
    }
    this._selectedLesson = value;
  }

  @Output() 
  removed = new EventEmitter<Lesson>();

  public previous: Lesson[];
  public future: Lesson[];
  public selectedContent: string;
  public selectedHomework: string;
  public editContent: boolean;
  public editHomework: boolean;
  public color: ColorInfo;

  constructor(
    private viewportScroller: ViewportScroller,
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
    this.saveState(this.selectedContent, this.selectedHomework, this.selected);
    this.editContent = false;
    this.editHomework = false;
  }

  saveState(currentContent: string, currentHomework: string, target: Lesson) {
    target.content = currentContent;
    target.homework = currentHomework;
    this.lessonService.save(target);
    this.snackBar.open('Changes saved.');

  }

  cancel() {
    this.selectedContent = this.selected.content;
    this.selectedHomework = this.selected.homework;

    this.snackBar.open('Changes are canceled.');
  }

  canBeSaved(): boolean {
    return this.selectedContent != this.selected?.content || this.selectedHomework != this.selected?.homework;
  }

  remove(): void {

      const refSnackbar = this.snackBar.open("Are you sure to remove this item?", "remove", { duration: 0 });
      refSnackbar.onAction().subscribe(()=> {
        this.lessonService.remove(this._selectedLesson);
        this.removed.emit(this._selectedLesson);
      } );
  }

  scrollToSelected(){
    var target = `#${this.selected.course.name}_${this.selected.start.toMillis()}`;
    this.viewportScroller.scrollToAnchor(target);    
  }

}
