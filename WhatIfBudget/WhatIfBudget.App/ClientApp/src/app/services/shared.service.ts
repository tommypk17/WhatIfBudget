import { KeyValue } from '@angular/common';
import { EventEmitter, Injectable } from '@angular/core';
import { MsalService } from '@azure/msal-angular';
import { AccountInfo } from '@azure/msal-browser';
import { BehaviorSubject } from 'rxjs';
import { EFrequency } from '../shared/enums/efrequency';
import { EPriority } from '../shared/enums/epriority';
import { Budget } from '../shared/models/budget';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  private _loadingQueue: string[] = [];

  isLoadingEmit: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  loggedInEmit: EventEmitter<void> = new EventEmitter<void>();
  budgetLoadedEmit: EventEmitter<void> = new EventEmitter<void>();
  chartReloadEmit: EventEmitter<string> = new EventEmitter<string>();

  constructor(private msalService: MsalService) { }

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
    this.budgetLoadedEmit.emit();
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

  get budgetLoaded(): boolean {
    if (this.budget && this.budget.id) return true;
    else return false;
  }

  get loggedIn(): boolean {
    return !!this.msalService.instance.getAllAccounts()[0];
  }

  logout(): void {
    localStorage.removeItem('budget');
    this.msalService.logout();
  }

  queueLoading(name: string): void {
    this._loadingQueue.push(name);
    this.isLoadingEmit.next(this.loadingQueue.length > 0);
  }

  dequeueLoading(name: string): void {
    let foundIdx = this._loadingQueue.findIndex(x => x == name);
    if (foundIdx > -1) this._loadingQueue.splice(foundIdx, 1);
    this.isLoadingEmit.next(this.loadingQueue.length > 0);
  }

  get loadingQueue(): string[] {
    return this._loadingQueue;
  }

  get user(): AccountInfo | undefined {
    if (this.loggedIn) {
      return this.msalService.instance.getAllAccounts()[0];
    }
    return undefined;
  }

  reloadCharts(type: string): void {
    if (type == 'all') {
      this.chartReloadEmit.emit('mortgage');
      this.chartReloadEmit.emit('investments');
      this.chartReloadEmit.emit('savings');
      this.chartReloadEmit.emit('debt');
    }
    else this.chartReloadEmit.emit(type);
  }
}
