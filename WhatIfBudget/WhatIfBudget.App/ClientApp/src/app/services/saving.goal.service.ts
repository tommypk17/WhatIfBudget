import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, finalize, Observable, retry } from 'rxjs';
import { environment } from '../../environments/environment';
import { SavingGoal } from '../shared/models/saving';

@Injectable({
  providedIn: 'root'
})
export class SavingGoalService {

  constructor(private http: HttpClient) { }

  public getSavingGoal(): Observable<SavingGoal[]> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.get<SavingGoal[]>(environment.URL + '/api/savingGoal').pipe(
      retry(3),
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<SavingGoal[]>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('saveIncome');
      })
    );
  }

  public saveSavingGoal(savingGoal: SavingGoal): Observable<SavingGoal> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.post<SavingGoal>(environment.URL + '/api/savingGoal', savingGoal).pipe(
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<SavingGoal>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('saveIncome');
      })
    );
  }

  public updateSavingGoal(savingGoal: SavingGoal): Observable<SavingGoal> {
    //this.sharedService.queueLoading('updateIncome');
    return this.http.put<SavingGoal>(environment.URL + '/api/savingGoal', savingGoal).pipe(
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<SavingGoal>((subscriber) => {
          subscriber.next(undefined);
        })
      }),
      finalize(() => {
        //this.sharedService.dequeueLoading('updateIncome');
      })
    );
  }

  public deleteSavingGoal(savingGoal: SavingGoal): Observable<SavingGoal> {
    //this.sharedService.queueLoading('saveIncome');
    return this.http.delete<SavingGoal>(environment.URL + `/api/savingGoal/${savingGoal.id}`).pipe(
      catchError((err, caught) => {
        this.handleError(err);
        return new Observable<SavingGoal>((subscriber) => {
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
