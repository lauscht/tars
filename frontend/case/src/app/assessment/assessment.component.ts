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
    this.grades = this.assessmentService.getGrades(this.assessment);
  }

  public grades: Grade[];

  constructor(
    private assessmentService: AssessmentService,
  ) { }

  ngOnInit(): void {
  }

}
