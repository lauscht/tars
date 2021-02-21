import { Injectable } from '@angular/core';
import { Course } from './course.entity';
import * as csm from './course.entities.mock';

@Injectable({
  providedIn: 'root'
})
export class CourseService {

  courses: Course[];

  constructor() {
    this.courses = csm.courses
  }

  getCourses(){
    return this.courses;
  }
}
