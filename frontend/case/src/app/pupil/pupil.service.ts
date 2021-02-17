import { Injectable } from '@angular/core';
import { Course } from '../course/course.entity';
import { Pupil } from './pupil.entity';

@Injectable({
  providedIn: 'root'
})
export class PupilService {
  pupils: Pupil[];
  constructor() {
    this.pupils = [
      new Pupil("Tobe", 0),
      new Pupil("Flow", 0),
      new Pupil("Luise", 1),
      new Pupil("Calli", 1),
      new Pupil("Anke", 2),
      new Pupil("Bernadette", 2),
    ]
   }

   getByCourse(course: Course) {
     return this.pupils.filter((p) => p.courseId == course.id);
   }
}
