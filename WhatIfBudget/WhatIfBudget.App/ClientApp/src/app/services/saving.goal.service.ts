import { KeyValue } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, finalize, Observable, retry } from 'rxjs';
import { environment } from '../../environments/environment';
import { SavingGoal } from '../shared/models/saving';

@Injectable({
  providedIn: 'root'
})
export class SavingGoalService {

  constructor(private http: HttpClient) { }

  public getSavingGoal(savingGoalId: number): Observable<SavingGoal[]> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.get<SavingGoal[]>(environment.URL + '/api/${savingGoalId}').pipe(
      retry(3),
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<SavingGoal[]>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('saveIncome');
      })
    );
  }

  public getBalanceOverTime(savingGoalId: number): Observable<KeyValue<number, number>[]> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.get<KeyValue<number, number>[]>(environment.URL + `/api/savingGoals/${savingGoalId}/balanceOverTime`).pipe(
      retry(3),
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<KeyValue<number, number>[]>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('saveIncome');
      })
    );
  }

  public getBalanceAtTarget(savingGoalId: number): Observable<number> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.get<number>(environment.URL + `/api/savingGoals/${savingGoalId}/balanceAtTarget`).pipe(
      retry(3),
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<number>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('saveIncome');
      })
    );
  }

  public saveSavingGoal(savingGoal: SavingGoal): Observable<SavingGoal> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.put<SavingGoal>(environment.URL + '/api/savingGoals', savingGoal).pipe(
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<SavingGoal>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('saveIncome');
      })
    );
  }

  public updateSavingGoal(savingGoal: SavingGoal): Observable<SavingGoal> {
    //this.sharedService.queueLoading('updateIncome');
    return this.http.put<SavingGoal>(environment.URL + '/api/savingGoals', savingGoal).pipe(
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<SavingGoal>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('updateIncome');
      })
    );
  }

  private handleError(err: any): void {
    console.log('Error: ' + err)
    //this.sharedService.clearLoading();
  }
}
