import { Pupil } from './../pupil/pupil.entity';
import { Assessment, Grade } from './assessment.entity';
import { Component, Input, OnInit } from '@angular/core';
import { AssessmentService } from './assessment.service';

@Component({
  selector: 'assessment',
  templateUrl: './assessment.component.html',
  styleUrls: ['./assessment.component.css']
})
export class AssessmentComponent implements OnInit {

  private _assessment: Assessment;
  get assessment(): Assessment {
    return this._assessment;
  }

  @Input()
  set assessment(a: Assessment){
    this._assessment = a;
    this.update();
  }

  _focusPupil: Pupil;

  @Input()
  set focusPupil(pupil: Pupil) {
    this._focusPupil = pupil;
    this.update();
  }

  update() {
    if (this._assessment === undefined) {
      return;
    }

    let grades = this.assessmentService.getGrades(this.assessment);

    if (this._focusPupil !== undefined)
    {
      grades = grades.filter((g) => g.pupilId == this._focusPupil.id);
    }

    this.grades = grades;
  }

  public grades: Grade[];

  constructor(
    private assessmentService: AssessmentService,
  ) { }

  ngOnInit(): void {
  }

}
