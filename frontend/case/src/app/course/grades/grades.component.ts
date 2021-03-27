import { Pupil } from './../../pupil/pupil.entity';
import { Component, Input, OnInit } from '@angular/core';
import { Assessment } from 'src/app/assessment/assessment.entity';
import { AssessmentService } from 'src/app/assessment/assessment.service';
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
    this.update();
  }

  _focusPupil: Pupil;

  @Input()
  set focusPupil(pupil: Pupil) {
    this._focusPupil = pupil;
    this.update();
  }

  update() {
    if (!this._course)
    {
      return;
    }

    let pupils = this.pupilService.getByCourse(this._course);
    if (this._focusPupil)
    {
      pupils = pupils.filter((p) => p.id == this._focusPupil?.id);
    }

    this.pupils = pupils;
    this.assessments = this.assessmentService.getByCourse(this._course);

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
