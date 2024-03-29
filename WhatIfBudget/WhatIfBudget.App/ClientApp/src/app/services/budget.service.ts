import { KeyValue } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, finalize, Observable, retry } from 'rxjs';
import { environment } from '../../environments/environment';
import { AdditionalContributions, Budget, NetValue } from '../shared/models/budget';

@Injectable({
  providedIn: 'root'
})
export class BudgetService {

  constructor(private http: HttpClient) { }

  public getBudgets(): Observable<Budget[]> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.get<Budget[]>(environment.URL + '/api/budgets').pipe(
      retry(3),
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<Budget[]>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('saveIncome');
      })
    );
  }

  public saveBudget(budget: Budget): Observable<Budget> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.post<Budget>(environment.URL + '/api/budgets', budget).pipe(
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<Budget>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('saveIncome');
      })
    );
  }

  public updateBudget(budget: Budget): Observable<Budget> {
    //this.sharedService.queueLoading('updateIncome');
    return this.http.put<Budget>(environment.URL + '/api/budgets', budget).pipe(
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<Budget>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('updateIncome');
      })
    );
  }

  public getNetAvailable(budgetId: number): Observable<number> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.get<number>(environment.URL + `/api/budgets/${budgetId}/netAvailable`).pipe(
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

  public getAdditionalContributions(budgetId: number): Observable<AdditionalContributions> {
    return this.http.get<AdditionalContributions>(environment.URL + `/api/budgets/${budgetId}/additionalContributions`).pipe(
      retry(3),
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<AdditionalContributions>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('saveIncome');
      })
    );
  }

  public updateAdditionalContributions(budgetId: number, additionalContributions: AdditionalContributions): Observable<AdditionalContributions> {
    return this.http.put<AdditionalContributions>(environment.URL + `/api/budgets/${budgetId}/additionalContributions`, additionalContributions).pipe(
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<AdditionalContributions>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('updateIncome');
      })
    );
  }

  public getCurrentNetWorth(budgetId: number): Observable<number> {
    return this.http.get<number>(environment.URL + `/api/budgets/${budgetId}/currentNetWorth`).pipe(
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

  public getNetWorthOverTime(budgetId: number): Observable<NetValue> {
    return this.http.get<NetValue>(environment.URL + `/api/budgets/${budgetId}/netWorthOverTime`).pipe(
      retry(3),
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<NetValue>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('saveIncome');
      })
    );
  }

  public getAvailableFreeCash(budgetId: number): Observable<number> {
    return this.http.get<number>(environment.URL + `/api/budgets/${budgetId}/availableFreeCash`).pipe(
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


  public deleteBudget(budget: Budget): Observable<Budget> {
    let budgetId: number = budget.id!;
    return this.http.delete<Budget>(environment.URL + `/api/budgets/${budgetId}`).pipe(
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<Budget>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
      })
    );
  }

  private handleError(err: any): void {
    console.log('Error: ' + err)
    //this.sharedService.clearLoading();
  }
}
