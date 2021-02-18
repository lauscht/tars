import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WeekInputComponent } from './week-input.component';

describe('WeekInputComponent', () => {
  let component: WeekInputComponent;
  let fixture: ComponentFixture<WeekInputComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WeekInputComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WeekInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
