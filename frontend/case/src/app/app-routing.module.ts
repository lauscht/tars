import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CourseComponent } from './course/course.component';
import { LoginComponent } from './login/login.component';
import { WeeklyComponent } from './weekly/weekly.component';

const routes: Routes = [  
  { path: 'course', component: CourseComponent },
  { path: 'weekly', component: WeeklyComponent },
  { path: 'login', component: LoginComponent},
  { path: '',   redirectTo: 'login', pathMatch: 'full' },];

@NgModule({
  imports: [RouterModule.forRoot(routes, { anchorScrolling: 'enabled'})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
