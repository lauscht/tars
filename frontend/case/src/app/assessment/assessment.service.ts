import { Injectable } from '@angular/core';
import { Assessment } from './assessment.entity';

import { course9a, course7ab } from '../course/course.entities.mock';

@Injectable({
  providedIn: 'root'
})
export class ExamService {

  private _assessments: Assessment[];
  constructor() {
    this._assessments = [
      new Assessment(course9a, "Klassenarbeit", "Exam")
    ]
   }
}
