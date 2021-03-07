import { DateTime, Interval } from 'luxon';
import { Course } from '../course/course.entity';
import { Lesson } from '../lesson/lesson.entity';

export class Assessment{
  constructor(
    public date: DateTime,
    public courseId: number,
    public context: string,
    public category: string,
    public weigth: number=1,
    public average: number=null,
    public lesson: Lesson=null,
  ) {

  }
}
