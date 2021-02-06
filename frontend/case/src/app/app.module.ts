import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MatTooltipModule } from '@angular/material/tooltip';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { WeeklyComponent } from './weekly/weekly.component';
import { LessonComponent } from './lesson/lesson.component';

@NgModule({
  declarations: [
    AppComponent,
    WeeklyComponent,
    LessonComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatTooltipModule,
    BrowserAnimationsModule,
    NgbModule
  ],
  providers: [
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
