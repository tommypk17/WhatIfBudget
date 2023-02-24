import { KeyValue } from '@angular/common';
import { Injectable } from '@angular/core';
import { EFrequency } from '../shared/enums/efrequency';
import { EPriority } from '../shared/enums/epriority';
import { Budget } from '../shared/models/budget';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  constructor() { }

  get frequencies(): KeyValue<number, string>[] {
    let frequencies: KeyValue<number, string>[] = [];
    Object.values(EFrequency).filter((o) => typeof o == 'string').forEach((v,i,a) => {
      frequencies.push({ key: i, value: v as string});
    });
    return frequencies;
  }

  get priorities(): KeyValue<number, string>[] {
    let frequencies: KeyValue<number, string>[] = [];
    Object.values(EPriority).filter((o) => typeof o == 'string').forEach((v, i, a) => {
      frequencies.push({ key: i, value: v as string });
    });
    return frequencies;
  }

  set budget(budget: Budget) {
    localStorage.setItem("budget", JSON.stringify(budget));
  }

  get budget(): Budget {
    let temp: string | null = localStorage.getItem("budget");
    if (temp != null) {
      try {
        return JSON.parse(temp) as Budget;
      } catch {

      }
    }
    return new Budget();
  }
}
