import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, finalize, Observable, retry } from 'rxjs';
import { environment } from '../../environments/environment';
import { Mortgage } from '../shared/models/mortgage';

@Injectable({
  providedIn: 'root'
})
export class MortgageService {

  constructor(private http: HttpClient) { }

  public getMortgages(): Observable<Mortgage[]> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.get<Mortgage[]>(environment.URL + '/api/mortgages').pipe(
      retry(3),
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<Mortgage[]>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('saveIncome');
      })
    );
  }

  public saveMortgages(mortgage: Mortgage): Observable<Mortgage> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.post<Mortgage>(environment.URL + '/api/mortgages', mortgage).pipe(
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

  public updateMortgages(mortgage: Mortgage): Observable<Mortgage> {
    //this.sharedService.queueLoading('updateIncome');
    return this.http.put<Mortgage>(environment.URL + '/api/mortgages', mortgage).pipe(
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

  public deleteMortgages(mortgage: Mortgage): Observable<Mortgage> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.delete<Mortgage>(environment.URL + `/api/mortgages/${mortgage.id}`).pipe(
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

  private handleError(err: any): void {
    console.log('Error: ' + err)
    //this.sharedService.clearLoading();
  }
}
