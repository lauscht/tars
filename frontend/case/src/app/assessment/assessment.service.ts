import { PupilService } from './../pupil/pupil.service';
import { DateTime } from 'luxon';
import { Injectable } from '@angular/core';
import { Assessment, Grade } from './assessment.entity';

import { course9a, course7ab } from '../course/course.entities.mock';
import { Course } from '../course/course.entity';
import { Pupil } from '../pupil/pupil.entity';

function DMY(date: string):DateTime {
  return DateTime.fromFormat(date, "dd.MM.yyyy");
}



@Injectable({
  providedIn: 'root'
})
export class AssessmentService {

  private _assessments: Assessment[];
  private _grades: Grade[];

  constructor(
    private pupilService: PupilService
  ) {
    this._assessments = [
      new Assessment(0, DMY("15.11.2020"), course9a.id, "Shakespeare", "Exam", 2, 3.3),
      new Assessment(1, DMY("10.12.2020"), course9a.id, "Voc p80", "Test", 1, 4.0),
      new Assessment(2, DMY("20.12.2020"), course9a.id, "-", "Oral", 2, 2.0),
      new Assessment(3, DMY("22.12.2020"), course9a.id, "Summary", "Haljahresnote", 0, 2.92),

      new Assessment(4, DMY("10.01.2021"), course9a.id, "Voc p120", "Test", 1, 4.0),
      new Assessment(5, DMY("20.02.2021"), course9a.id, "-", "Oral", 2, 2.0),
      new Assessment(6, DMY("04.03.2021"), course9a.id, "Summary", "Jahresnote", 0, 2.83),
    ]

    const grades = [];
    this._assessments.forEach(ass => {
      const pupils = pupilService.getByCourseId(ass.courseId).forEach((p) => {
        grades.push(new Grade(ass.id, p.id, p.id, p))
      })
    });
    this._grades = grades;
   }

   getByCourse(course: Course) {
    return this._assessments.filter((p) => p.courseId == course.id);
  }

  getGrades(assessment: Assessment) {
    return this._grades.filter((g) => g.assessmentId == assessment.id);
  }

  getGradesByPupil(pupil: Pupil) {
    return this._grades.filter((g) => g.pupilId == pupil.id);
  }

}
