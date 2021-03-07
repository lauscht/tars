import { AfterContentInit, AfterViewInit, DoCheck, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'markdownbox',
  templateUrl: './markdownbox.component.html',
  styleUrls: ['./markdownbox.component.css']
})
export class MarkdownboxComponent implements OnInit, OnChanges {

  constructor() { }



  @Input()
  text: string;

  @Output()
  textChange = new EventEmitter<string>();

  @Input()
  rows: number;

  @Input()
  isEditMode: boolean;

  @Output()
  isEditModeChange = new EventEmitter<boolean>();

  ngOnInit(): void {
    this.text = this.text ? this.text : null;
    // this.isEditMode = (!this.text || this.text == "" || this.text == null);
    // this.isEditModeChange.emit(this.isEditMode);
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.textChange.emit(changes['text'].currentValue);

    if (changes['text'].currentValue == null && !this.isEditMode) {
      this.isEditMode = true;
      this.isEditModeChange.emit(this.isEditMode);
    }    
  }

  update() {
    this.textChange.emit(this.text);
  }

  log(value: string) {
    console.log(value);
  }

}
