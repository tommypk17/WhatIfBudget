import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import { MsalService } from '@azure/msal-angular';
import { AuthenticationResult, SilentRequest } from '@azure/msal-browser';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.prod';
import {AuthService} from "../services/auth.service";

@Injectable({
  providedIn: 'root'
})
export class LoginGuard implements CanActivate {
  constructor(private authService: AuthService, private msalService: MsalService, private router: Router) {
  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return new Observable<boolean>((subscriber) => {
      this.authService.isLoggedIn().subscribe((res: boolean) => {
        if (res) {
          let silent: SilentRequest = {
            scopes: [environment.AzureAd.defaultScope],
            forceRefresh: true,
            account: this.msalService.instance.getAllAccounts()[0]
          };
          this.msalService.instance.acquireTokenSilent(silent).then((res: AuthenticationResult) => {
            subscriber.next(true);
          });
        } else {
          sessionStorage.setItem('afterLogin', state.url);
          this.router.navigate(['/login'], {skipLocationChange: true});
          subscriber.next(false);
        }
      });
    });
  }

}
