import { Component, Input, OnInit } from '@angular/core';
import { Assessment } from 'src/app/assessment/assessment.entity';
import { AssessmentService } from 'src/app/assessment/assessment.service';
import { Pupil } from 'src/app/pupil/pupil.entity';
import { PupilService } from 'src/app/pupil/pupil.service';
import { Course } from '../course.entity';

@Component({
  selector: 'grades',
  templateUrl: './grades.component.html',
  styleUrls: ['./grades.component.css']
})
export class GradesComponent implements OnInit {

  public pupils: Pupil[];
  public assessments: Assessment[];
  public data;

  _course: Course;
  get course():Course {
    return this._course;
  }
  @Input()
  set course(course: Course) {
    this._course = course;
    this.pupils = this.pupilService.getByCourse(course);
    this.assessments = this.assessmentService.getByCourse(course);

    this.data = this.pupils.map((p) => {
      const grades = this.assessmentService.getGradesByPupil(p);
      const marks = this.assessments.map((a) => grades.find(g => g.assessmentId == a.id)?.mark);
      return {name: p.name, marks };
    });
  }

  constructor(
    private assessmentService: AssessmentService,
    private pupilService: PupilService
  ) { }

  ngOnInit(): void {
  }

}
