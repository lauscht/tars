import _ from "lodash-es";
import { WeekDay } from '@angular/common';
import { Injectable } from '@angular/core';
import { DateTime, Duration } from 'luxon';
import { ColorInfo } from "./color.info";


@Injectable({
  providedIn: 'root'
})
export class ColorService {

  WeekColorMap: Map<WeekDay, string>;

  constructor() {

    this.WeekColorMap = new Map([
      [WeekDay.Monday, "purple"],
      [WeekDay.Tuesday, "blue"],
      [WeekDay.Wednesday, "indigo"],
      [WeekDay.Thursday, "green"],
      [WeekDay.Friday, "yellow"],
      [WeekDay.Saturday, "red"]
    ]);

  }

  public getColor(time: DateTime): ColorInfo {

    let baseColor = "";
      ///https://developer.mozilla.org/de/docs/Web/JavaScript/Reference/Global_Objects/Date/getDay
    const day = time.toJSDate().getDay();

    if (!this.WeekColorMap.has(day))
      baseColor = "gray";
    else
      baseColor = this.WeekColorMap.get(day);

    const min = 6;
    // gradient range is 100-900
    const gradient = _.clamp(Math.abs(time.hour - min) * 100, 100, 900);

    return new ColorInfo(baseColor, gradient, gradient - 100);
  }

}
