import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {MsalService} from "@azure/msal-angular";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private http: HttpClient, private msalService: MsalService) { }

  login(): Observable<void> {
    return this.msalService.loginRedirect();
  }

  logout(): Observable<void> {
    return this.msalService.logout();
  }

  isLoggedIn(): Observable<boolean> {
    return new Observable<boolean>((subscriber) => {
      let account = this.msalService.instance.getAllAccounts()[0];
      if(account) subscriber.next(true);
      else subscriber.next(false);
    });
  }

}
