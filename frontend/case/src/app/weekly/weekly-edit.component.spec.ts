import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WeeklyEditComponent } from './weekly-edit.component';

describe('WeeklyEditComponent', () => {
  let component: WeeklyEditComponent;
  let fixture: ComponentFixture<WeeklyEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WeeklyEditComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WeeklyEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
