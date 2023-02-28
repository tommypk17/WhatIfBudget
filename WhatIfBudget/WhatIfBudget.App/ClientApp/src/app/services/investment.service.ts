import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, finalize, Observable, retry } from 'rxjs';
import { environment } from '../../environments/environment';
import { Investment } from '../shared/models/investment';

@Injectable({
  providedIn: 'root'
})
export class InvestmentService {

  constructor(private http: HttpClient) { }

  public getInvestments(): Observable<Investment[]> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.get<Investment[]>(environment.URL + '/api/investments').pipe(
      retry(3),
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<Investment[]>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('saveIncome');
      })
    );
  }

  public getInvestmentsByGoalId(goalId: number): Observable<Investment[]> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.get<Investment[]>(environment.URL + `/api/investments/goals/${goalId}`).pipe(
      retry(3),
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<Investment[]>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('saveIncome');
      })
    );
  }

  public saveInvestments(investment: Investment): Observable<Investment> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.post<Investment>(environment.URL + '/api/investments', investment).pipe(
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<Investment>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('saveIncome');
      })
    );
  }

  public updateInvestment(investment: Investment): Observable<Investment> {
    //this.sharedService.queueLoading('updateIncome');
    return this.http.put<Investment>(environment.URL + '/api/investments', investment).pipe(
      retry(3),
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<Investment>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('updateIncome');
      })
    );
  }

  public deleteInvestment(investment: Investment): Observable<Investment> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.delete<Investment>(environment.URL + '/api/investments/' + investment.id).pipe(
      retry(3),
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<Investment>((subscriber) => {
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
