import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'case-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  public todos;

  constructor() {
    this.todos = [
      {
        component: "weekly.component",
        todos: [
          'Manchmal gibt es auch samstags kurse. Ansicht wechselbar 5,6 oder 7 Tage.',
          'getBackground() shall we switch to props get background() instead?',
          'We should provide changeable colors and gradient settings :-)',
          'Add color property to courses.',
          'Change alpha with day time',
          'white color and low alpha have to small contrast.'
        ],
      }, {
        component: "course.component",
        todos: [
          'Add grades to pupils.'
        ]
      },
    ];
   }

  ngOnInit(): void {
  }

}
