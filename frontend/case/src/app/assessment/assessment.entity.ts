import { Pupil } from './../pupil/pupil.entity';
import { DateTime, Interval } from 'luxon';
import { Course } from '../course/course.entity';
import { Lesson } from '../lesson/lesson.entity';

export class Grade{
  constructor(
    public assessmentId: number,
    public pupilId: number,
    public mark: number,
    public pupil: Pupil=null
  ){

  }
}

export class Assessment{
  constructor(
    public id: number,
    public date: DateTime,
    public courseId: number,
    public context: string,
    public category: string,
    public weigth: number=1,
    public average: number=null,
    public lesson: Lesson=null
  ) {

  }
}
