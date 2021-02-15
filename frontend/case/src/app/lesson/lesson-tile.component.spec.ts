import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessonTileComponent } from './lesson-tile.component';

describe('LessonTileComponent', () => {
  let component: LessonTileComponent;
  let fixture: ComponentFixture<LessonTileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LessonTileComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LessonTileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
