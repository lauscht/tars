import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MatTooltipModule } from '@angular/material/tooltip';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { WeeklyComponent } from './weekly/weekly.component';
import { LessonEditComponent } from './lesson/lesson-edit.component';
import { LessonComponent } from './lesson/lesson.component';
import { MatInputModule } from '@angular/material/input';
import { MatTabsModule } from '@angular/material/tabs';
import { FormsModule } from '@angular/forms';
import { LuxonModule } from 'luxon-angular';
import { MarkdownModule } from 'ngx-markdown';
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
