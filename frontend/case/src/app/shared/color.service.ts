import { _ } from "lodash-es";
import { WeekDay } from '@angular/common';
import { Injectable } from '@angular/core';
import { DateTime, Duration } from 'luxon';


@Injectable({
  providedIn: 'root'
})
export class ColorService {

  constructor() { }

  public getColor(time: DateTime):string {

    let baseColor = "";

    ///https://developer.mozilla.org/de/docs/Web/JavaScript/Reference/Global_Objects/Date/getDay
    switch (time.toJSDate().getDay()) {
      case WeekDay.Monday:
        baseColor = "green";
        break;
      case WeekDay.Tuesday:
        baseColor = "blue";
        break;
      case WeekDay.Wednesday:
        baseColor = "indigo";
        break;
      case WeekDay.Thursday:
        baseColor = "indigo";
        break;
      case WeekDay.Friday:
        baseColor = "purple";
        break;
      case WeekDay.Saturday:
        baseColor = "pink";
        break;
      default:
        baseColor = "gray";
        break;
    }

    const min = 6;
    // gradient range is 100-900
    const gradient = _.clamp(Math.abs(time.hour - min) * 100, 100, 900);

    var test = `bg-${baseColor}-${gradient}`;
    return test;
  }

}
