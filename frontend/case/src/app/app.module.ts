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
import { MarkdownModule } from 'ngx-markdown';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { WeeklyComponent } from './weekly/weekly.component';
import { LessonEditComponent } from './lesson/lesson-edit.component';
import { LessonComponent } from './lesson/lesson.component';
import { FlyoutComponent } from './shared/flyout/flyout.component';


@NgModule({
  declarations: [
    AppComponent,
    WeeklyComponent,
    LessonComponent,
    LessonEditComponent,
    FlyoutComponent
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
    NgbModule    
  ],
  providers: [
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
