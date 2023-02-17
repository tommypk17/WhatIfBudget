import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, finalize, Observable, retry } from 'rxjs';
import { environment } from '../../environments/environment';
import { Income } from '../shared/models/income';

@Injectable({
  providedIn: 'root'
})
export class IncomeService {

  constructor(private http: HttpClient) { }

  public getIncomes(): Observable<Income[]> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.get<Income[]>(environment.URL + '/api/incomes').pipe(
      retry(3),
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<Income[]>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('saveIncome');
      })
    );
  }

  public saveIncome(income: Income): Observable<Income> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.post<Income>(environment.URL + '/api/incomes', income).pipe(
      retry(3),
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<Income>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('saveIncome');
      })
    );
  }

  public deleteIncome(income: Income): Observable<Income> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.delete<Income>(environment.URL + '/api/incomes/' + income.id).pipe(
      retry(3),
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<Income>((subscriber) => {
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
