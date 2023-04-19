import { KeyValue } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, finalize, Observable, retry } from 'rxjs';
import { environment } from '../../environments/environment';
import { InvestmentGoal, InvestmentTotals } from '../shared/models/investment-goal';


@Injectable({
  providedIn: 'root'
})
export class InvestmentGoalService {

  constructor(private http: HttpClient) { }

  public getInvestmentGoal(goalId: number): Observable<InvestmentGoal> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.get<InvestmentGoal>(environment.URL + `/api/investmentGoals/${goalId}`).pipe(
      retry(3),
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<InvestmentGoal>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('saveIncome');
      })
    );
  }

  public getBalanceOverTime(investmentGoalId: number): Observable<KeyValue<number, number>[]> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.get<KeyValue<number, number>[]>(environment.URL + `/api/investmentGoals/${investmentGoalId}/balanceOverTime`).pipe(
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

  public getInvestmentTotals(investmentGoalId: number): Observable<InvestmentTotals> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.get<InvestmentTotals>(environment.URL + `/api/investmentGoals/${investmentGoalId}/totals`).pipe(
      retry(3),
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<InvestmentTotals>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('saveIncome');
      })
    );
  }

  public saveInvestmentGoal(investmentGoal: InvestmentGoal): Observable<InvestmentGoal> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.put<InvestmentGoal>(environment.URL + '/api/investmentGoals', investmentGoal).pipe(
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<InvestmentGoal>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('saveIncome');
      })
    );
  }

  public updateInvestmentGoal(investmentGoal: InvestmentGoal): Observable<InvestmentGoal> {
    //this.sharedService.queueLoading('updateIncome');
    return this.http.put<InvestmentGoal>(environment.URL + '/api/investmentGoals', investmentGoal).pipe(
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<InvestmentGoal>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('updateIncome');
      })
    );
  }

  public deleteInvestmentGoal(investmentGoal: InvestmentGoal): Observable<InvestmentGoal> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.delete<InvestmentGoal>(environment.URL + '/api/investmentGoals/' + investmentGoal.id).pipe(
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<InvestmentGoal>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('saveIncome');
      })
    );
  }

  public toggleRolloverContributions(investmentGoalId: number): Observable<InvestmentGoal> {
    //this.sharedService.queueLoading('updateIncome');
    return this.http.put<InvestmentGoal>(environment.URL + `/api/investmentGoals/${investmentGoalId}/rollover`, null).pipe(
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<InvestmentGoal>((subscriber) => {
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
