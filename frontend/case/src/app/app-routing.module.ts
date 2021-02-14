import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CourseComponent } from './course/course.component';
import { WeeklyComponent } from './weekly/weekly.component';

const routes: Routes = [  
  { path: 'course', component: CourseComponent },
  { path: 'weekly', component: WeeklyComponent },
  { path: '',   redirectTo: 'weekly', pathMatch: 'full' },];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
