import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, finalize, Observable, retry } from 'rxjs';
import { environment } from '../../environments/environment';
import { Budget } from '../shared/models/budget';

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
  private handleError(err: any): void {
    console.log('Error: ' + err)
    //this.sharedService.clearLoading();
  }
}
