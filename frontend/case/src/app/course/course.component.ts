import { Assessment } from './../assessment/assessment.entity';
import { AssessmentService } from './../assessment/assessment.service';
import { Component, OnInit } from '@angular/core';
import { Lesson } from '../lesson/lesson.entity';
import { LessonService } from '../lesson/lesson.service';
import { Pupil } from '../pupil/pupil.entity';
import { PupilService } from '../pupil/pupil.service';
import { Course } from './course.entity';
import { CourseService } from './course.service';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css']
})
export class CourseComponent implements OnInit {

  public courses: Course[];
  private _course: Course;
  public lessons: Lesson[];
  public assessments: Assessment[];
  public pupils: Pupil[];
  public filters: string[];
  public content: [];

  constructor(
    private courseService: CourseService,
    private lessonService: LessonService,
    private pupilService: PupilService,
    private assessmentService: AssessmentService,
    private snackBar: MatSnackBar,
  ) {
    this.courses = this.courseService.getCourses();
    this.course = this.courses[0];
    this.filters = ["lessons", "assessments"]
    this.content = [];
  }

  get course() {
    return this._course;
  }

  set course(value: Course) {
    this._course = value;
    this.lessons = this.lessonService.getLessonByCourse(value);
    this.pupils = this.pupilService.getByCourse(value);
    this.assessments = this.assessmentService.getByCourse(value);
  }

  ngOnInit(): void {
  }

  add():void {

  }
}
