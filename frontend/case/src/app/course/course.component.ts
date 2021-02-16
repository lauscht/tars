import { Component, OnInit } from '@angular/core';
import { Lesson } from '../lesson/lesson.entity';
import { LessonService } from '../lesson/lesson.service';
import { Pupil } from '../pupil/pupil.entity';
import { PupilService } from '../pupil/pupil.service';
import { Course } from './course.entity';
import { CourseService } from './course.service';

@Component({
  selector: 'course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css']
})
export class CourseComponent implements OnInit {

  public courses: Course[];
  private _course: Course;
  public lessons: Lesson[];
  public pupils: Pupil[];

  constructor(
    private courseService: CourseService,
    private lessonService: LessonService,
    private pupilService: PupilService,
  ) {
    this.courses = this.courseService.getCourses();
    this.course = this.courses[0];
  }

  get course() {
    return this._course;
  }

  set course(v: Course) {
    this._course = v;
    this.lessons = this.lessonService.getLessonByCourse(v);
    this.pupils = this.pupilService.getByCourse(v);
  }

  ngOnInit(): void {
  }

}
