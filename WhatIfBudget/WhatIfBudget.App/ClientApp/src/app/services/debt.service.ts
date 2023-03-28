import { KeyValue } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, finalize, Observable, retry } from 'rxjs';
import { environment } from '../../environments/environment';
import { Debt } from '../shared/models/debt';
import { DebtGoal } from '../shared/models/debt-goal';

@Injectable({
  providedIn: 'root'
})
export class DebtService {

  constructor(private http: HttpClient) { }

  public getDebts(): Observable<Debt[]> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.get<Debt[]>(environment.URL + '/api/Debts').pipe(
      retry(3),
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<Debt[]>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('saveIncome');
      })
    );
  }

  public saveDebt(debt: Debt): Observable<Debt> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.post<Debt>(environment.URL + '/api/Debts', debt).pipe(
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<Debt>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('saveIncome');
      })
    );
  }

  public updateDebt(debt: Debt): Observable<Debt> {
    //this.sharedService.queueLoading('updateIncome');
    return this.http.put<Debt>(environment.URL + '/api/Debts', debt).pipe(
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<Debt>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('updateIncome');
      })
    );
  }

  public deleteDebt(debt: Debt): Observable<Debt> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.delete<Debt>(environment.URL + `/api/Debts/${debt.id}/${debt.goalId}`).pipe(
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<Debt>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('saveIncome');
      })
    );
  }

  public getDebtsByGoalId(debtGoalId: number): Observable<Debt[]> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.get<Debt[]>(environment.URL + `/api/Debts/goals/${debtGoalId}`).pipe(
      retry(3),
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<Debt[]>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('saveIncome');
      })
    );
  }

  public getDebtGoalsByGoalId(debtGoalId: number): Observable<DebtGoal[]> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.get<DebtGoal[]>(environment.URL + `/api/DebtsGoals/${debtGoalId}`).pipe(
      retry(3),
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<DebtGoal[]>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('saveIncome');
      })
    );
  }

  public updateDebtGoal(debtGoal: DebtGoal): Observable<DebtGoal> {
    //this.sharedService.queueLoading('updateIncome');
    return this.http.put<DebtGoal>(environment.URL + '/api/DebtGoals', debtGoal).pipe(
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<DebtGoal>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('updateIncome');
      })
    );
  }

  public getDebtGoalBalanceOverTime(debtGoalId: number): Observable<KeyValue<number, number>[]> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.get<KeyValue<number, number>[]>(environment.URL + `/api/DebtGoals/${debtGoalId}/BalanceOverTime`).pipe(
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

  private handleError(err: any): void {
    console.log('Error: ' + err)
    //this.sharedService.clearLoading();
  }
}
