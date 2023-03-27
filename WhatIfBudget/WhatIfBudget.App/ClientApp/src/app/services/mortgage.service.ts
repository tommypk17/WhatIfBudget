import { KeyValue } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, finalize, Observable, retry } from 'rxjs';
import { environment } from '../../environments/environment';
import { Mortgage, MortgageTotals } from '../shared/models/mortgage';

@Injectable({
  providedIn: 'root'
})
export class MortgageService {

  constructor(private http: HttpClient) { }

  public getMortgageByGoalId(MortgageGoalId: number): Observable<Mortgage> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.get<Mortgage>(environment.URL + `/api/MortgageGoals/${MortgageGoalId}`).pipe(
      retry(3),
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<Mortgage>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('saveIncome');
      })
    );
  }

  public getMortgageTotals(MortgageGoalId: number): Observable<MortgageTotals> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.get<MortgageTotals>(environment.URL + `/api/investmentGoals/${MortgageGoalId}/totals`).pipe(
      retry(3),
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<MortgageTotals>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('saveIncome');
      })
    );
  }

  public getNetValueOverTime(MortgageGoalId: number): Observable<KeyValue<number, number>[]> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.get<KeyValue<number, number>[]>(environment.URL + `/api/MortgageGoals/${MortgageGoalId}/NetValueOverTime`).pipe(
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

  public getMortgageAmortization(MortgageGoalId: number): Observable<KeyValue<number, number[]>[]> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.get<KeyValue<number, number[]>[]>(environment.URL + `/api/MortgageGoals/${MortgageGoalId}/Amortization`).pipe(
      retry(3),
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<KeyValue<number, number[]>[]>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('saveIncome');
      })
    );
  }

  public saveMortgages(mortgage: Mortgage): Observable<Mortgage> {
    //this.sharedService.queueLoading('updateIncome');
    return this.http.put<Mortgage>(environment.URL + '/api/MortgageGoals', mortgage).pipe(
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<Mortgage>((subscriber) => {
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
