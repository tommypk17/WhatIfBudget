import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, finalize, Observable, retry } from 'rxjs';
import { environment } from '../../environments/environment';
import { Expense } from '../shared/models/expense';

@Injectable({
  providedIn: 'root'
})
export class ExpenseService {

  constructor(private http: HttpClient) { }

  public getExpenses(): Observable<Expense[]> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.get<Expense[]>(environment.URL + '/api/expenses').pipe(
      retry(3),
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<Expense[]>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('saveIncome');
      })
    );
  }

  public getExpensesByBudgetId(budgetId: number): Observable<Expense[]> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.get<Expense[]>(environment.URL + `/api/expenses/budgets/${budgetId}`).pipe(
      retry(3),
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<Expense[]>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('saveIncome');
      })
    );
  }

  public getMonthlyExpenseByBudgetId(budgetId: number): Observable<number> {
    return this.http.get<number>(environment.URL + `/api/expenses/budgets/${budgetId}/monthlyExpense`).pipe(
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

  public saveExpense(expense: Expense): Observable<Expense> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.post<Expense>(environment.URL + '/api/expenses', expense).pipe(
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<Expense>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('saveIncome');
      })
    );
  }

  public updateExpense(expense: Expense): Observable<Expense> {
    //this.sharedService.queueLoading('updateIncome');
    return this.http.put<Expense>(environment.URL + '/api/expenses', expense).pipe(
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<Expense>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('updateIncome');
      })
    );
  }

  public deleteExpense(expense: Expense): Observable<Expense> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.delete<Expense>(environment.URL + `/api/expenses/ ${expense.id}/${expense.budgetId}`).pipe(
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<Expense>((subscriber) => {
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
