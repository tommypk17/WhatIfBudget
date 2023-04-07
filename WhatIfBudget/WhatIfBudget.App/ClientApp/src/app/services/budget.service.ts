import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, finalize, Observable, retry } from 'rxjs';
import { environment } from '../../environments/environment';
import { AdditionalContributions, Budget } from '../shared/models/budget';

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

  public getAditionalContributions(budgetId: number): Observable<AdditionalContributions> {
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

  public updateAditionalContributions(budgetId: number): Observable<AdditionalContributions> {
    return this.http.put<AdditionalContributions>(environment.URL + `/api/incomes/budgets/${budgetId}/aditionalContributions`, AdditionalContributions).pipe(
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
