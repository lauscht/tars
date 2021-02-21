import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatInputModule } from '@angular/material/input';
import { MatTabsModule } from '@angular/material/tabs';

import { LuxonModule } from 'luxon-angular';
import { MatLuxonDateModule } from 'ngx-material-luxon';
import { MarkdownModule } from 'ngx-markdown';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';

import { HeaderComponent } from './header/header.component';
import { WeeklyComponent } from './weekly/weekly.component';
import { CourseComponent } from './course/course.component';

import { LessonComponent } from './lesson/lesson.component';
import { LessonEditComponent } from './lesson/lesson-edit.component';

import { FlyoutComponent } from './shared/flyout/flyout.component';
import { LessonTileComponent } from './lesson/lesson-tile.component';
import { WeekInputComponent } from './shared/week-input/week-input.component';
import { ScheduleComponent } from './schedule/schedule.component';

@NgModule({
  declarations: [
    AppComponent,
    WeekInputComponent,
    WeeklyComponent,
    LessonComponent,
    LessonEditComponent,
    FlyoutComponent,
    LessonTileComponent,
    CourseComponent,
    HeaderComponent,
    ScheduleComponent,
  ],

  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MarkdownModule.forRoot(),
    AppRoutingModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatTooltipModule,
    MatInputModule,
    MatTabsModule,
    FormsModule,
    LuxonModule,
    MatLuxonDateModule,
    NgbModule,
  ],
  providers: [
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
