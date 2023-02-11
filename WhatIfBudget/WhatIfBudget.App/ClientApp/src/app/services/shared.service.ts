import { KeyValue } from '@angular/common';
import { Injectable } from '@angular/core';
import { EFrequency } from '../shared/enums/efrequency';
import { EPriority } from '../shared/enums/epriority';

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
}
