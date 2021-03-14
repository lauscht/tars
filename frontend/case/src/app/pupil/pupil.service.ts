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
      new Pupil(0, "Tobe", 0),
      new Pupil(1, "Flow", 0),
      new Pupil(2, "Luise", 1),
      new Pupil(3, "Calli", 1),
      new Pupil(4, "Anke", 2),
      new Pupil(5, "Bernadette", 2),
    ]
   }

   getByCourse(course: Course) {
     return this.pupils.filter((p) => p.courseId == course.id);
   }
   getByCourseId(id: number) {
    return this.pupils.filter((p) => p.courseId == id);
   }
}
