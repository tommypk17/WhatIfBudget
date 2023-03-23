import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, finalize, Observable, retry } from 'rxjs';
import { environment } from '../../environments/environment';
import { Debt } from '../shared/models/debt';

@Injectable({
  providedIn: 'root'
})
export class DebtService {

  constructor(private http: HttpClient) { }

  public getDebts(): Observable<Debt[]> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.get<Debt[]>(environment.URL + '/api/debts').pipe(
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

  public saveDebts(debt: Debt): Observable<Debt> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.post<Debt>(environment.URL + '/api/debts', debt).pipe(
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

  public updateDebts(debt: Debt): Observable<Debt> {
    //this.sharedService.queueLoading('updateIncome');
    return this.http.put<Debt>(environment.URL + '/api/debts', debt).pipe(
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

  public deleteDebts(debt: Debt): Observable<Debt> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.delete<Debt>(environment.URL + `/api/debts/${debt.id}`).pipe(
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

  private handleError(err: any): void {
    console.log('Error: ' + err)
    //this.sharedService.clearLoading();
  }
}
