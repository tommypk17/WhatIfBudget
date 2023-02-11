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

  private handleError(err: any): void {
    console.log('Error: ' + err)
    //this.sharedService.clearLoading();
  }
}
