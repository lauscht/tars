import { DateTime } from 'luxon';
import { Injectable } from '@angular/core';
import { Assessment } from './assessment.entity';

import { course9a, course7ab } from '../course/course.entities.mock';
import { Course } from '../course/course.entity';

function DMY(date: string):DateTime {
  return DateTime.fromFormat(date, "dd.MM.yyyy");
}

@Injectable({
  providedIn: 'root'
})
export class AssessmentService {

  private _assessments: Assessment[];
  constructor() {
    this._assessments = [
      new Assessment(DMY("15.11.2020"), course9a.id, "Shakespeare", "Exam", 2, 3.3),
      new Assessment(DMY("10.12.2020"), course9a.id, "Voc p80", "Test", 1, 4.0),
      new Assessment(DMY("20.12.2020"), course9a.id, "-", "Oral", 2, 2.0),
      new Assessment(DMY("22.12.2020"), course9a.id, "Summary", "Haljahresnote", 0, 2.92),

      new Assessment(DMY("10.01.2021"), course9a.id, "Voc p120", "Test", 1, 4.0),
      new Assessment(DMY("20.02.2021"), course9a.id, "-", "Oral", 2, 2.0),
      new Assessment(DMY("04.03.2021"), course9a.id, "Summary", "Jahresnote", 0, 2.83),
    ]
   }

   getByCourse(course: Course) {
    return this._assessments.filter((p) => p.courseId == course.id);
  }
}
