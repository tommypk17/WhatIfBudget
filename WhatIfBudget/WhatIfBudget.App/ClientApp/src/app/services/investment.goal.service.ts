import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, finalize, Observable, retry } from 'rxjs';
import { environment } from '../../environments/environment';
import { InvestmentGoal } from '../shared/models/investment-goal';


@Injectable({
  providedIn: 'root'
})
export class InvestmentGoalService {

  constructor(private http: HttpClient) { }

  public getInvestmentGoals(): Observable<InvestmentGoal[]> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.get<InvestmentGoal[]>(environment.URL + '/api/investmentGoals').pipe(
      retry(3),
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<InvestmentGoal[]>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('saveIncome');
      })
    );
  }

  public saveInvestmentGoals(investmentGoal: InvestmentGoal): Observable<InvestmentGoal> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.post<InvestmentGoal>(environment.URL + '/api/investments', investmentGoal).pipe(
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<InvestmentGoal>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('saveIncome');
      })
    );
  }

  public updateInvestmentGoal(investmentGoal: InvestmentGoal): Observable<InvestmentGoal> {
    //this.sharedService.queueLoading('updateIncome');
    return this.http.put<InvestmentGoal>(environment.URL + '/api/investmentGoals', investmentGoal).pipe(
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<InvestmentGoal>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('updateIncome');
      })
    );
  }

  public deleteInvestmentGoal(investmentGoal: InvestmentGoal): Observable<InvestmentGoal> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.delete<InvestmentGoal>(environment.URL + '/api/investmentGoals/' + investmentGoal.id).pipe(
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<InvestmentGoal>((subscriber) => {
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
