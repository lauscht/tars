import { Course } from '../course/course.entity';

export class Assessment{
  constructor(
    public course: Course,
    public title: string,
    public category: string,
  ) {

  }
}
