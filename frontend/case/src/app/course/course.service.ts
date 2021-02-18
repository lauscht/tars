import { Injectable } from '@angular/core';
import { Course } from './course.entity';

@Injectable({
  providedIn: 'root'
})
export class CourseService {

  courses: Course[];

  constructor() { 
    this.courses = [
      new Course(0, "9a", "es"),
      new Course(1, "7b", "bk")
    ];
  }
  
  getCourses(){
    return this.courses;
  }
}
